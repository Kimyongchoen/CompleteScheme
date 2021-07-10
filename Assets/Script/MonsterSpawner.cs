using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterSpawner : MonoBehaviour
{
    public enum MonsterType { Slime = 1001,  }

    [SerializeField]
    private PlayerSpawner playerSpawner; // 현재 맵에 존재하는 적 리스트 정보를 얻기 위해
    [SerializeField]
    public StartMonsterSpawner[] startMonsterSpawners;//시작할때 몬스터 배치할 정보
    [SerializeField]
    private GameObject monsterHPSliderPrefab;//몬스터 체력 Slider
    [SerializeField]
    public Transform canvasTransform; // UI를 표현하는 canvas 오브젝트의 transform
    [SerializeField]
    private GameObject ObjectPositionPrefab;//몬스터 Vector Text UI
    [SerializeField]
    private GameObject MonsterNamePrefab;//몬스터 이름 Text UI
    [SerializeField]
    private ObjectDetector ObjectDetector;//타일삭제 용
    
    [SerializeField]
    private DemageTextView demageTextView;//데미지 텍스트

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
        public int number; // 몬스터 number (보통근거리(1), 강한근거리(2), 원거리(3), 버퍼(4), 회복(5), Stage 보스(6) ) 1201 (1=지역 2=type 01은 Number)
    }

    private void Awake()
    {
        GameReset();
    }

    public void GameReset() {
        //몬스터 리스트 메모리 할당
        monsterList = new List<Monster>();
        //몬스터 생성 코루틴 함수
        StartCoroutine("SpawnMonsterCoroutine");
    }

    private IEnumerator SpawnMonsterCoroutine()
    {
        for (int i = 0; i < startMonsterSpawners.Length ;i++)
        {
            int number = 0;

            //number로 stats 몇번째인지 검사
            for (int j = 0; j < startMonsterSpawners[i].monsterStats.stats.Length; j++)
            {
                if (startMonsterSpawners[i].monsterStats.stats[j].number == startMonsterSpawners[i].number) {
                    number = j;
                }
            }
            
            //몬스터 정보
            MonsterStats.Stats monsterstats = startMonsterSpawners[i].monsterStats.stats[number];

            GameObject clone = Instantiate(monsterstats.MonsterPrefab);//플레이어 오브젝트 생성
            Monster monster = clone.GetComponent<Monster>();//방금 생성된 몬스터의 Monster 컴포넌트

            TileManager tileManager = startMonsterSpawners[i].monsterPoints.GetComponent<TileManager>();//TileSelect의 TileManager 컴포넌트
            tileManager.setMonsterFlag(1, startMonsterSpawners[i].number, monster.gameObject); //몬스터 생성 타일에 stats를 1로 세팅
            
            //몬스터 생성자, 몬스터 생성위치, 몬스터 정보 전달
            monster.Setup(this, startMonsterSpawners[i].monsterPoints, monsterstats, demageTextView);
            //몬스터 리스트 저장 
            monsterList.Add(monster);
            //MonsterName(monster.gameObject);// 몬스터 이름 표현

            clone.GetComponent<MonsterAttack>().Setup(monster,playerSpawner);//공격 target 검사 및 Attack
        }

        playerSpawner.Setup();


        yield return null;
    }

    public void SpawnMonster(Transform transform, int MonsterNumber)
    {
        MonsterStats.Stats monsterstats = startMonsterSpawners[0].monsterStats.stats[MonsterNumber];

        GameObject clone = Instantiate(monsterstats.MonsterPrefab);//몬스터 오브젝트 생성
        Monster monster = clone.GetComponent<Monster>();//방금 생성된 몬스터의 Monster 컴포넌트

        //몬스터 생성자, 몬스터 생성위치, 몬스터 정보 전달
        monster.Setup(this, transform, monsterstats, demageTextView);
        //몬스터 리스트 저장 
        monsterList.Add(monster);
        //MonsterName(monster.gameObject);// 몬스터 이름 표현

        clone.GetComponent<MonsterAttack>().Setup(monster, playerSpawner);//공격 target 검사 및 Attack
    }
    public void Setup()
    {

        for (int i = 0; i < monsterList.Count; ++i)
        {
            SpawnMonsterHPSlider(MonsterList[i].gameObject);
            //공격 시작
            monsterList[i].GetComponent<MonsterAttack>().StartAttack();
            MonsterName(MonsterList[i].gameObject);// 몬스터 이름 표현
            //ObjectPosition(MonsterList[i].gameObject);
        }
    }

    public void DestroyMonster(Monster monster)
    {
        bool resetBuffflag = false;

        GoldExpDrop(monster.gold, monster.transform);

        if (monster.monsterStats.buff > 0) //죽는 몬스터가 버프 몬스터라면
        {
            resetBuffflag = true;
        }

        // 리스트에서 사망하는 몬스터 정보 삭제
        monsterList.Remove(monster);
        //몬스터 오브젝트 삭제
        Destroy(monster.gameObject);
        
        if (resetBuffflag)//버프 재설정
        {
            resetBuff();
        }
    }

    private void GoldExpDrop(float gold, Transform monster)
    {
        for (int i = 0; i < gold/10; i++)
        {
            var goldprefab = ObjectPool.getObjectGold();//골드 오브젝트 생성 //Instantiate(GoldPrefab, monster.position, Quaternion.identity);//골드 오브젝트 생성
            goldprefab.transform.position = monster.position;
            goldprefab.Setup(playerSpawner.player.transform);

            var expPrefab = ObjectPool.getObjectExp();//Instantiate(ExpPrefab, monster.transform.position, Quaternion.identity);//경험치 오브젝트 생성
            expPrefab.transform.position = monster.position;
            expPrefab.Setup(playerSpawner.player.transform);
        }

    }

    public void resetBuff()
    {
        //기존 버프 삭제
        for (int i = 0; i < monsterList.Count; ++i)
        {
            monsterList[i].GetComponent<MonsterAttack>().addedDamage = 0;
        }
        //버프 재설정
        for (int i = 0; i < monsterList.Count; ++i)
        {
            if (monsterList[i].monsterStats.buff > 0)
            {
                monsterList[i].GetComponent<MonsterAttack>().BuffToTarget();
            }
        }
    }
    private void SpawnMonsterHPSlider(GameObject monster)
    {

        //적 체력을 나타내는 Slider UI 생성
        GameObject sliderClone = Instantiate(monsterHPSliderPrefab);
        //Slider UI 오브젝트를 parent("Canvas" 오브젝트)의 자식으로 설정
        //Tip.UI는 캔버스의 자식 오브젝트로 설정되어 있어야화면에 보인다.
        sliderClone.transform.SetParent(canvasTransform);
        //가장 앞쪽에 표시 UI에 보이지 않게
        sliderClone.transform.SetAsFirstSibling();
        //계층 설정으로 바뀐 크기를 다시 (1,1,1)로 설정
        sliderClone.transform.localScale = Vector3.one;

        //Slider UI가 쫓아다닐 대상을 본인으로 설정
        sliderClone.GetComponent<PositionAutoSetter>().Setup(monster.transform,0);
        //Slider UI에 자신의 체력 정보를 표시하도록 설정
        sliderClone.GetComponent<MonsterHPViewer>().Setup(monster.GetComponent<Monster>(),0);
    }
