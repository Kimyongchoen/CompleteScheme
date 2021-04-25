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
    [SerializeField]
    private GameObject playerHPSliderPrefab;//플래이어 체력 Slider
    [SerializeField]
    private Transform canvasTransform; // UI를 표현하는 canvas 오브젝트의 transform
    [SerializeField]
    private GameObject ObjectPositionPrefab;//플래이어 좌표 값
    [SerializeField]
    private CameraManager cameraManager;

    private Transform tileTransform;
    public bool Playing = false; //게임중인지 확인
    public Player player;
    private GameObject clone;

    public void Setup()
    {
        GameObject clone = Instantiate(playerStats.PlyerPrefab);//플레이어 오브젝트 생성
        Player player = clone.GetComponent<Player>();//방금 생성된 플레이어의 Player 컴포넌트
        this.clone = clone;
        this.player = player;
        player.transform.position = wayPoints[0].position; //플레이어의 위치를 첫번째 wayPoint 위치로 설정
        player.Setup(this, wayPoints);
        clone.GetComponent<PlayerAttack>().Setup(player, monsterSpawner);//공격 target 검사 및 Attack PlayerAttack에 MonsterSpawner 정보 전달


    }

    private IEnumerator SpawnPlayer()
    {

        MainCameraColtroll(clone);
        //카메라 체력바 위치바 세팅 1초후 이동
        yield return new WaitForSeconds(1f);
        

        //플레이어 Slider , Position 표시
        SpawnPlayerHPSlider(clone);
        ObjectPosition(clone);

        yield return new WaitForSeconds(0.3f);
        //몬스터 Slider , Position 표시
        monsterSpawner.Setup();

        //플레이어 이동 시작
        player.GameStart();

        yield return null;

    }
    public void GameStart()
    {
        if (Playing == false)
        {
            StartCoroutine("SpawnPlayer");
            //현재 웨이브 시작
            Playing = true;
        }
    }
    public void DestroyMonster(Player player)
    {
        //플레이어 오브젝트 삭제
        Destroy(player.gameObject);
    }

    private void SpawnPlayerHPSlider(GameObject Player)
    {

        //적 체력을 나타내는 Slider UI 생성
        GameObject sliderClone = Instantiate(playerHPSliderPrefab);
        //Slider UI 오브젝트를 parent("Canvas" 오브젝트)의 자식으로 설정
        //Tip.UI는 캔버스의 자식 오브젝트로 설정되어 있어야화면에 보인다.
        sliderClone.transform.SetParent(canvasTransform);
        //가장 앞쪽에 표시 UI에 보이지 않게
        sliderClone.transform.SetAsFirstSibling();
        //계층 설정으로 바뀐 크기를 다시 (1,1,1)로 설정
        sliderClone.transform.localScale = Vector3.one;

        //Slider UI가 쫓아다닐 대상을 본인으로 설정
        sliderClone.GetComponent<PositionAutoSetter>().Setup(Player.transform, 0);
        //Slider UI에 자신의 체력 정보를 표시하도록 설정
        sliderClone.GetComponent<PlayerHPViewer>().Setup(Player.GetComponent<Player>(), 0);
    }
    private void ObjectPosition(GameObject player)
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
        PositionClone.GetComponent<PositionAutoSetter>().Setup(player.transform,1);//type 0 Slider 1 Vector
        //Slider UI에 자신의 체력 정보를 표시하도록 설정
        PositionClone.GetComponent<PlayerHPViewer>().Setup(player.GetComponent<Player>(),1);
    }

    private void MainCameraColtroll(GameObject player)
    {
        //clone.GetComponent<PlayerAttack>().Setup(player, monsterSpawner);
        
        cameraManager.Setup(player);
    }
}