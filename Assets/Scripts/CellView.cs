using System;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    public event Action<int> OnCellClicked;
    [SerializeField] private Image image;
    [SerializeField] private Button button;
    [SerializeField] private bool isEnabled;
    [SerializeField] private bool isPlayed;
    private static Color transparent = new(1f, 1f, 1f, 0f);
    private static Color opaque = new(1f, 1f, 1f, 1f);

    public void Init()
    {
        image.color = transparent;
        isPlayed = false;
        isEnabled = false;
    }

    public void SetEnabled(bool value)
    {
        if (value && isPlayed) return;
        isEnabled = value;
    }

    public void SetMarking(Sprite sprite)
    {
        image.sprite = sprite;
        image.color = opaque;
        isPlayed = true;
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnClickHandler);
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    private void OnClickHandler()
    {
        if (isPlayed || !isEnabled) return;
        
        var id = GetCellNumber();
        OnCellClicked?.Invoke(id);
    }

    private int GetCellNumber()
    {
        var index = gameObject.name.Length - 1;
        var lastChar = gameObject.name.Substring(index);
        var last = int.Parse(lastChar);
        return last;
    }
    
}
