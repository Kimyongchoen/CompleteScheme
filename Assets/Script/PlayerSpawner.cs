using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum WeaponState { SearchTarget = 0, AttackToTarget }
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] wayPoints;//현재 스테이지의 이동경로
    [SerializeField]
    public PlayerStats playerStatsScriptableObject; // 플레이어 정보 ( 공격력, 체력, 방어력 등)
    [SerializeField]
    private MonsterSpawner monsterSpawner; // 현재 맵에 존재하는 적 리스트 정보를 얻기 위해
    [SerializeField]
    private GameObject playerHPSliderPrefab;//플래이어 체력 Slider
    [SerializeField]
    public Transform canvasTransform; // UI를 표현하는 canvas 오브젝트의 transform
    [SerializeField]
    private GameObject ObjectPositionPrefab;//플래이어 좌표 값
    [SerializeField]
    public CameraManager cameraManager;
    [SerializeField]
    public Image imageScreenRed;//플레이어 hp20%미만일때 표시
    [SerializeField]
    private TabManager tabManager;//플레이어 탭
    [SerializeField]
    private ObjectDetector objectDetector;//몬스터 랜덤생성
    
    [SerializeField]
    private int StageNumber;//현재 스테이지 번호

    [SerializeField]
    private GameObject GameStartBtn;//게임 시작 버튼
    [SerializeField]
    private GameObject StageSelectBtn;//게임 시작전 스테이지 이동 버튼
    [SerializeField]
    private GameObject ButtonExitBtn;//게임 재시작 버튼
    [SerializeField]
    private GameObject StageSelect2Btn;//게임 종료후 스테이지 이동 버튼
    [SerializeField]
    private GameObject NextStageBtn;//게임 종료후 다음 스테이지 이동 버튼

    

    [SerializeField]
    public PlayerTabManager playerTabManager;//플레이어 정보 표시

    [SerializeField]
    private Stage10 stage10;//스테이지 정보
    [SerializeField]
    private ItemStats itemStats;//아이템 정보

    [SerializeField]
    private ChangeScene changeScene;//스테이지 변경

    public PlayerStats.Stats playerStats; // 플레이어 정보 ( 공격력, 체력, 방어력 등)

    private Transform tileTransform;
    public bool Playing = false; //게임중인지 확인
    public Player player;
    private GameObject clone;

    public float gold; //게임 중 gold
    public float experience; //게임 중 경험치


    [SerializeField]
    private TextMeshProUGUI MainMessageBox;

    [SerializeField]
    private Levelinfo levelinfo;

    [SerializeField]
    public PlayerStat playerStat;

    public void Setup()
    {


        this.playerStats = playerStatsScriptableObject.stats[0];//플레이어 타입 1 (기사)

        GameObject clone = Instantiate(playerStats.PlayerPrefab);//플레이어 오브젝트 생성
        Player player = clone.GetComponent<Player>();//방금 생성된 플레이어의 Player 컴포넌트
        this.clone = clone;
        this.player = player;
        player.transform.position = wayPoints[0].position; //플레이어의 위치를 첫번째 wayPoint 위치로 설정
        player.Setup(this, wayPoints);

        TileManager tileManager = wayPoints[0].GetComponent<TileManager>();//TileSelect의 TileManager 컴포넌트
        tileManager.setMonsterFlag(4, 0, player.gameObject); //몬스터 생성 타일에 stats를 1로 세팅

    }
    public void GameStart()//게임 시작버튼
    {
        if (!Playing)
        {
            GameStartBtn.SetActive(false);
            StageSelectBtn.SetActive(false);

            StartCoroutine("SpawnPlayer");
            //현재 웨이브 시작
            Playing = true;
        }
    }
    public void GameEnd(bool Claer)
    {
        Playing = false;
        cameraManager.Setup(null, true);

        //msgbox 초기화
        MainMessageBox.text = "";
        Color color = MainMessageBox.color;
        color.a = 1f;
        MainMessageBox.color = color;

        if (Claer)//게임 클리어
        {
            StageClear();
        }
        else //플레이어 행동불가
        {
            PlayerDie();
        }
    }

    public void GameReset()//게임 리셋 버튼 플레이어 사망시
    {
        if (!Playing)//게임중 일때 리셋 가능
        {
            Color color = imageScreenRed.color;
            color.a = 0f;
            color.r = 255f;
            imageScreenRed.color = color;

            MainMessageBox.text = "";

            Destroy(player.gameObject);//플레이어 삭제

            monsterSpawner.MonsterALLClaer();//몬스터 삭제

            //스타트 버튼 활성화 리셋 버튼 비활성화
            ButtonExitBtn.SetActive(false);
            StageSelect2Btn.SetActive(false);
            StageSelectBtn.SetActive(true);
            GameStartBtn.SetActive(true);

            playerTabManager.SetPlayerInfomation(1);//플레이어정보 리셋

            //게임 초기 상태로 리셋
            monsterSpawner.GameReset();


            cameraManager.Setup(null, false);

        }
        else
        {
            SetMainMessageBox("게임이 진행 중입니다");
        }
    }

    private void StageClear() //게임 완료
    {
        MainMessageBox.text = "지역 클리어";

        //플레이어 정보 저장 기존 점수보다 높다면 갱신
        if ( gold + experience > stage10.stage[0].gold + stage10.stage[0].experience)
        {
            playerStatsScriptableObject.stats[0].gold -= stage10.stage[0].gold;
            playerStatsScriptableObject.stats[0].goldMax -= stage10.stage[0].gold;//획득한 총 골드 추후 초기화에 사용
            playerStatsScriptableObject.stats[0].experience -= stage10.stage[0].experience;

            stage10.stage[0].gold = gold;
            stage10.stage[0].experience = experience;

            playerStatsScriptableObject.stats[0].gold += stage10.stage[0].gold;
            playerStatsScriptableObject.stats[0].goldMax += stage10.stage[0].gold;//획득한 총 골드 추후 초기화에 사용
            playerStatsScriptableObject.stats[0].experience += stage10.stage[0].experience;

        }
        else
        {
            MainMessageBox.text = "지역 클리어\n 기존 획득한 경험치와 골드보다\n획득한 경험치와 골드가 적습니다\n기존 경험치와 골드가 유지 됩니다";
        }

        int LevelUp = 0;
        //레벨 계산 & 보너스 스텟 저장
        for (int i = 0; levelinfo.levelInfo.Length > i; i++)
        {
            if (playerStatsScriptableObject.stats[0].experience < levelinfo.levelInfo[0].experience) break; //레벨1 경험치라면 정지

            if (playerStatsScriptableObject.stats[0].experience < levelinfo.levelInfo[i].experience) { //경험치에 따라서 레벨 변경
                LevelUp = levelinfo.levelInfo[i-1].Level - playerStatsScriptableObject.stats[0].level; //레벨 업 한 수
                playerStatsScriptableObject.stats[0].level = levelinfo.levelInfo[i-1].Level;
                break;
            }
        }

        if (playerStatsScriptableObject.stats[0].level > playerStat.Level) // 레벨업 했다면
        {
            //레벨업 한만큼 보너스 스텟 추가..
            for (int i = 0; LevelUp > i; i++)
            {
                playerStat.BonusStat += levelinfo.levelInfo[ playerStatsScriptableObject.stats[0].level - 2 - i ].Stat;
            }
            playerStat.Level = playerStatsScriptableObject.stats[0].level;
        }

        itemStats.AttackDamageUp = 0;
        itemStats.DefenseUp = 0;

        Color color = imageScreenRed.color;
        color.a = 0.4f;
        color.r = 0f;
        imageScreenRed.color = color;

        //if (StageNumber < 10)
            //NextStageBtn.SetActive(true);

        StageSelect2Btn.SetActive(true);

        

    }
    private void PlayerDie()
    {
        Color color = imageScreenRed.color;
        color.a = 0.4f;
        color.r = 0f;
        imageScreenRed.color = color;

        ButtonExitBtn.SetActive(true);
        StageSelect2Btn.SetActive(true);

        MainMessageBox.text = "게임 오버";

        //스테이지 정보 초기화 ( 버프 회수, 경험치, 골드 회수 )
        gold = 0;
        experience = 0;
        itemStats.AttackDamageUp = 0;
        itemStats.DefenseUp = 0;
    }

    private IEnumerator SpawnPlayer()
    {

        objectDetector.RandomMonsterChange();
        yield return new WaitForSeconds(1f);

        tabManager.TabClick(4);

        MainCameraColtroll(clone);
        //카메라 이동

        yield return new WaitForSeconds(1f);
        //플레이어 Slider 표시
        SpawnPlayerHPSlider(clone);
        //ObjectPosition(clone);//좌표값 표시

        yield return new WaitForSeconds(0.3f);
        //몬스터 Slider , Position 표시
        monsterSpawner.Setup();

        //플레이어 이동 시작
        player.GameStart();
        
        clone.GetComponent<PlayerAttack>().Setup(player, monsterSpawner);//공격 target 검사 및 Attack PlayerAttack에 MonsterSpawner 정보 전달

        yield return null;
    }

    public void DestroyPlayer(Player player)
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
        PositionClone.GetComponent<PositionAutoSetter>().Setup(player.transform, 1);//type 0 Slider 1 Vector
        //Slider UI에 자신의 체력 정보를 표시하도록 설정
        PositionClone.GetComponent<PlayerHPViewer>().Setup(player.GetComponent<Player>(), 1);
    }

    private void MainCameraColtroll(GameObject player)
    {
        //clone.GetComponent<PlayerAttack>().Setup(player, monsterSpawner);

        cameraManager.Setup(player,true);
    }
    private void SetMainMessageBox(string msg)
    {
        MainMessageBox.text = msg;
        StartCoroutine(AlphaLerp(1, 0));

    }
    private IEnumerator AlphaLerp(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;
        float lerpTime = 0.5f;

        while (percent < 1)
        {
            //lerpTime 동안 While()반복문 실행
            currentTime += Time.deltaTime;
            percent = currentTime / lerpTime;

            // Text - TextMeshPro의 폰트 투명도를 start에서 end로 변경
            Color color = MainMessageBox.color;
            color.a = Mathf.Lerp(start, end, percent);
            MainMessageBox.color = color;

            yield return null;
        }



    }
    public void NextStage()
    {
        if (StageNumber<10)
        {
            changeScene.SetStage(++StageNumber);
            changeScene.ChangeSecen();
        }
    }
}