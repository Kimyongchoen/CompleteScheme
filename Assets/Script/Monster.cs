﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private int wayPointCount;    //이동 경로 개수
    private Transform wayPoints;        //이동 경로 정보
    private int currentIndex = 0; //현재 목표지점 인덱스
    private Movement2D movement2D;       //오브젝트 이동제어

    public void Setup(Transform wayPoinsts)
    {
        movement2D = GetComponent<Movement2D>();

        //플레이어 이동 경로 WayPoint 정보 설정
        wayPointCount = 1;
        this.wayPoints = wayPoinsts;

        //플레이어의 위치를 첫번째 wayPoint 위치로 설정
        transform.position = wayPoinsts.position;

        //플레이어 이동/목표지점 설정 코루틴 함수 시작
        StartCoroutine("OnMove");
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
            if (Vector3.Distance(transform.position, wayPoints.position) < 0.02f * movement2D.MoveSpeed)
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
            transform.position = wayPoints.position;
            //이동 방향 설정 => 다음 목표지점 (wayPoints)
            currentIndex++;
            Vector3 direction = (wayPoints.position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else//현재위치가 마지막 wayPoints 이면
        {
            //플레이어 오브젝트 삭제
            //Destroy(gameObject);
            movement2D.MoveStop();
        }
    }

}
