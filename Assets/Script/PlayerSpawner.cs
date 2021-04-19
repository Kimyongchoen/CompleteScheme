using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState { SearchTarget = 0, AttackToTarget }
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] wayPoints;//현재 스테이지의 이동경로
    [SerializeField]
    public PlayerStats playerStats; // 플레이어 정보 ( 공격력, 체력, 방어력 등)
    [SerializeField]
    private MonsterSpawner monsterSpawner; // 현재 맵에 존재하는 적 리스트 정보를 얻기 위해

    private Transform tileTransform;
    public bool Playing = false; //게임중인지 확인

    private IEnumerator SpawnPlayer()
    {
        GameObject clone = Instantiate(playerStats.PlyerPrefab);//플레이어 오브젝트 생성
        
        //타워 무기에 MonsterSpawner 정보 전달
        Player player = clone.GetComponent<Player>();//방금 생성된 플레이어의 Player 컴포넌트
        clone.GetComponent<PlayerAttack>().Setup(player, monsterSpawner);//공격 target 검사 및 Attack

        player.Setup(this,wayPoints);

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
