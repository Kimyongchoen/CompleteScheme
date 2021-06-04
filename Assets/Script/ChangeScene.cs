using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private GameObject Questionpanel;
    [SerializeField]
    private Stage10 stage10;
    private int Stage = 0;

    public void ChangeSecenBtn(int Stage)
    {
        this.Stage = Stage;

        if (Stage < 0)
        {
            ChangeSecen();//스테이지 선택으로 이동시 메시지 없이 이동
        }
        else
        {
            if (stage10.stage[Stage].gold + stage10.stage[Stage].experience > 0)
            {
                Questionpanel.SetActive(true);
            }
            else
            {
                ChangeSecen();//저장된 골드 경험치가 없다면
            }
        }
    }

    public void SetStage(int Stage)
    {
        this.Stage = Stage;
    }

    public void ChangeSecen()
    {
        switch (Stage)
        {
            case -1: //StageSelect
                SceneManager.LoadScene("StageSelect");
                break;
            case 0: //Stage1
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

    public void Cancel()
    {
        Questionpanel.SetActive(false);
        Questionpanel.GetComponent<QuestionManager>().setStageName(null);
    }
}
