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

    // Start is called before the first frame update
    void Start()
    {
        SetMainMessageBox("Tab To Start");
    }

    // Update is called once per frame
    void Update()
    {
        //마우스 클릭으로 이동
        if (Input.GetMouseButtonDown(0))
        {
            //구글 로그인 연동

            //닉네임 설정

            changeScene.SetStage(1000);//지역선택
            changeScene.ChangeSecen();
        }
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
}
