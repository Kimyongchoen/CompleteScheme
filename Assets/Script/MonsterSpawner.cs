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

            clone.GetComponent<MonsterAttack>().Setup(monster,playerSpawner);//공격 target 검사 및 Attack
            
        }

        playerSpawner.Setup();


        yield return null;
    }

    public void Setup()
    {

        for (int i = 0; i < monsterList.Count; ++i)
        {
            SpawnMonsterHPSlider(MonsterList[i].gameObject);
            ObjectPosition(MonsterList[i].gameObject);
        }
    }

    public void DestroyMonster(Monster monster)
    {
        // 리스트에서 사망하는 몬스터 정보 삭제
        monsterList.Remove(monster);
        //몬스터 오브젝트 삭제
        Destroy(monster.gameObject);
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
    private void ObjectPosition(GameObject monster)
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
        PositionClone.GetComponent<MonsterHPViewer>().Setup(monster.GetComponent<Monster>(),1);
    }
}
