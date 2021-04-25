using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Transform monsterPoints;        //몬스터 위치 정보
    private Movement2D movement2D;
    MonsterSpawner monsterSpawner;  //몬스터 생성자 정보

    private float maxHP; //최대 체력
    private float currentHP; //현재 체력

    private SpriteRenderer spriteRenderer;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;

    public void Setup(MonsterSpawner monsterSpawner ,Transform monsterPoints)
    {
        movement2D = GetComponent<Movement2D>();
        //몬스터 배치 정보 설정
        this.monsterPoints = monsterPoints;
        //몬스터 생성자 정보
        this.monsterSpawner = monsterSpawner;
        maxHP = monsterSpawner.startMonsterSpawners[0].monsterStats.stats[0].health;
        currentHP = maxHP;

        spriteRenderer = GetComponent<SpriteRenderer>();

        //몬스터의 위치를 설정된 곳으로 이동
        transform.position = monsterPoints.position;
    }

    public void OnDie() //몬스터 삭제
    {
        monsterSpawner.DestroyMonster(this);
    }

    public void OnDemage(int demage) //몬스터 데미지
    {
        currentHP -= demage;
        
        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

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
}
