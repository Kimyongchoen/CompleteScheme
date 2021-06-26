using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DemageTextView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI DemageTextPrefab;//몬스터가 입는 데미지 prefab

    [SerializeField]
    private Transform canvasTransform; // UI를 표현하는 canvas 오브젝트의 transform

    [SerializeField]
    private TextMeshProUGUI RecoveryTextPrefab;//몬스터가 회복하는 데미지 prefab

    private int demage;
    private bool criticalFlag;
    private Transform Demagetransform;
    private Transform Recoverytransform;
    private int Recovery;


    // Update is called once per frame
    public void DemageText(int demage, bool criticalFlag, Transform transform)
    {
        this.demage = demage;
        this.criticalFlag = criticalFlag;
        this.Demagetransform = transform;

        StartCoroutine("DemageTextCoroutine");

    }

    private IEnumerator DemageTextCoroutine()
    {
        int demage = this.demage;
        bool criticalFlag = this.criticalFlag;
        Transform transform = this.Demagetransform;

        //플래이어 위치을 나타내는 Text UI 생성
        TextMeshProUGUI DemageText = Instantiate(DemageTextPrefab);
        //Text UI 오브젝트를 parent("Canvas" 오브젝트)의 자식으로 설정
        //Tip.UI는 캔버스의 자식 오브젝트로 설정되어 있어야화면에 보인다.
        DemageText.transform.SetParent(canvasTransform);
        //가장 앞쪽에 표시 UI에 보이지 않게
        DemageText.transform.SetAsFirstSibling();
        //계층 설정으로 바뀐 크기를 다시 (1,1,1)로 설정
        DemageText.transform.localScale = Vector3.one;

        DemageText.transform.position = transform.position;
        if (demage > 0)
        {
            DemageText.text = demage.ToString();
        }
        else
        {
            DemageText.text = "MISS";
        }


        int count = 0;
        int pox = 1;
        int poy = 2;
        //현재 적의 색사을 color 변수에 저장
        Color color = DemageText.color;

        color.a = 1.0f;
        DemageText.color = color;
        int sizex = 500;
        int sizey = 250;

        while (count < 30)
        {
            count++;
            pox++;

            if (count < 2)
            {
                poy++;
            }
            else if (count < 18)
            {
                //poy++;
            }
            else
            {
                poy--;
            }

            DemageText.transform.position =
            new Vector3(
                DemageText.transform.position.x + ((pox) * 0.0005f),
                DemageText.transform.position.y + ((poy) * 0.005f),
                DemageText.transform.position.z);

            color.a -= 0.015f;
            DemageText.color = color;
            DemageText.fontSize -= 3;
            if (criticalFlag && demage > 0)
            {
                color.b = 0;
                DemageText.color = color;

                sizex -= 10;
                sizey -= 5;
                DemageText.GetComponent<SpriteRenderer>().color = color;
                DemageText.GetComponent<SpriteRenderer>().size = new Vector2(sizex, sizey);
            }
            if (demage <= 0)
            {
                color.g = 0;
                DemageText.color = color;
            }
            yield return new WaitForSeconds(0.03f);
        }

        Destroy(DemageText.gameObject);

        yield return null;

    }
    public void RecoveryText(int Recovery, Transform transform)
    {
        this.Recovery = Recovery;
        this.Recoverytransform = transform;
        StartCoroutine("RecoveryTextView");
    }

    private IEnumerator RecoveryTextView()
    {
        Transform transform = this.Recoverytransform;
        int Recovery = this.Recovery;

        //플래이어 위치을 나타내는 Text UI 생성
        TextMeshProUGUI RecoveryText = Instantiate(RecoveryTextPrefab);
        //Text UI 오브젝트를 parent("Canvas" 오브젝트)의 자식으로 설정
        //Tip.UI는 캔버스의 자식 오브젝트로 설정되어 있어야화면에 보인다.
        RecoveryText.transform.SetParent(canvasTransform);
        //가장 앞쪽에 표시 UI에 보이지 않게
        RecoveryText.transform.SetAsFirstSibling();
        //계층 설정으로 바뀐 크기를 다시 (1,1,1)로 설정
        RecoveryText.transform.localScale = Vector3.one;

        RecoveryText.transform.position = transform.position + (Vector3.up * 0.01f);
        if (Recovery > 0)
        {
            RecoveryText.text = "+ " + Recovery.ToString();
        }

        int count = 20;

        while (count < 30)
        {
            RecoveryText.transform.SetAsFirstSibling();
            count++;
            RecoveryText.transform.position = RecoveryText.transform.position + (Vector3.up * (count * 0.001f));
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(RecoveryText.gameObject);

        yield return null;

    }

}
