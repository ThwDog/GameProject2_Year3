using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeing : MonoBehaviour
{

    [Header("Color Sprite")]
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeDuration = 1f;

    public void fadeIN(){
        Image image = gameObject.GetComponent<Image>();
        StartCoroutine(Fade(image,defaultColor));
    }

    public void fadeOut(){
        Image image = gameObject.GetComponent<Image>();
        StartCoroutine(Fade(image,fadeColor));
    }

    private IEnumerator Fade(Image sprite, Color endColor) {
        if(!sprite) yield break;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration) {
            sprite.color = Color.Lerp(sprite.color, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

       sprite.color = endColor;
    } 
}
