using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;// 플레이어 프리팹
    [SerializeField]
    private Transform[] wayPoints;//현재 스테이지의 이동경로
    [SerializeField]
    private bool Playing = false; //게임중인지 확인



    // Start is called before the first frame update
    private void Awake()
    {
        //플레이어 이동 코루틴 함수 호출
        //StartCoroutine("SpawnPlayer");
    }

    private IEnumerator SpawnPlayer()
    {
        GameObject clone = Instantiate(playerPrefab);//플레이어 오브젝트 생성
        Player player = clone.GetComponent<Player>();//방금 생성된 플레이어의 Player 컴포넌트

        player.Setup(wayPoints);

        yield return null;// 1초동안 대기
    }
    public void GameStart()
    {
        if (Playing == false)
        {
            //현재 웨이브 시작
            StartCoroutine("SpawnPlayer");
            Playing = true;
        }
    }

}
