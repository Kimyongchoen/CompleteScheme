using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Monster : MonoBehaviour
{
    private Transform monsterPoints;        //몬스터 위치 정보
    private Movement2D movement2D;
    
    private float maxHP; //최대 체력
    public float currentHP; //현재 체력
    private int demage;//입는 데미지
    private int Recovery;//회복하는 데미지
    private bool criticalFlag;//크리티컬데미지인지확인
    private SpriteRenderer spriteRenderer;
    private Transform canvasTransform; // UI를 표현하는 canvas 오브젝트의 transform
    public MonsterStats.Stats monsterStats;///몬스터 스텟 정보
    [SerializeField]
    private TextMeshProUGUI DemageTextPrefab;//몬스터가 입는 데미지 prefab
    [SerializeField]
    private TextMeshProUGUI RecoveryTextPrefab;//몬스터가 회복하는 데미지 prefab
    
    public float experience;//몬스터 처치시 얻는 경험치
    public float gold;//몬스터 처치시 얻는 골드
    public MonsterSpawner monsterSpawner;  //몬스터 생성자 정보
    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    public void Setup(MonsterSpawner monsterSpawner ,Transform monsterPoints, MonsterStats.Stats monsterStats)
    {
        movement2D = GetComponent<Movement2D>();
        //몬스터 배치 정보 설정
        this.monsterPoints = monsterPoints;
        //몬스터 생성자 정보
        this.monsterSpawner = monsterSpawner;
        this.canvasTransform = monsterSpawner.canvasTransform;
        this.monsterStats = monsterStats;//몬스터 정보 저장
        this.experience = monsterStats.experience;//몬스터 처치시 얻는 경험치
        this.gold = monsterStats.gold;//몬스터 처치시 얻는 골드
        maxHP = monsterStats.health;//몬스터의 최대 hp
        currentHP = maxHP;
        
        spriteRenderer = GetComponent<SpriteRenderer>();

        //몬스터의 위치를 설정된 곳으로 이동
        transform.position = monsterPoints.position;
    }

    public void OnDie() //몬스터 삭제
    {
        StartCoroutine("DieAlphaAnimation");
    }

    public void OnDemage(int demage,bool criticalFlag) //플레이어 데미지, 크리티컬 확인
    {
        this.demage = demage;

        if(demage > 0)
        {
            currentHP -= demage;
            this.criticalFlag = criticalFlag;
            
            StopCoroutine("HitAlphaAnimation");
            StartCoroutine("HitAlphaAnimation");
        }
        else//회피
        {
            StopCoroutine("HitAlphaAnimation");
        }

        StartCoroutine("DemageTextView");

        if (currentHP <= 0)
        {
            
            OnDie();
        }

    }
    private IEnumerator HitAlphaAnimation()
    {
        //현재 적의 색사을 color 변수에 저장
        Color color = spriteRenderer.color;

        //적의 투명도를 40%로 설정
        color.a = 0.4f;
        spriteRenderer.color = color;


        //0.05초 동안 대기
        yield return new WaitForSeconds(0.05f);

        //적의 투명도를 100%로 설정
        color.a = 1.0f;
        spriteRenderer.color = color;




    }
    private IEnumerator DieAlphaAnimation()
    {
        //현재 적의 색사을 color 변수에 저장
        Color color = spriteRenderer.color;
        
        color.a = 1.0f;
        spriteRenderer.color = color;

        while (color.a > 0)
        {
            color.a -= 0.08f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.05f);
        }

        monsterSpawner.DestroyMonster(this);
        
        yield return null;
    }
    public void vampire(int demage)//데미지 흡혈
    {
        currentHP += demage;
    }
    private IEnumerator DemageTextView()
    {
        //플래이어 위치을 나타내는 Text UI 생성
        TextMeshProUGUI DemageText = Instantiate(DemageTextPrefab);
        //Text UI 오브젝트를 parent("Canvas" 오브젝트)의 자식으로 설정
        //Tip.UI는 캔버스의 자식 오브젝트로 설정되어 있어야화면에 보인다.
        DemageText.transform.SetParent(canvasTransform);
        //가장 앞쪽에 표시 UI에 보이지 않게
        DemageText.transform.SetAsFirstSibling();
        //계층 설정으로 바뀐 크기를 다시 (1,1,1)로 설정
        DemageText.transform.localScale = Vector3.one;

        DemageText.transform.position = this.transform.position + (Vector3.up* 0.45f);
        if (demage > 0)
        {
            if (criticalFlag)
            {
                DemageText.text = "CRITICAL : "+demage.ToString();
            }
            else
            {
                DemageText.text = demage.ToString();
            }
        }
        else
        {
            DemageText.text = "MISS";
        }

        
        int count = 0;
        int pox = 0;
        int poy = 0;
        //현재 적의 색사을 color 변수에 저장
        Color color = DemageText.color;

        color.a = 1.0f;
        DemageText.color = color;

        while (count < 10)
        {
            count++;
            pox++;
            
            if (count < 3)
            {
                poy++;
            }
            else if(count < 6)
            {
                //poy++;
            }
            else
            {
                poy--;
            }

            DemageText.transform.position =  
                new Vector3 (
                    this.transform.position.x + ((pox + 1) * 0.04f), 
                    this.transform.position.y + ((poy + 2) * 0.09f),
                    this.transform.position.z );
            
            color.a -= 0.04f;
            DemageText.color = color;
            DemageText.fontSize -= 7;
            yield return new WaitForSeconds(0.05f);
        }
        
        Destroy(DemageText.gameObject);

        yield return null;

    }
    public void RecoveryText(int Recovery)
    {
        this.Recovery = Recovery;
        StartCoroutine("RecoveryTextView");
    }

    private IEnumerator RecoveryTextView()
    {
        //플래이어 위치을 나타내는 Text UI 생성
        TextMeshProUGUI RecoveryText = Instantiate(RecoveryTextPrefab);
        //Text UI 오브젝트를 parent("Canvas" 오브젝트)의 자식으로 설정
        //Tip.UI는 캔버스의 자식 오브젝트로 설정되어 있어야화면에 보인다.
        RecoveryText.transform.SetParent(canvasTransform);
        //가장 앞쪽에 표시 UI에 보이지 않게
        RecoveryText.transform.SetAsFirstSibling();
        //계층 설정으로 바뀐 크기를 다시 (1,1,1)로 설정
        RecoveryText.transform.localScale = Vector3.one;

        RecoveryText.transform.position = this.transform.position + (Vector3.up * 0.45f);
        if (Recovery > 0)
        {
            RecoveryText.text = "+ " + Recovery.ToString();
        }

        int count = 20;

        while (count < 30)
        {
            count++;
            RecoveryText.transform.position = this.transform.position + (Vector3.up * (count * 0.01f));
            yield return new WaitForSeconds(0.05f);
        }

        Destroy(RecoveryText.gameObject);

        yield return null;

    }


}
