using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{

    private Movement2D movement2D;//이동
    private Transform target;//지정해준 목표
    private Transform Goldtransform;//지정해준 목표

    public void Setup(Transform target)
    {
        movement2D = GetComponent<Movement2D>();
        movement2D.MoveStart();

        this.target = target; //공격할 몬스터

        StartCoroutine("DropGold");
        StartCoroutine("StopGold");
    }

    private IEnumerator DropGold()
    {

        if (target != null)
        {
            //발사체를 Target으로 이동
            Vector3 direction = (
                transform.position
                + (Vector3.up * Random.Range(0f, 0.5f))
                + (Vector3.down * Random.Range(0f, 0.5f))
                + (Vector3.left * Random.Range(0f, 0.5f))
                + (Vector3.right * Random.Range(0f, 0.5f))
                - transform.position
                ).normalized;

            movement2D.MoveTo(direction);
            movement2D.ChangeMoveSpeed(2f);
            yield return new WaitForSeconds(Random.Range(0f, 0.3f));
            movement2D.ChangeMoveSpeed(0.8f);
            movement2D.MoveStop();

        }
        else
        {
            DestroyExp();//발사체 반환
        }
        //1초 만큼 대기
        yield return new WaitForSeconds(0.001f);
    }

    private IEnumerator StopGold()
    {
        yield return new WaitForSeconds(1f);
        movement2D.MoveStart();

        while (true)
        {
            if (target != null)
            {
                //발사체를 Target으로 이동
                Vector3 direction = (target.position - transform.position).normalized;
                movement2D.MoveTo(direction);
            }
            else
            {
                DestroyExp();//발사체 반환
            }
            //1초 만큼 대기
            yield return new WaitForSeconds(0.001f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("player")) return; //적이아닌 대상과 부딪히면
        if (collision.transform != target) return; //현재 target인 적이 아닐때
        DestroyExp();//발사체 반환
    }

    private void DestroyExp()
    {
        ObjectPool.ReturnObjectExp(this);
    }
}
