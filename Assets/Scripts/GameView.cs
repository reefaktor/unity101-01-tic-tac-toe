using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public event Action OnButtonQuitClicked;
    public event Action<int> OnCellSelected;
    
    [SerializeField] private Button buttonQuit;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Image imageTurn;
    [SerializeField] private Image imageMode;
    [SerializeField] private Image imageWinner;
    [SerializeField] private TextMeshProUGUI labelWinner;
    [SerializeField] private CellView[] cells;
    [Space(10)]
    [SerializeField] private AudioClip sfxClick;
    [SerializeField] private AudioClip sfxWin;
    [SerializeField] private AudioClip sfxLose;
    [SerializeField] private AudioClip sfxTie;
    [SerializeField] private Sprite[] spriteMarkings;
    [SerializeField] private Sprite[] spriteTurns;
    [SerializeField] private Sprite[] spriteModes;
    

    public void ClearBoard()
    {
        labelWinner.text = "";
        imageWinner.gameObject.SetActive(false);
        ShowTurn(PlayerMarking.Empty);
        
        foreach (var cell in cells)
        {
            cell.Init();
        }
    }

    public void EnableCells(bool enable)
    {
        foreach (var cellView in cells)
        {
            cellView.SetEnabled(enable);
        }
    }
    
    public void SelectCell(int index, PlayerMarking player)
    {
        print($"SelectCell player.{player.ToString()}, pos:{index}");
        var sprite = spriteMarkings[(int)player];
        cells[index].SetMarking(sprite);
        PlaySound(sfxClick);
    }

    public void ShowWinner(PlayerMarking player)
    {
        var text = player switch
        {
            PlayerMarking.One => "o won!",
            PlayerMarking.Two => "x won!",
            _ => "tie!!!"
        };
        var sfx = player switch
        {
            PlayerMarking.One => sfxWin,
            PlayerMarking.Two => sfxLose,
            _ => sfxTie
        };

        labelWinner.text = text;
        imageWinner.gameObject.SetActive(true);
        PlaySound(sfx);
    }

    public void ShowTurn(PlayerMarking player)
    {
        if (player == PlayerMarking.Empty)
        {
            imageTurn.gameObject.SetActive(false);
            return;
        }

        imageTurn.sprite = spriteTurns[(int)player];
        imageTurn.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        buttonQuit.onClick.AddListener(QuitGame);
        foreach (var cellView in cells)
        {
            cellView.OnCellClicked += OnCellClickedHandler;
        }
    }
    
    private void OnDisable()
    {
        foreach (var cellView in cells)
        {
            cellView.OnCellClicked -= OnCellClickedHandler;
        }
        buttonQuit.onClick.RemoveAllListeners();
    }
    
    private void OnCellClickedHandler(int position)
    {
        print($"OnCellClickedHandler index: {position}");
        OnCellSelected?.Invoke(position);
    }

    private void QuitGame()
    {
        PlaySound(sfxClick);
        OnButtonQuitClicked?.Invoke();
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    
}
