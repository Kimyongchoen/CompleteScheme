using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    private int wayPointCount;    //이동 경로 개수
    private Transform[] wayPoints;        //이동 경로 정보
    private int currentIndex = 0; //현재 목표지점 인덱스
    private Movement2D movement2D;       //오브젝트 이동제어

    private float maxHP; //최대 체력
    private float currentHP; //현재 체력

    private SpriteRenderer spriteRenderer;
    private int demage;//입는 데미지
    private int addedDamage;//입는 추가 데미지 (버프)
    private bool criticalFlag;//크리티컬인지 확인
    private Transform canvasTransform; // UI를 표현하는 canvas 오브젝트의 transform
    
    private float attackSpeed; //공격속도

    private int attackDamageMax; //공격력 최대값
    private int attackDamageMin; //공격력 최소값
    private int defense; //방어력

    private float experienceBonus;//경험치 획득 %
    private float goldBonus;//골드 획득 %
    [SerializeField]
    private TextMeshProUGUI DemageTextPrefab;//몬스터가 입는 데미지 prefab

    private PlayerTabManager playerTabManager;

    private Image imageScreenRed;//몬스터가 입는 데미지 prefab

    public PlayerSpawner playerSpawner;

    private Animator animator; // 케릭터 애니메이션

    private Vector2 PlayerPosition; //케릭터 이동
    float DirX = 0f; //케릭터 이동
    float DirY = 0f; //케릭터 이동

    bool GameEnd = false; //게임종료
    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    public ItemStats ItemStats;//아이템정보

    Material material; //플레이어 버프 효과
    
    [SerializeField]
    private Stage10 Stage10;//스테이지 정보

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip AudioAttack;//공격소리

    [SerializeField]
    private AudioClip AudioWalk;//걷는소리
    

    public void GameStart()
    {
        StartCoroutine("OnMove");//플레이어 이동/목표지점 설정 코루틴 함수 시작
        StartCoroutine("BuffEffect1"); //공격력 버프 이팩트
        StartCoroutine("BuffEffect2"); //방어력 버프 이팩트
        StartCoroutine("OnAudio"); //게임소리
    }

    public void Setup(PlayerSpawner playerSpawner, Transform[] wayPoinsts)
    {
        movement2D = GetComponent<Movement2D>();
        material = GetComponent<SpriteRenderer>().material;

        //플레이어 이동 경로 WayPoint 정보 설정
        wayPointCount = wayPoinsts.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoinsts;

        this.playerSpawner = playerSpawner; //플레이어 생성자 정보
        this.canvasTransform = playerSpawner.canvasTransform;

        this.experienceBonus = playerSpawner.playerStats.experienceBonus * 0.01f;//몬스터 처치시 얻는 경험치 %로 변경
        this.goldBonus = playerSpawner.playerStats.goldBonus * 0.01f;//몬스터 처치시 얻는 골드 %로 변경

        this.imageScreenRed = playerSpawner.imageScreenRed;

        this.playerTabManager = playerSpawner.playerTabManager;

        this.attackDamageMax = playerSpawner.playerStatsScriptableObject.stats[0].attackDamageMax;
        this.attackDamageMin = playerSpawner.playerStatsScriptableObject.stats[0].attackDamageMin;
        this.defense = playerSpawner.playerStatsScriptableObject.stats[0].defense;
        
        this.audioSource = playerSpawner.audioSource;
        
        this.attackSpeed = playerSpawner.playerStatsScriptableObject.stats[0].attackSpeed;
        
        if (attackSpeed!=1)
        {
            attackSpeed = 2f;
        }
        
        int Maxhealth = playerSpawner.playerStats.Maxhealth + (playerSpawner.playerStat.Maxhealth * playerSpawner.playerStat.MaxhealthUp);
        int health = playerSpawner.playerStats.health + (playerSpawner.playerStat.Maxhealth * playerSpawner.playerStat.MaxhealthUp);
        
        //투구 체력은 강화할때 적용필요      
        if (playerSpawner.playerStats.Hat >= 0)//투구 강화 따른 체력 UP 투구가 없으면 -1
        {
            Maxhealth += ItemStats.Hat[playerSpawner.playerStats.Hat];
            health += ItemStats.Hat[playerSpawner.playerStats.Hat];
        }

        maxHP = Maxhealth;
        currentHP = health;

        spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>(); //케릭터 애니메이션

        animator.speed = attackSpeed;//케릭터 공격속도 애니메이션에 적용

        PlayerPosition = new Vector2(transform.position.x, transform.position.y); //플레이어의 현재위치

        
    }


    private IEnumerator OnMove()
    {
        //다음 이동 방향 설정

        NextMoveTo();

        while (true)
        {
            //플레이어 현재 위치와 목표위치 거리가 0.02 * movement2D.MoveSpeed보다 작을 때 if 조건문 실행
            // Tip. movement2D.MoveSpeed를 곱해주는 이유는 속도가 빠르면 한 프레임 0.02보다 크게 움직이기 때문에
            //if 조건문에 걸리지 않고 경로를 탈주하는 오브젝트가 발생할수 있다.
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                //다음 이동 방향 설정
                NextMoveTo();

            }
            yield return null;
        }
    }
    private void NextMoveTo()
    {
        // 아직 이동할 WayPoints가 남아있다면
        if (currentIndex < wayPointCount - 1)
        {
            //플레이어의 위치를 정확하게 목표위치로 설정
            transform.position = wayPoints[currentIndex].position;
            //이동 방향 설정 => 다음 목표지점 (wayPoints)
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);

            MoveStart();
        }
        else//현재위치가 마지막 wayPoints 이면
        {
            MoveStop();
            
            if (!GameEnd)//1번만 실행
            {
                GameEnd = true;
                //플레이어 오브젝트 이동정지
                //테스트를 위한 재시작 가능하게 초기화 추후 삭제
                /*
                Destroy(gameObject);
                */
                playerSpawner.GameEnd(true);
            }
        }
    }

    private IEnumerator OnAudio()
    {
        while (true)
        {
            if (animator.GetBool("Walking"))
            {
                audioSource.clip = AudioWalk;//걷는소리
                audioSource.Play();
                yield return new WaitForSeconds(0.3f);
            }
            else if (animator.GetBool("Attacking"))
            {
                audioSource.clip = AudioAttack;//공격소리
                audioSource.Play();
                yield return new WaitForSeconds(1f);
            }
            else
            {
                audioSource.clip = AudioAttack;//공격소리
                audioSource.Stop();
                yield return new WaitForSeconds(1f);
            }
        }
    }

    public void MoveStart()
    {
        animator.SetBool("Attacking", false);
        movement2D.MoveStart();
        animator.SetBool("Walking", true);
        PlayerPosition = new Vector2(transform.position.x, transform.position.y);
    }

    public void MoveStop()
    {
        movement2D.MoveStop();
        animator.SetBool("Walking", false);
        if (!GameEnd)
        {
            animator.SetBool("Attacking", true);
        }
        else
        {
            animator.SetBool("Attacking", false);
        }
        PlayerPosition = new Vector2(transform.position.x, transform.position.y);
    }

    public bool GetMoveFlag()
    {
        bool MoveFlag = movement2D.MoveFlag;

        return MoveFlag;
    }

    private void Update()
    {
        /* 키보드로 Player 이동
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(inputX, inputY) * Time.deltaTime);
        */

        if (PlayerPosition.x < transform.position.x)
            DirX = 1f;
        if (PlayerPosition.x > transform.position.x)
            DirX = -1f;
        if (PlayerPosition.y < transform.position.y)
            DirY = 1f;
        if (PlayerPosition.y > transform.position.y)
            DirY = -1f;

        animator.SetFloat("DirX", DirX);
        animator.SetFloat("DirY", DirY);
    }

    private IEnumerator BuffEffect1()
    {
        int a = 0;
        bool flag = true;
        float fade = 0.9f; //플레이어 버프 효과
        while (true)
        {
            if (ItemStats.AttackDamageUp > 0)
            {
                material.SetFloat("_BuffFade1", fade);

                a++;

                if (a >= 7)
                {
                    if (flag)
                        flag = false;
                    else
                        flag = true;

                    a = 0;
                }

                if (flag)
                    fade -= 0.1f;
                else
                    fade += 0.1f;
            }
            else
            {
                material.SetFloat("_BuffFade1", 0f);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator BuffEffect2()
    {
        int a = 0;
        bool flag = true;
        float fade = 0.9f; //플레이어 버프 효과
        while (true)
        {
            if (ItemStats.DefenseUp > 0)
            {
                material.SetFloat("_BuffFade2", fade);

                a++;

                if (a >= 7)
                {
                    if (flag)
                        flag = false;
                    else
                        flag = true;

                    a = 0;
                }

                if (flag)
                    fade -= 0.1f;
                else
                    fade += 0.1f;

            }
            else
            {
                material.SetFloat("_BuffFade2", 0f);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator BuffEffect3()
    {
        int j = 0;
        int a = 0;
        bool flag = true;
        float fade = 0.9f; //플레이어 버프 효과
        while (j < 15)
        {
            material.SetFloat("_BuffFade3", fade);

            a++;

            if (a >= 7)
            {
                if (flag)
                    flag = false;
                else
                    flag = true;

                a = 0;
            }

            if (flag)
                fade -= 0.1f;
            else
                fade += 0.1f;
            
            j++;

            yield return new WaitForSeconds(0.1f);
        }

        material.SetFloat("_BuffFade3", 0f);
        yield return null;
    }

    public void OnDie() //플래이어 삭제
    {
        GameEnd = true;
        MoveStop();
        StartCoroutine("DieAlphaAnimation");
        //playerSpawner.DestroyMonster(this);
    }

    public void OnDemage(int demage, bool criticalFlag, int addedDamage) //플레이어 데미지
    {
        this.demage = demage;

        if (demage > 0)
        {
            this.playerSpawner.cameraManager.VibrateForTime(0.2f);//데미지 입을때 카메라 흔들림

            this.criticalFlag = criticalFlag;
            this.addedDamage = addedDamage;

            currentHP -= demage;
            StopCoroutine("HitAlphaAnimation");
            StartCoroutine("HitAlphaAnimation");

        }
        else//회피
        {
            StopCoroutine("HitAlphaAnimation");
        }

        StartCoroutine("DemageTextView");

        if (maxHP * 0.2f > currentHP)
        {
            StartCoroutine("ImageScreenRed");
        }


        if (currentHP <= 0)
        {
            currentHP = 0; //체력의 최소값은 0
            OnDie();
        }

        playerTabManager.SetPlayerInfomation(1, (int)currentHP);//기사 1
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

        while (color.a > 0.5)
        {
            color.a -= 0.1f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.1f);
        }

        playerSpawner.GameEnd(false);

        yield return null;
    }

    private IEnumerator ImageScreenRed()
    {
        Color color = imageScreenRed.color;
        color.a = 0.4f;
        imageScreenRed.color = color;

        while (color.a > 0.0f)
        {
            color.a -= Time.deltaTime;
            imageScreenRed.color = color;

            yield return null;
        }


    }

    public void vampire(int demage)//데미지 흡혈
    {
        currentHP += demage;

        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        playerTabManager.SetPlayerInfomation(1, (int)currentHP);//기사 1
    }

    public void GoldExperience(float gold, float experience)//경험치 골드 획득
    {
        if (playerSpawner.Playing)
        {
            if (goldBonus > 0)
                gold += gold * goldBonus;
            if (experienceBonus > 0)
                experience += experience * experienceBonus;

            playerSpawner.gold += gold;
            playerSpawner.experience += gold;

            playerTabManager.SetPlayerInfomation(1, (int)currentHP);//기사 1
        }
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

        DemageText.transform.position = this.transform.position + (Vector3.up * 0.1f);
        if (demage > 0)
        {
            if (criticalFlag)
            {
                DemageText.text = "CRITICAL : " + demage.ToString();
            }
            else
            {
                DemageText.text = demage.ToString();
            }

            if (addedDamage > 0) //추가 데미지가 있으면 표기
            {
                DemageText.text = (demage - addedDamage).ToString() + " + " + addedDamage.ToString();
            }

        }
        else
        {
            DemageText.text = "MISS";
        }

        int count = 20;
        while (count < 30)
        {
            count++;
            DemageText.transform.position = this.transform.position + (Vector3.up * (count * 0.01f));
            yield return new WaitForSeconds(0.05f);
        }

        Destroy(DemageText.gameObject);

        yield return null;

    }


    private void OnTriggerEnter2D(Collider2D collision)//캐릭터 충돌 제어
    {
        if (collision.CompareTag("buff"))//충돌된 대상이 버프 일 경우
        {
            int BuffStats = collision.GetComponent<Buff>().getBuffStats();
            float BuffValue = collision.GetComponent<Buff>().getBuffValue();

            if (BuffStats == 1)// 1 회복 
            {
                currentHP += maxHP * BuffValue; //최대 체력의 BuffValue % 회복

                if (currentHP > maxHP)
                    currentHP = maxHP;
                
                StartCoroutine("BuffEffect3");
                Destroy(collision.gameObject);
            }
            else if (BuffStats == 2)//2 공격력 증가
            {
                StartCoroutine("AttackDamageUp");
                Destroy(collision.gameObject);
            }
            else if (BuffStats == 3)//3 방어력 증가
            {
                StartCoroutine("DefenseUp");
                Destroy(collision.gameObject);
            }


        }

    }

    private IEnumerator AttackDamageUp()
    {
        int attackDamageUp = playerSpawner.playerStatsScriptableObject.stats[0].attackDamageMin + playerSpawner.playerStatsScriptableObject.stats[0].attackDamageMax / 2;
        
        if (attackDamageUp <= 4)//공격력 평균이 4보다 작으면
        {
            ItemStats.AttackDamageUp += 1;
        }
        else
        {
            ItemStats.AttackDamageUp += (int)((float)attackDamageUp * 1.3f); //30 % 공격력 증가
        }

        playerTabManager.SetPlayerInfomation(1, (int)currentHP);//기사 1 Player info 최신화

        yield return new WaitForSeconds(10f);//10초동안 공격력 UP
        

        if (attackDamageUp <= 4) //공격력 평균이 5보다 작으면
        {
            ItemStats.AttackDamageUp -= 1;
        }
        else
        {
            ItemStats.AttackDamageUp -= (int)((float)attackDamageUp * 1.3f); //30 % 공격력 증가
        }
        playerTabManager.SetPlayerInfomation(1, (int)currentHP);//기사 1 Player info 최신화

        yield return null;
    }
    private IEnumerator DefenseUp()
    {

        int defense = playerSpawner.playerStatsScriptableObject.stats[0].defense;
        
        if (defense <= 4)//방어력이 5보다 작으면
        {
            
            ItemStats.DefenseUp += 1; //방어력 1증가
        }
        else
        {
            ItemStats.DefenseUp += (int)((float)defense * 1.3f); ; //30% 방어력 증가
        }
        playerTabManager.SetPlayerInfomation(1, (int)currentHP);//기사 1 Player info 최신화

        yield return new WaitForSeconds(10f);//10초동안 방어력 UP

        if (defense <= 4)//방어력이 5보다 작으면
        {

            ItemStats.DefenseUp -= 1; //방어력 1감소
        }
        else
        {
            ItemStats.DefenseUp -= (int)((float)defense * 1.3f); ; //30% 방어력 감소
        }

        playerTabManager.SetPlayerInfomation(1, (int)currentHP);//기사 1 Player info 최신화
        
        yield return null;
    }
}

