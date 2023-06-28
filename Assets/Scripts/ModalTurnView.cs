using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ModalTurnView : MonoBehaviour
{
    [SerializeField] private Image imageTurn;
    [SerializeField] private Sprite[] spriteTurn;
    [SerializeField] private float timeShown = 1f;

    public void Show(PlayerMarking marking)
    {
        imageTurn.sprite = spriteTurn[(int)marking];
        gameObject.SetActive(true);
        StartCoroutine(HideTimed(timeShown));
    }

    private IEnumerator HideTimed(float delay = 1f)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
