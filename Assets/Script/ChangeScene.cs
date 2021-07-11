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
        //-1 스테이지선택
        //1000 지역선택
        //1001 캐릭터 스탯
        //1002 상점
        if (Stage < 0 || Stage > 999)
        {
            ChangeSecen();//스테이지 선택 or 지역선택 으로 이동시 메시지 없이 이동
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
            case -2: //처음실행
                LoadingSceneColtroller.LoadScene("AreaSelect");
                //SceneManager.LoadScene("AreaSelect");
                break;
            case 1000: //지역선택
                //LoadingSceneColtroller.LoadScene("AreaSelect");
                SceneManager.LoadScene("AreaSelect");
                break;
            case 1001: //캐릭터 스탯 
                //LoadingSceneColtroller.LoadScene("PlayerStats");
                SceneManager.LoadScene("PlayerStats");
                break;
            case 1002: //상점
                //LoadingSceneColtroller.LoadScene("Shop"); 
                SceneManager.LoadScene("Shop");
                break;
            case -1: //StageSelect
                //LoadingSceneColtroller.LoadScene("StageSelect");
                SceneManager.LoadScene("StageSelect");
                break;
            case 0: //Stage1
                LoadingSceneColtroller.LoadScene("GameScene");
                //SceneManager.LoadScene("GameScene");
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
