using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] wayPoints;//현재 스테이지의 이동경로
    [SerializeField]
    private PlayerStats playerStats; // 플레이어 정보 ( 공격력, 체력, 방어력 등)
    
    private bool Playing = false; //게임중인지 확인

    // Start is called before the first frame update
    private void Awake()
    {
        //시작할때 몬스터 배치
        //monsterSpawner.StartMonster();
    }

    private IEnumerator SpawnPlayer()
    {
        GameObject clone = Instantiate(playerStats.PlyerPrefab);//플레이어 오브젝트 생성
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
