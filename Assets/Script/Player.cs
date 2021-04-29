using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int wayPointCount;    //이동 경로 개수
    private Transform[] wayPoints;        //이동 경로 정보
    private int currentIndex = 0; //현재 목표지점 인덱스
    private Movement2D movement2D;       //오브젝트 이동제어

    private float maxHP; //최대 체력
    private float currentHP; //현재 체력

    private SpriteRenderer spriteRenderer;

    public PlayerSpawner playerSpawner;
    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    public void GameStart()
    {
        StartCoroutine("OnMove");//플레이어 이동/목표지점 설정 코루틴 함수 시작
    }

    public void Setup(PlayerSpawner playerSpawner, Transform[] wayPoinsts)
    {
        movement2D = GetComponent<Movement2D>();

        //플레이어 이동 경로 WayPoint 정보 설정
        wayPointCount = wayPoinsts.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoinsts;

        this.playerSpawner = playerSpawner; //플레이어 생성자 정보

        maxHP = playerSpawner.playerStats.stats.health;
        currentHP = maxHP;

        spriteRenderer = GetComponent<SpriteRenderer>();


    }

    private IEnumerator OnMove()
    {
        //다음 이동 방향 설정

        NextMoveTo();

        while (true)
        {
            //플레이어 현재 위치와 목표위치 거리가 0.02 * movement2D.MoveSpeed보다 작을 때 if 조건문 실행
            // Tip. movement2D.MoveSpeed를 곱해주는 이유는 속도가 빠르면 한 프레임 0.02qhek zmrp dnawlrdlrl Eoansdp
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
        }
        else//현재위치가 마지막 wayPoints 이면
        {
            //플레이어 오브젝트 이동정지
            movement2D.MoveStop();

            //테스트를 위한 재시작 가능하게 초기화 추후 삭제
            /*
            Destroy(gameObject);
            this.playerSpawner.Playing = false;
            */
        }
    }

    public void MoveStop()
    {
        movement2D.MoveStop();
    }
    public void MoveStart()
    {
        movement2D.MoveStart();
    }

    public bool GetMoveFlag()
    {
        bool MoveFlag = movement2D.MoveFlag;

        return MoveFlag;
    }

    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(inputX, inputY) * Time.deltaTime);
    }

    public void OnDie() //플래이어 삭제
    {
        StartCoroutine("DieAlphaAnimation");
        //playerSpawner.DestroyMonster(this);
    }

    public void OnDemage(int demage, bool criticalFlag) //플레이어 데미지
    {
        if (demage > 0)
        {
            this.playerSpawner.cameraManager.VibrateForTime(0.2f);//데미지 입을때 카메라 흔들림

            currentHP -= demage;
            StopCoroutine("HitAlphaAnimation");
            StartCoroutine("HitAlphaAnimation");

            if (criticalFlag) //크티티컬 이펙트
            {

            }
            else //일반 이펙트
            {

            }
        }
        else//회피
        {
            StopCoroutine("HitAlphaAnimation");
        }

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

        while (color.a > 0.5)
        {
            color.a -= 0.1f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }

    public void vampire(int demage)//데미지 흡혈
    {
        currentHP += demage;
    }
}
