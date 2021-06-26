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
    
    public float experience;//몬스터 처치시 얻는 경험치
    public float gold;//몬스터 처치시 얻는 골드
    public MonsterSpawner monsterSpawner;  //몬스터 생성자 정보
    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;
    
    public DemageTextView demageTextView;

    Material material; //몬스터 삭제 효과
    float fade = 1f; //몬스터 삭제 효과

    public void Setup(MonsterSpawner monsterSpawner ,Transform monsterPoints, MonsterStats.Stats monsterStats, DemageTextView demageTextView)
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
        this.demageTextView = demageTextView;//데미지 텍스트
        maxHP = monsterStats.health;//몬스터의 최대 hp
        currentHP = maxHP;

        material = GetComponent<SpriteRenderer>().material;
        spriteRenderer = GetComponent<SpriteRenderer>();

        //몬스터의 위치를 설정된 곳으로 이동
        transform.position = monsterPoints.position;
        OnCreate();
    }
    
    public void OnCreate() //몬스터 생성
    {
        StartCoroutine("CreateMonster");
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

        DemageTextView(demage , criticalFlag);

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
        /*      
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
        */
        while (fade > 0)
        {
            material.SetFloat("_Fade", fade);
            fade -= 0.1f;
            yield return new WaitForSeconds(0.05f);
        }

        monsterSpawner.DestroyMonster(this);

        yield return null;
    }
    private IEnumerator CreateMonster()
    {
        fade = 0;
        while (fade < 1)
        {
            material.SetFloat("_Fade", fade);
            fade += 0.1f;
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }
    public void vampire(int demage)//데미지 흡혈
    {
        currentHP += demage;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }
    private void DemageTextView(int demage,bool criticalFlag)
    {
        demageTextView.DemageText(demage, criticalFlag, this.transform);
    }

    public void RecoveryText(int Recovery)
    {
        this.Recovery = Recovery;
        demageTextView.RecoveryText(Recovery, this.transform);
    }



}
