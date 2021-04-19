using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public enum MonsterType { Slime = 1001,  }

    [SerializeField]
    public StartMonsterSpawner[] startMonsterSpawners;//시작할때 몬스터 배치할 정보
    private List<Monster> monsterList; //현재 맵에 존재하는 모든 몬스터의 정보

    //몬스터의 생성과 삭제는 EnemySpawner에서 하기 때문에 Set은 필요없음
    public List<Monster> MonsterList => monsterList;
    

    //private GameObject monsterPrefab; // 몬스터 프리팹
    //private Transform[] monsterPoints; // 배치할 몬스터 위치

    [System.Serializable]
    public struct StartMonsterSpawner // 시작할때 몬스터 배치
    {
        public Transform monsterPoints; // 배치할 몬스터 위치
        public MonsterStats monsterStats; // 몬스터 정보 (이미지, 공격력, 체력, 방어력 등)
    }

    private void Awake()
    {
        //몬스터 리스트 메모리 할당
        monsterList = new List<Monster>();
        //몬스터 생성 코루틴 함수
        StartCoroutine("SpawnMonster");
    }

    private IEnumerator SpawnMonster()
    {
        for (int i = 0; i < startMonsterSpawners.Length ;i++)
        {
            GameObject clone = Instantiate(startMonsterSpawners[i].monsterStats.stats[0].MonsterPrefab);//플레이어 오브젝트 생성
            Monster monster = clone.GetComponent<Monster>();//방금 생성된 몬스터의 Monster 컴포넌트
            monster.Setup(this, startMonsterSpawners[i].monsterPoints);
            monsterList.Add(monster);
        }
        yield return null;
    }
    public void DestroyMonster(Monster monster)
    {
        // 리스트에서 사망하는 몬스터 정보 삭제
        monsterList.Remove(monster);
        //몬스터 오브젝트 삭제
        Destroy(monster.gameObject);
    }
}
