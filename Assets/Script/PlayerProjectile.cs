using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private Movement2D movement2D;//이동
    private Transform target;//지정해준 목표
    private int demage;

    public void Setup(Transform target,int demage)
    {
        movement2D = GetComponent<Movement2D>();
        this.demage = demage;
        this.target = target;
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
        collision.GetComponent<Monster>().OnDemage(demage);
        Destroy(gameObject);
    }
}
