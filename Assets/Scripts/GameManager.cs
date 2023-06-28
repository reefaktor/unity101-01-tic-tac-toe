using System.Collections;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WelcomeView welcomeView;
    [SerializeField] private GameView gameView;
    [SerializeField] private GameObject[] viewsAndModals;
    [Space(10), Header("Balancing")]
    [SerializeField] private float adversaryDelayMin = 1f;
    [SerializeField] private float adversaryDelayMax = 3f;

    private GameGrid grid;
    private GameController controller;
    private AdversaryController adversary;

    private void InitGame()
    {
        Debug.Log($"GameManager.InitGame");

        grid = new GameGrid();
        controller = new GameController(grid);
        adversary = new AdversaryController(grid, controller);

        AddListeners();

        StartCoroutine(InitController());
    }

    private IEnumerator InitController(float delay = 2f)
    {
        yield return new WaitForSeconds(delay);
        controller.Init(PlayerMarking.One);
    }
    
    private void FinishGame()
    {
        RemoveListeners();
    }

    private void AddListeners()
    {
        controller.OnPlayerChanged += OnPlayerChangedHandler;
        controller.OnMoveMade += OnMoveMadeHandler;
        controller.OnGameFinished += OnGameFinishedHandler;
    }

    private void RemoveListeners()
    {
        controller.OnPlayerChanged -= OnPlayerChangedHandler;
        controller.OnMoveMade -= OnMoveMadeHandler;
        controller.OnGameFinished -= OnGameFinishedHandler;
    }


    private void OnPlayerChangedHandler(PlayerMarking player)
    {
        print($"OnPlayerChangedHandler Player:{player.ToString()}");
        var state = player == PlayerMarking.One;
        gameView.EnableCells(state);
        gameView.ShowTurn(player);
    }

    private void OnMoveMadeHandler(int position, PlayerMarking player)
    {
        print($"OnMoveMadeHandler pos:{position}, player:{player.ToString()}");
        gameView.SelectCell(position, player);
        if (player == PlayerMarking.Two) return;

        var delay = Random.Range(adversaryDelayMin, adversaryDelayMax);
        StartCoroutine(PlayAdversary(delay));
    }

    private void OnGameFinishedHandler(PlayerMarking player)
    {
        print($"OnGameFinishedHandler winner:{player.ToString()}");
        gameView.EnableCells(false);
        gameView.ShowWinner(player);
        
        FinishGame();
    }

    private IEnumerator PlayAdversary(float delay = 1f)
    {
        yield return new WaitForSeconds(delay);
        adversary.MakeMove();
    }

    private void OnGridCellSelectedHandler(int position)
    {
        print($"OnGridCellSelectedHandler pos:{position}");
        controller.Play(position, PlayerMarking.One);
    }
    
    private void OnEnable()
    {
        welcomeView.OnButtonPlayClicked += ShowGameView;
        welcomeView.OnButtonQuitClicked += QuitGame;

        gameView.OnButtonQuitClicked += ShowWelcomeView;
        gameView.OnCellSelected += OnGridCellSelectedHandler;
    }

    private void OnDisable()
    {
        welcomeView.OnButtonPlayClicked -= ShowGameView;
        welcomeView.OnButtonQuitClicked -= QuitGame;

        gameView.OnButtonQuitClicked -= ShowWelcomeView;
        gameView.OnCellSelected -= OnGridCellSelectedHandler;
    }

    private void HideAllViews()
    {
        foreach (var view in viewsAndModals)
        {
            view.SetActive(false);
        }
    }

    private void ShowWelcomeView()
    {
        StopAllCoroutines();
        
        HideAllViews();
        welcomeView.gameObject.SetActive(true);
    }

    private void ShowGameView()
    {
        HideAllViews();
        gameView.ClearBoard();
        gameView.EnableCells(false);
        gameView.gameObject.SetActive(true);
        
        InitGame();
    }

    private void QuitGame()
    {
        print($"QuitGame");
        StartCoroutine(CloseApp());
    }

    private IEnumerator CloseApp(float delay = 0.4f)
    {
        yield return new WaitForSeconds(delay);
        
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #endif
        Application.Quit();
    }
}
