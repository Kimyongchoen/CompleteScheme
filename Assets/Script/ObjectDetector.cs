using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private MonsterSpawner monsterSpawner;
    [SerializeField]
    private GameObject TileSelectfrefab;
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
    private Transform[] TileList;//현재 생성되어있는 모든 타일
    //[SerializeField]
    //private TowerDataViewer towerDataViewer;
    private TileManager tileManager;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;
    private Transform hitTransform = null;//마우스 픽킹으로 선택한 오프젝트 임시저장
    private GameObject TileSelect = null;

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
        //마우스가 UI에 머물러 있을 때는 아래 코드가 실행되지 않도록 함
        if (EventSystem.current.IsPointerOverGameObject() == true)
        {
            if (Input.GetMouseButtonDown(0)) 
            { 
                if (TileSelect != null)
                Destroy(TileSelect.gameObject);
            }
            return;
        }

        if (cameraManager.getGameStartFlag()) //게임시작하면 기능 정지
        {
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
                    if (TileSelect == null)
                    {
                        TileSelect = Instantiate(TileSelectfrefab);//Tile 선택 오브젝트 생성
                    }

                    for (int i=0; TileList.Length > i; i++)
                    {
                        if (TileList[i].transform.position == hit.transform.position)
                        {
                            TileSelect.transform.position = TileList[i].transform.position;//타일리스트에 있는 위치로 이동
                            

                            cameraManager.Setup(TileSelect, false);//타일로 카메라 이동 

                            tileManager = TileList[i].GetComponent<TileManager>();//TileSelect의 TileManager 컴포넌트
                            if (tileManager.getMonsterFlag()==0)//아무것도 없다면 (0)
                            {
                                tabManager.TabClick(1);

                                //Tile 관련 script 작성 에서 버튼 활성화

                                //몬스터 생성 버튼 활성화 / 몬스터는 무제한
                                //공격력 증가 버튼 활성화 / 공격력 증가 버프가 있는 경우
                                //방어력 증가 버튼 활성화 / 방어력 증가 버프가 있는 경우 
                                //회복 버튼 활성화 회복이 있는 경우
                            }
                            else if (tileManager.getMonsterFlag() == 1)//처음 생성되어있는 몬스터
                            {
                                tabManager.TabClick(2);
                                monsterTabManager.SetMonsterInfomation(tileManager.getnumber());
                            }
                            else if (tileManager.getMonsterFlag() == 2)//사용자가 추가한 몬스터
                            {

                            }
                            else if (tileManager.getMonsterFlag() == 3)//사용자가 버프 회복
                            {

                            }
                            else if (tileManager.getMonsterFlag() == 4)//Player
                            {
                                tabManager.TabClick(0);
                                playerTabManager.SetPlayerInfomation(1);//기사 1
                            }
                        }
                    }
                    
                    //몬스터를 생성하는 SpawnTower()호출
                    //monsterSpawner.SpawnMonster(hit.transform);
                }

            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //마우스를 눌렀을 때 선택한 오브젝트가 없거나 선택한 오브젝트가 타일이 아니면
            
            //Debug.Log("hitTransform.CompareTag(Tile)==" + hitTransform.CompareTag("Tile"));

            if (hitTransform == null || hitTransform.CompareTag("Tile") == false)
            {
                if (TileSelect != null)
                    Destroy(TileSelect.gameObject);

                //타워 정보 패널을 비활성화 한다
                //towerDataViewer.OffPanel();
            }

            hitTransform = null;
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
