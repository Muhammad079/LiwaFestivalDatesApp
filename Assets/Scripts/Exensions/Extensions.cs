using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public static class Extensions
{
    public static IEnumerator MoveToPosition(Transform Obj, Vector3 finalPos, float transitionTime = 1)
    {
        float elapsedTime = 0;
        Vector3 startPos = Obj.position;
        while (transitionTime > elapsedTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionTime;
            Obj.transform.position = Vector3.Lerp(startPos, finalPos, elapsedTime);
            yield return null;
        }
        Obj.transform.position = finalPos;
    }
    public static IEnumerator ScaleObject(Transform Obj, Vector3 finalScale, float transitionTime = 1)
    {
        float elapsedTime = 0;
        Vector3 startScale = Obj.localScale;
        while (transitionTime > elapsedTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionTime;
            Obj.transform.localScale = Vector3.Lerp(startScale, finalScale, elapsedTime);
            yield return null;
        }
        Obj.transform.localScale = finalScale;
    }
    public static IEnumerator FadeIn(CanvasGroup screen, float transitionTime = 1)
    {
        float elapsedTime = 0;
        screen.alpha = 0;
        while (elapsedTime < transitionTime)
        {
            elapsedTime += Time.deltaTime;
            screen.alpha = Mathf.Clamp01(elapsedTime / transitionTime);
            yield return null;
        }
        screen.alpha = 1;
    }
    public static IEnumerator FadeOut(CanvasGroup screen, float transitionTime = 1)
    {
        float elapsedTime = 0;
        screen.alpha = 1;
        while (elapsedTime < transitionTime)
        {
            elapsedTime += Time.deltaTime;
            screen.alpha = Mathf.Clamp01(1 - (elapsedTime / transitionTime));
            yield return null;
        }
        screen.alpha = 0;

    }
}
