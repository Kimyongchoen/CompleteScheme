using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadingSceneColtroller : MonoBehaviour
{
    static string nextScene;

    [SerializeField]
    Image progressBar;

    [SerializeField]
    Image Background1;

    [SerializeField]
    Image Background2;

    [SerializeField]
    Image Background3;

    [SerializeField]
    Image Background4;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadsceneProcess());
    }

    IEnumerator LoadsceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;//로딩 80프로까지만 완료
        
        //로딩화면 랜덤 tip
        int flag = Random.Range(0, 4);
        
        Background1.gameObject.SetActive(false);
        Background2.gameObject.SetActive(false);
        Background3.gameObject.SetActive(false);
        Background4.gameObject.SetActive(false);

        if (flag == 0)
        {
            Background1.gameObject.SetActive(true);
        }
        else if (flag == 1)
        {
            Background2.gameObject.SetActive(true);
        }
        else if (flag == 2)
        {
            Background3.gameObject.SetActive(true);
        }
        else if (flag == 3)
        {
            Background4.gameObject.SetActive(true);
        }

        //80%까지만 완료
        float timer = 0f;

        while (!op.isDone)
        {
            yield return null;
            if(op.progress < 0.8f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.8f, 1f, timer);
                yield return new WaitForSeconds(0.05f);
                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
