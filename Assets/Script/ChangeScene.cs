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

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip AudioOnBtn;

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
        StartCoroutine("goScene");
    }

    IEnumerator goScene()
    {
        audioSource.clip = AudioOnBtn;
        audioSource.Play();

        

        switch (Stage)
        {
            case -2: //처음실행
                yield return new WaitForSeconds(0.8f);
                LoadingSceneColtroller.LoadScene("AreaSelect");
                //SceneManager.LoadScene("AreaSelect");
                break;
            case 1000: //지역선택
                yield return new WaitForSeconds(0.3f);
                //LoadingSceneColtroller.LoadScene("AreaSelect");
                SceneManager.LoadScene("AreaSelect");
                break;
            case 1001: //캐릭터 스탯 
                yield return new WaitForSeconds(0.3f);
                //LoadingSceneColtroller.LoadScene("PlayerStats");
                SceneManager.LoadScene("PlayerStats");
                break;
            case 1002: //상점
                yield return new WaitForSeconds(0.3f);
                //LoadingSceneColtroller.LoadScene("Shop"); 
                SceneManager.LoadScene("Shop");
                break;
            case -1: //StageSelect
                yield return new WaitForSeconds(0.3f);
                //LoadingSceneColtroller.LoadScene("StageSelect");
                SceneManager.LoadScene("StageSelect");
                break;
            case 0: //Stage1
                yield return new WaitForSeconds(0.3f);
                LoadingSceneColtroller.LoadScene("GameScene");
                //SceneManager.LoadScene("GameScene");
                break;
                
            case 1: //Stage2
                yield return new WaitForSeconds(0.3f);
                LoadingSceneColtroller.LoadScene("GameScene2");
                break;
                /*
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
