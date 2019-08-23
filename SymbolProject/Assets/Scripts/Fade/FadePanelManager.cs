﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePanelManager : MonoBehaviour
{
    // アルファ値
    private float alfa;

    #region Singleton
    public static FadePanelManager instance;
    public static FadePanelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (FadePanelManager)FindObjectOfType(typeof(FadePanelManager));

                if (instance == null)
                {
                    Debug.LogError(typeof(FadePanelManager) + "is nothing");
                }
            }

            return instance;
        }
    }
    #endregion

    /// <summary>
    /// フェードイン
    /// </summary>
    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    /// <summary>
    /// フェードアウト
    /// </summary>
    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    /// <summary>
    /// フェードインコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeInCoroutine()
    {
        gameObject.SetActive(true);
        while(alfa < 1)
        {
            GetComponent<Image>().color += new Color(0, 0, 0, 0.01f);
            alfa += 0.01f;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
    }

    /// <summary>
    /// フェードアウトコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeOutCoroutine()
    {
        yield return new WaitForSeconds(1f);
        while (alfa < 1)
        {
            GetComponent<Image>().color -= new Color(0, 0, 0, 0.01f);
            alfa -= 0.01f;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}