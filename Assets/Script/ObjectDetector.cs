using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private MonsterSpawner monsterSpawner;
    [SerializeField]
    private GameObject TileSelectfrefab;
    [SerializeField]
    private GameObject RemoveBtnfrefab;
    [SerializeField]
    private TabManager tabManager;
    [SerializeField]
    private CameraManager cameraManager;
    [SerializeField]
    private TileTabManager tileTabManager;
    [SerializeField]
    private MonsterTabManager monsterTabManager;
    [SerializeField]
    private PlayerTabManager playerTabManager;

    [SerializeField]
    private GameObject RecoveryBtn;
    [SerializeField]
    private GameObject AttackDamageUpBtn;
    [SerializeField]
    private GameObject DefenseUpBtn;
    [SerializeField]
    private GameObject MonsterBtn;

    [SerializeField]
    private Text RecoveryCnt;
    [SerializeField]
    private Text AttackDamageUpCnt;
    [SerializeField]
    private Text DefenseUpCnt;
    [SerializeField]
    private Text MonsterCnt;

    [SerializeField]
    private TextMeshProUGUI MainMessageBox;

    [SerializeField]
    private ItemStats ItemStats;

    [SerializeField]
    private Transform[] TileList;//현재 생성되어있는 모든 타일
    //[SerializeField]
    //private TowerDataViewer towerDataViewer;
    private TileManager tileManager;

    [SerializeField]
    private Stage10 Stage10;//스테이지 정보

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;
    private Transform hitTransform = null;//마우스 픽킹으로 선택한 오프젝트 임시저장
    private GameObject TileSelectView = null;
    private GameObject RemoveBtn = null;
    private Transform TileSelect = null;

    private void Awake()
    {
        // "MainCamera" 태그를 가지고 있는 오브젝트 탐색 후 Camera 컴포넌트 정보 전달
        // GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); 와 동일
        mainCamera = Camera.main;
        monsterTabManager.SetMonsterInfomation(1101);

    }

    // Update is called once per frame
    private void Update()
    {
        SetBtnCount();//버튼 카운트 수정

        //마우스가 UI에 머물러 있을 때는 아래 코드가 실행되지 않도록 함
        if (EventSystem.current.IsPointerOverGameObject() == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                /*
                if (TileSelectView != null)
                {
                    Destroy(TileSelectView.gameObject);
                    TileSelect = null;
                }*/
            }
            return;
        }

        if (cameraManager.getGameStartFlag()) //게임시작하면 기능 정지
        {
            if (TileSelectView != null)//게임시작하면 선택된 Tile삭제
            {
                Destroy(TileSelectView.gameObject);
                TileSelect = null;
            }
            if (RemoveBtn != null)//게임시작하면 회수버튼 삭제
            {
                Destroy(RemoveBtn.gameObject);
            }
            return;
        }

        //마우스 왼쪽 버튼 눌렀을때
        if (Input.GetMouseButtonDown(0))
        {
            //카메라 위치에서 화면의 마우스 위치를 관통하는 광선 생성
            // ray.origin : 광선의 시작위치 (=카메라 위치)
            // ray.direction : 광선의 진행방향
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // 2D 모니터를 통해 3D월드의 오브젝트를 마우스로 선택하는 방법
            // 광선에 부딪히는 오브젝트를 검출해서 hit에 저장
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                hitTransform = hit.transform;

                //광선에 부딪힌 오브젝트의 태그가 "Tile" 이면
                if (hit.transform.CompareTag("Tile"))
                {
                    if (TileSelectView == null)
                    {
                        TileSelectView = Instantiate(TileSelectfrefab);//Tile 선택 오브젝트 생성
                    }
                    if (RemoveBtn == null)
                    {
                        RemoveBtn = Instantiate(RemoveBtnfrefab);//제거버튼 오브젝트 생성
                    }
                    for (int i = 0; TileList.Length > i; i++)
                    {
                        if (TileList[i].transform.position == hit.transform.position)
                        {
                            TileSelectView.transform.position = TileList[i].transform.position;//타일리스트에 있는 위치로 이동
                            cameraManager.Setup(TileSelectView, false);//타일로 카메라 이동 
                            tileManager = TileList[i].GetComponent<TileManager>();//TileList의 TileManager 컴포넌트

                            tabManager.TabClick(1);
                            TileSelect = TileList[i];

                            if (tileManager.getMonsterFlag() == 0)//아무것도 없다면 (0)
                            {
                                if (ItemStats.RecoveryCnt > 0) //회복 버튼 활성화 회복이 있는 경우
                                {
                                    btnColorChange(RecoveryBtn, 1.0f);
                                }
                                if (ItemStats.AttackDamageUpCnt > 0) //공격력 증가 버튼 활성화 / 공격력 증가 버프가 있는 경우
                                {
                                    btnColorChange(AttackDamageUpBtn, 1.0f);
                                }
                                if (ItemStats.DefenseUpCnt > 0) //방어력 증가 버튼 활성화 / 방어력 증가 버프가 있는 경우
                                {
                                    btnColorChange(DefenseUpBtn, 1.0f);
                                }
                                if (ItemStats.MonsterCnt > 0) //몬스터 생성 버튼 활성화 / 몬스터는 무제한
                                {
                                    btnColorChange(MonsterBtn, 1.0f);
                                }

                                if (RemoveBtn != null)
                                {
                                    Destroy(RemoveBtn.gameObject);
                                }
                            }
                            else if (tileManager.getMonsterFlag() == 1)//처음 생성되어있는 몬스터
                            {
                                tabManager.TabClick(2);
                                monsterTabManager.SetMonsterInfomation(tileManager.getnumber());
                                if (RemoveBtn != null)
                                {
                                    Destroy(RemoveBtn.gameObject);
                                }
                            }
                            else if (tileManager.getMonsterFlag() == 2)//사용자가 추가한 몬스터
                            {
                                tabManager.TabClick(1);
                                SetRemoveBtn();//타일리스트에 있는 위치로 이동
                            }
                            else if (tileManager.getMonsterFlag() == 3)//사용자가 추가한 버프 회복
                            {
                                tabManager.TabClick(1);
                                SetRemoveBtn();//타일리스트에 있는 위치로 이동
                            }
                            else if (tileManager.getMonsterFlag() == 4)//Player
                            {
                                //플레이어 정보
                                tabManager.TabClick(0);
                                playerTabManager.SetPlayerInfomation(1);//기사 1
                                if (RemoveBtn != null)
                                {
                                    Destroy(RemoveBtn.gameObject);
                                }
                            }

                            if (tileManager.getMonsterFlag() != 0)//타일에 무언가 있다면 (! 0 )
                            {
                                //회복 버튼 비활성화 회복이 있는 경우
                                btnColorChange(RecoveryBtn, 0.5f);
                                //공격력 증가 버튼 비활성화
                                btnColorChange(AttackDamageUpBtn, 0.5f);
                                //방어력 증가 버튼 비활성화
                                btnColorChange(DefenseUpBtn, 0.5f);
                                //몬스터 생성 버튼 비활성화
                                btnColorChange(MonsterBtn, 0.5f);

                                //TileSelect 초기화
                                //TileSelect = null;
                            }
                        }
                    }
                }
                else if (hit.transform.CompareTag("RemoveBtn"))
                {
                    Remove();//선택된 타일 삭제
                }
            }
            else
            {
                if (TileSelectView != null)
                {
                    Destroy(TileSelectView.gameObject);
                    TileSelect = null;
                }
                if (RemoveBtn != null)
                {
                    Destroy(RemoveBtn.gameObject);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //마우스를 눌렀을 때 선택한 오브젝트가 없거나 선택한 오브젝트가 타일이 아니면
            if (hitTransform == null || hitTransform.CompareTag("Tile") == false)
            {

            }
        }
    }
    public void Remove()
    {
        if (TileSelect != null)//선택된 타일이 있을경우에만 삭제
        {
            tileManager = TileSelect.GetComponent<TileManager>();//TileList의 TileManager 컴포넌트

            // 2 배치한 몬스터 3 회복 / 버프(1 회복 2 공격력 증가 3 방어력 증가)

            if (tileManager.getMonsterFlag() == 2)//사용자가 추가한 몬스터
            {
                Destroy(tileManager.getgameObject());//제거
                ItemStats.MonsterCnt += 1;
            }
            else if (tileManager.getMonsterFlag() == 3)//사용자가 추가한 버프 회복
            {
                if (tileManager.getnumber() == 1)//회복
                {
                    Destroy(tileManager.getgameObject());//제거
                    ItemStats.RecoveryCnt += 1;
                }
                else if (tileManager.getnumber() == 2)//공격력 증가
                {
                    Destroy(tileManager.getgameObject()); //제거
                    ItemStats.AttackDamageUpCnt += 1;
                }
                else if (tileManager.getnumber() == 3)//방어력 증가
                {
                    Destroy(tileManager.getgameObject());  //제거
                    ItemStats.DefenseUpCnt += 1;
                }
            }
            else if (tileManager.getMonsterFlag() == 0)//비어 있다면
            {
                SetMainMessageBox("회수 할 몬스터나 버프가 없습니다");
                return;
            }
            else
            {
                SetMainMessageBox("회수 할 수 없습니다");
                return;
            }

            /*
            if (TileSelectView != null)
            {
                Destroy(TileSelectView.gameObject);
                TileSelect = null;
            }
            */
            if (RemoveBtn != null)
            {
                Destroy(RemoveBtn.gameObject);
            }

            tileManager.setMonsterFlag(0, 0, null);//아무것도 없음으로 세팅

            //회복 버튼 활성화 회복이 있는 경우
            btnColorChange(RecoveryBtn, 1f);
            //공격력 증가 버튼 활성화
            btnColorChange(AttackDamageUpBtn, 1f);
            //방어력 증가 버튼 활성화
            btnColorChange(DefenseUpBtn, 1f);
            //몬스터 생성 버튼 활성화
            btnColorChange(MonsterBtn, 1f);
        }
        else
        {
            SetMainMessageBox("선택된 타일이 없습니다");
        }
    }
    //회복 배치
    public void SetRecovery()
    {
        if (ItemStats.RecoveryCnt > 0) //회복이 있는 경우
        {
            if (TileSelect != null)
            {
                if (tileManager.getMonsterFlag() == 0)//타일이 비어있다면
                { 
                    if (RemoveBtn == null)
                    {
                        RemoveBtn = Instantiate(RemoveBtnfrefab);//제거버튼 오브젝트 생성
                    }
                    SetRemoveBtn();
                    tileTabManager.SetRecovery(TileSelect);
                    ItemStats.RecoveryCnt -= 1;
                }
                else
                {
                    SetMainMessageBox("이미 몬스터나 버프가 있습니다");
                }
            }
            else
            {
                SetMainMessageBox("선택된 타일이 없습니다");
            }
        }
        else
        {
            SetMainMessageBox("배치할 회복 버프가 부족합니다");
            
        }
    }

    //공격력 증가 배치
    public void SetAttackDamageUp()
    {
        if (ItemStats.AttackDamageUpCnt > 0) //공격력 증가 버튼 활성화 / 공격력 증가 버프가 있는 경우
        {
            if (TileSelect != null)
            {
                if (tileManager.getMonsterFlag() == 0)//타일이 비어있다면
                { 
                    if (RemoveBtn == null)
                    {
                        RemoveBtn = Instantiate(RemoveBtnfrefab);//제거버튼 오브젝트 생성
                    }
                    SetRemoveBtn();
                    tileTabManager.SetAttackDamageUp(TileSelect);
                    ItemStats.AttackDamageUpCnt -= 1;
                }
                else
                {
                    SetMainMessageBox("이미 몬스터나 버프가 있습니다");
                }
            }
            else
            {
                SetMainMessageBox("선택된 타일이 없습니다");
            }
        }
        else
        {
            SetMainMessageBox("배치할 공력력 증가 버프가 부족합니다");
        }
    }
    //방어력 증가 배치
    public void SetDefenseUp()
    {
        if (ItemStats.DefenseUpCnt > 0) //방어력 증가 버튼 활성화 / 방어력 증가 버프가 있는 경우
        {
            if (TileSelect != null)
            {
                if (tileManager.getMonsterFlag() == 0)//타일이 비어있다면
                { 
                    if (RemoveBtn == null)
                    {
                        RemoveBtn = Instantiate(RemoveBtnfrefab);//제거버튼 오브젝트 생성
                    }
                    SetRemoveBtn();
                    tileTabManager.SetDefenseUp(TileSelect);
                    ItemStats.DefenseUpCnt -= 1;
                }
                else
                {
                    SetMainMessageBox("이미 몬스터나 버프가 있습니다");
                }
            }
            else
            {
                SetMainMessageBox("선택된 타일이 없습니다");
            }
        }
        else
        {
            SetMainMessageBox("배치할 방어력 증가 버프가 부족합니다");
        }
    }
    //몬스터 배치
    public void SetMonster()
    {
        if (ItemStats.MonsterCnt > 0) //몬스터 생성 버튼 활성화 / 몬스터는 무제한
        {
            if (TileSelect != null)
            {
                if (tileManager.getMonsterFlag() == 0) //타일이 비어있다면
                {
                    if (RemoveBtn == null)
                    {
                        RemoveBtn = Instantiate(RemoveBtnfrefab);//제거버튼 오브젝트 생성
                    }
                    SetRemoveBtn();
                    tileTabManager.SetMonster(TileSelect);
                    ItemStats.MonsterCnt -= 1;
                }
                else
                {
                    SetMainMessageBox("이미 몬스터나 버프가 있습니다");
                }
            }
            else
            {
                SetMainMessageBox("선택된 타일이 없습니다");
            }

        }
        else
        {
            SetMainMessageBox("배치할 몬스터가 부족합니다");
        }

    }

    public void btnColorChange(GameObject btngameObject, float colorf)
    {
        Color color = btngameObject.GetComponentInChildren<Image>().color;
        color.a = colorf;
        btngameObject.GetComponentInChildren<Image>().color = color;
    }

    //회수 버튼 생성
    public void SetRemoveBtn()
    {
        RemoveBtn.transform.position = TileSelectView.transform.position + Vector3.down * 0.5f;
        RemoveBtn.transform.position = new Vector3(RemoveBtn.transform.position.x, RemoveBtn.transform.position.y, 0);

        //회복 버튼 비활성화
        btnColorChange(RecoveryBtn, 0.5f);
        //공격력 증가 버튼 비활성화
        btnColorChange(AttackDamageUpBtn, 0.5f);
        //방어력 증가 버튼 비활성화
        btnColorChange(DefenseUpBtn, 0.5f);
        //몬스터 생성 버튼 비활성화
        btnColorChange(MonsterBtn, 0.5f);
    }

    public void SetBtnCount()
    {
        RecoveryCnt.text = ItemStats.RecoveryCnt.ToString();
        AttackDamageUpCnt.text = ItemStats.AttackDamageUpCnt.ToString();
        DefenseUpCnt.text = ItemStats.DefenseUpCnt.ToString();
        MonsterCnt.text = ItemStats.MonsterCnt.ToString();
    }

    public void RandomMonsterChange()
    {
        for (int i = 0; TileList.Length > i; i++)
        {
            tileManager = TileList[i].GetComponent<TileManager>();//TileList의 TileManager 컴포넌트

            if (tileManager.getMonsterFlag() == 2)//배치한 몬스터이면
            {
                
                int RandomNumber = Random.Range(Stage10.stage[0].MonsterNumberMin, Stage10.stage[0].MonsterNumberMax);// 0 ~ 4 까지 랜덤한 숫자 전달
                //몬스터를 생성하는 SpawnMonster()호출
                monsterSpawner.SpawnMonster(tileManager.transform, RandomNumber);
                Destroy(tileManager.getgameObject());//제거
            }
        }
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
    /*
     * file : ObjectDetector.cs
     * 
     * 
     * 
     * 
     */
}
