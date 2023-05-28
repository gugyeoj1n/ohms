using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeAnim : MonoBehaviour
{
    public GameObject Panel;

    public void TitleFadeOut()
    {
        StartCoroutine(FadeOut(2));
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    public void BackToTitle()
    {
        StartCoroutine(FadeOut(1));
    }

    IEnumerator FadeOut(int sceneNum)
    {
        Panel.SetActive(true);
        Image image = Panel.GetComponent<Image>();
        float fadeCount = 0;
        while(fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
        SceneManager.LoadScene(sceneNum);
    }

    IEnumerator FadeIn()
    {
        Panel.SetActive(true);
        Image image = Panel.GetComponent<Image>();
        float fadeCount = 1.0f;
        while(fadeCount > 0f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
        Panel.SetActive(false);
    }
}