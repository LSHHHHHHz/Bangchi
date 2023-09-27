using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circle : MonoBehaviour
{
    public float startScale = 0.3f;
    public float endScale = 0.6f;
    public float fadeStartScale = 0.5f;
    public float duration = 1f;
    public bool shouldRepeat = true;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        if (image == null)
        {
            Debug.LogWarning("Image ¾øÀ½");
        }
        StartCoroutine(AnimateScaleAndFade());
    }

    IEnumerator AnimateScaleAndFade()
    {
        while (shouldRepeat)
        {
            float startTime = Time.time;
            while (Time.time - startTime < duration)
            {
                float elapsed = Time.time - startTime;
                float progress = elapsed / duration;
                float currentScale = Mathf.Lerp(startScale, endScale, progress);
                transform.localScale = Vector3.one * currentScale;
                if (currentScale > fadeStartScale)
                {
                    float alphaProgress = (currentScale - fadeStartScale) / (endScale - fadeStartScale);
                    Color color = image.color;
                    color.a = Mathf.Lerp(1f, 0f, alphaProgress);
                    image.color = color;
                }
                yield return null;
            }
            transform.localScale = Vector3.one * endScale;
            Color finalColor = image.color;
            finalColor.a = 0f;
            image.color = finalColor;
            ResetToStartState();
            yield return new WaitForSeconds(1f);
        }
    }

    void ResetToStartState()
    {
        transform.localScale = Vector3.one * startScale;
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;
    }
}
