using System;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeView : MonoBehaviour
{
    public event Action OnButtonPlayClicked;
    public event Action OnButtonQuitClicked;
    
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonQuit;
    [SerializeField] private AudioSource audioSource;
    [Space(10)]
    [SerializeField] private AudioClip clipClick;

    private void OnEnable()
    {
        buttonPlay.onClick.AddListener(PlayGame);
        buttonQuit.onClick.AddListener(QuitGame);
    }

    private void OnDisable()
    {
        buttonPlay.onClick.RemoveAllListeners();
        buttonQuit.onClick.RemoveAllListeners();
    }

    private void PlayGame()
    {
        ClickButton(clipClick);
        OnButtonPlayClicked?.Invoke();
    }

    private void QuitGame()
    {
        ClickButton(clipClick);
        OnButtonQuitClicked?.Invoke();
    }

    private void ClickButton(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
