using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeSecenBtn()
    {
        switch (this.gameObject.name)
        {
            case "StageSelectBtn":
                SceneManager.LoadScene("StageSelect");
                break;
            case "StageSelect2Btn":
                SceneManager.LoadScene("StageSelect");
                break;
            case "Stage1":
                SceneManager.LoadScene("GameScene");
                break;
            /*
            case "Stage2":
                SceneManager.LoadScene("GameScene2");
                break;
            case "Stage3":
                SceneManager.LoadScene("GameScene3");
                break;
            case "Stage4":
                SceneManager.LoadScene("GameScene4");
                break;
            case "Stage5":
                SceneManager.LoadScene("GameScene5");
                break;
            case "Stage6":
                SceneManager.LoadScene("GameScene6");
                break;
            case "Stage7":
                SceneManager.LoadScene("GameScene7");
                break;
            case "Stage8":
                SceneManager.LoadScene("GameScene8");
                break;
            case "Stage9":
                SceneManager.LoadScene("GameScene9");
                break;
            case "Stage10":
                SceneManager.LoadScene("GameScene10");
                break;
            */
        }
    }
}
