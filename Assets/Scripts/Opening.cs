using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    public Image img1, img2;
    public GameObject img1Obj, img2Obj;
    public string nextScene;

    void Start()
    {
        StartCoroutine(FadeImage(img1, true));
        img2Obj.SetActive(false);

        Invoke("CallLoadScene", 4.5f);
    }

    void Update()
    {
        if(img1.color.a <= 0.1f)
        {
            img2Obj.SetActive(true);
            StartCoroutine(FadeImage(img2, false));
        } 
    }

    void CallLoadScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    IEnumerator FadeImage(Image _img, bool fadeAway)
    {
        if (fadeAway == true)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime / 2)
            {
                _img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }

        if (fadeAway == false)
        {
            for (float i = 0; i <= 1; i += Time.deltaTime / 2)
            {
                _img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
}
