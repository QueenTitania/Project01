using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashImage : MonoBehaviour
{
    Image image = null;
    Coroutine currentFlash = null;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void StartFlash(float secFlash, float maxAlpha)
    {
        image.color = Color.white;
        maxAlpha = Mathf.Clamp(maxAlpha, 0, 1);

        if(currentFlash != null)
            StopCoroutine(currentFlash);

        currentFlash = StartCoroutine(Flash(secFlash,maxAlpha));
    }

    private IEnumerator Flash(float secFlash, float maxAlpha)
    {
        float flashIn = secFlash / 4;
        for (float t = 0; t <= flashIn; t += Time.deltaTime)
        {
            Color currentColor = image.color;
            currentColor.a = Mathf.Lerp(0, maxAlpha, t / flashIn);
            image.color = currentColor;

            yield return null;
        }

        for (float t = 0; t <= secFlash/2; t += Time.deltaTime)
        {
            yield return null;
        }

        float flashOut = secFlash / 4;
        for (float t = 0; t <= flashOut; t += Time.deltaTime)
        {
            Color currentColor = image.color;
            currentColor.a = Mathf.Lerp(maxAlpha, 0, t / flashOut);
            image.color = currentColor;

            yield return null;
        }
        
    }

}
