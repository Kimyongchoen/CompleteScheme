using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private Movement2D movement2D;//이동
    private Transform target;//공격할 몬스터
    private int demage;//플레이어의 데미지
    private bool criticalFlag;//치명타 인지 확인

    public void Setup(Transform target, int demage, bool criticalFlag)
    {
        movement2D = GetComponent<Movement2D>();
        this.demage = demage; //플레이어의 데미지
        this.target = target; //공격할 몬스터
        this.criticalFlag = criticalFlag; //치명타 인지 확인
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
        if (!collision.CompareTag("monster")) return; //적이아닌 대상과 부딪히면
        if (collision.transform != target ) return; //현재 target인 적이 아닐때
        collision.GetComponent<Monster>().OnDemage(demage, criticalFlag);
        Destroy(gameObject);
    }
}
