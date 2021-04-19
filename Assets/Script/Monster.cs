using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Transform monsterPoints;        //몬스터 위치 정보
    private Movement2D movement2D;
    MonsterSpawner monsterSpawner;  //몬스터 생성자 정보
    private int monterHealth = 0;
    public void Setup(MonsterSpawner monsterSpawner ,Transform monsterPoints)
    {
        movement2D = GetComponent<Movement2D>();
        //몬스터 배치 정보 설정
        this.monsterPoints = monsterPoints;
        //몬스터 생성자 정보
        this.monsterSpawner = monsterSpawner;
        monterHealth = monsterSpawner.startMonsterSpawners[0].monsterStats.stats[0].health;

        //몬스터의 위치를 설정된 곳으로 이동
        transform.position = monsterPoints.position;
    }

    public void OnDie() //몬스터 삭제
    {
        monsterSpawner.DestroyMonster(this);
    }

    public void OnDemage(int demage) //몬스터 삭제
    {
        monterHealth -= demage;

        if (monterHealth <= 0)
        {
            OnDie();
        }
    }
}
