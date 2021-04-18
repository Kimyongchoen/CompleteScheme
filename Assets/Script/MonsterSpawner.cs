using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public enum MonsterType { Slime = 1001,  }

    [SerializeField]
    private StartMonsterSpawner[] startMonsterSpawners;//시작할때 몬스터 배치할 정보

    

    //private GameObject monsterPrefab; // 몬스터 프리팹
    //private Transform[] monsterPoints; // 배치할 몬스터 위치

    [System.Serializable]
    public struct StartMonsterSpawner // 시작할때 몬스터 배치
    {
        public Transform monsterPoints; // 배치할 몬스터 위치
        public MonsterStats monsterStats; // 플레이어 정보 (이미지, 공격력, 체력, 방어력 등)
    }

    private void Awake()
    {
        StartCoroutine("SpawnMonster");
    }

    private IEnumerator SpawnMonster()
    {
        for (int i = 0; i < startMonsterSpawners.Length ;i++)
        {
            GameObject clone = Instantiate(startMonsterSpawners[i].monsterStats.stats[0].MonsterPrefab);//플레이어 오브젝트 생성
            Monster monster = clone.GetComponent<Monster>();//방금 생성된 몬스터의 Monster 컴포넌트
            monster.Setup(startMonsterSpawners[i].monsterPoints);
        }
        yield return null;
    }
}