/*    private void ObjectPosition(GameObject monster)
    {
        //플래이어 위치을 나타내는 Text UI 생성
        GameObject PositionClone = Instantiate(ObjectPositionPrefab);
        //Text UI 오브젝트를 parent("Canvas" 오브젝트)의 자식으로 설정
        //Tip.UI는 캔버스의 자식 오브젝트로 설정되어 있어야화면에 보인다.
        PositionClone.transform.SetParent(canvasTransform);
        //가장 앞쪽에 표시 UI에 보이지 않게
        PositionClone.transform.SetAsFirstSibling();
        //계층 설정으로 바뀐 크기를 다시 (1,1,1)로 설정
        PositionClone.transform.localScale = Vector3.one;
        //Slider UI가 쫓아다닐 대상을 본인으로 설정
        PositionClone.GetComponent<PositionAutoSetter>().Setup(monster.transform, 1);//type 0 Slider 1 Vector
        //Slider UI에 자신의 체력 정보를 표시하도록 설정
        PositionClone.GetComponent<MonsterHPViewer>().Setup(monster.GetComponent<Monster>(), 1);
    }*/

    private void MonsterName(GameObject monster)
    {
        //플래이어 위치을 나타내는 Text UI 생성
        GameObject PositionClone = Instantiate(MonsterNamePrefab);
        //Text UI 오브젝트를 parent("Canvas" 오브젝트)의 자식으로 설정
        //Tip.UI는 캔버스의 자식 오브젝트로 설정되어 있어야화면에 보인다.
        PositionClone.transform.SetParent(canvasTransform);
        //가장 앞쪽에 표시 UI에 보이지 않게
        PositionClone.transform.SetAsFirstSibling();
        //계층 설정으로 바뀐 크기를 다시 (1,1,1)로 설정
        PositionClone.transform.localScale = Vector3.one;
        //Slider UI가 쫓아다닐 대상을 본인으로 설정
        PositionClone.GetComponent<PositionAutoSetter>().Setup(monster.transform, 2);//type 0 Slider 1 Vector
        //Slider UI에 자신의 체력 정보를 표시하도록 설정
        PositionClone.GetComponent<MonsterHPViewer>().Setup(monster.GetComponent<Monster>(), 2);
    }

    public void MonsterALLClaer()
    {
        //몬스터 삭제
        while (MonsterList.Count>0)
        {
            if (MonsterList[0] != null)
            {
                DestroyMonster(MonsterList[0]);
            }

        }
        ObjectDetector.TileAllClaer();
    }
}
