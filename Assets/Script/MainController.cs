using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI MainMessageBox;

    [SerializeField]
    private ChangeScene changeScene;

    [SerializeField]
    private Levelinfo levelinfo;

    // Start is called before the first frame update
    void Start()
    {
        
        SetMainMessageBox("Tab To Start");
        //Setting();
    }

    // Update is called once per frame
    void Update()
    {
        //마우스 클릭으로 이동
        if (Input.GetMouseButtonDown(0))
        {
            //구글 로그인 연동

            //닉네임 설정
            StartCoroutine("goScene");
        }
    }

    IEnumerator goScene()
    {
        changeScene.SetStage(-2);//지역선택
        changeScene.ChangeSecen();
        yield return null;
    }
    private void SetMainMessageBox(string msg)
    {
        MainMessageBox.text = msg;
        StartCoroutine(AlphaLerp(1, 0));

    }
    private IEnumerator AlphaLerp(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;
        float lerpTime = 1f;
        while (true)
        {
            //lerpTime 동안 While() 반복문 실행
            currentTime += Time.deltaTime;
            percent = currentTime / lerpTime;

            // Text - TextMeshPro의 폰트 투명도를 start에서 end로 변경
            Color color = MainMessageBox.color;
            color.a = Mathf.Lerp(start, end, percent);
            MainMessageBox.color = color;

            if (percent > 1) currentTime = 0 ; //초기화 반복

            yield return null;
        }

    }

    private void Setting()
    {
        //레벨 세팅 로직 저장용
        float experience = 0;
        float experienceP = 300;
        int Stat = 1;
        int a = 0;

        for (int i = 0; levelinfo.levelInfo.Length > i; i++)
        {
            if (i > 30)
            {
                Stat = 3;
                if (a == 2)
                {
                    a = 0;
                    experienceP += 200;
                }
            }
            else if (i > 18)
            {
                Stat = 2;
                if (a == 2)
                {
                    a = 0;
                    experienceP += 100;
                }
            }
            else
            {
                if (a == 2)
                {
                    a = 0;
                    experienceP += 50;
                }
            }
            a++;
            experience += experienceP;
            levelinfo.levelInfo[i].Stat = Stat;
            levelinfo.levelInfo[i].Level = i + 2;
            levelinfo.levelInfo[i].experience = experience;
        }
    }
}
