using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    private Movement2D movement2D;//이동
    private Transform target;//지정해준 목표
    private int demage;//몬스터의 데미지
    private int addedDamage;//몬스터의 추가 데미지 (버프)
    private bool criticalFlag;//치명타 인지 확인

    public void Setup(Transform target, int demage, bool criticalFlag, int addedDamage)
    {
        movement2D = GetComponent<Movement2D>();

        this.demage = demage; //플레이어의 데미지
        this.target = target; //공격할 몬스터
        this.criticalFlag = criticalFlag; //치명타 인지 확인
        this.addedDamage = addedDamage; //치명타 인지 확인
    }
    
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            //발사체를 Target으로 이동
            Vector3 direction = (target.position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        {
            Destroy(gameObject);//발사체 삭제
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("player")) return; //적이아닌 대상과 부딪히면
        if (collision.transform != target ) return; //현재 target인 적이 아닐때
        collision.GetComponent<Player>().OnDemage(demage, criticalFlag, addedDamage);
        Destroy(gameObject);
    }
}
