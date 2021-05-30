using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour
{
    public float moveSpeed;
    private GameObject target = null;
    private Vector3 TargetPosition;
    [SerializeField]
    private Camera MainCamera;

    //마우스 클릭으로 이동
    private Vector3 MouseStart;
    private Vector3 derp;
    private bool MoveFlag = false;
    private bool GameStartFlag = false;

    //카메라 흔들기
    public float ShakeAmount = 1f;
    float ShakeTime = 0f;
    Vector3 initialPosition;

    //리셋 카메라
    private float ResetMainCameraOrthographicSize;

    private void Start()
    {
        ResetMainCameraOrthographicSize = MainCamera.orthographicSize;
    }
    public void Setup(GameObject target, bool GameStartFlag)
    {
        this.target = target;
        this.GameStartFlag = GameStartFlag;
        //게임 시작 전으로 되면
        if (target==null && !GameStartFlag)
        {
            MainCamera.orthographicSize = ResetMainCameraOrthographicSize;
            MainCamera.transform.position = new Vector3(0,1,-10);
        }
    }

    public bool getGameStartFlag()
    {
        return GameStartFlag;
    }
    // Update is called once per frame
    private void Update()
    {
        /*        Vector2?[] touchPrevPos = { null, null };
                Vector2 touchPrevVector;
                float touchPrevDist = 1f ;
                if (Input.touchCount == 0)
                {
                    touchPrevPos[0] = null;
                    touchPrevPos[1] = null;
                } 
                else if (Input.touchCount == 1) //터치가 1개
                {
                    //순차적 터치의 예외처리 - 두번째 터치를 null로
                    if (touchPrevPos[0] == null || touchPrevPos[1] != null )
                    {
                        touchPrevPos[0] = Input.GetTouch(0).position;
                        touchPrevPos[1] = null;
                        return;
                    }

                    //이상없을때 - 첫번째터치의 위치를 받아 이동, 그후 최종이동 위치를 처음위치로 초기화
                    Vector2 touchNewPos = Input.GetTouch(0).position;
                    transform.position += transform.TransformDirection((Vector3)((touchPrevPos[0] - touchNewPos) *
                        Camera.main.orthographicSize / Camera.main.pixelHeight * 2f));

                    MoveLimit();
                    touchPrevPos[0] = touchNewPos;

                }
                else if (Input.touchCount == 2)//터치가 두개
                {
                    //순차적 예외처리 - 값 다시 받아오기
                    if(touchPrevPos[1] == null)
                    {
                        touchPrevPos[0] = Input.GetTouch(0).position;
                        touchPrevPos[1] = Input.GetTouch(1).position;
                        touchPrevVector = (Vector2)(touchPrevPos[0] - touchPrevPos[1]);
                        touchPrevDist = touchPrevVector.magnitude;
                    }
                    else //이상없을시 두개터치 위치의 거리로 확대 축소
                    {
                        //?
                        Vector2 screen = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);

                        //새위치 배열에 저장
                        Vector2[] touchNewPos = { Input.GetTouch(0).position, Input.GetTouch(1).position };
                        Vector2 touchNewVector = touchNewPos[0] - touchNewPos[1];
                        float touchNewDist = touchNewVector.magnitude;

                        //사잇값으로 확대 축소
                        transform.position += transform.TransformDirection((Vector3)((touchPrevPos[0] + touchPrevPos[1] - screen) * 
                            Camera.main.orthographicSize / screen.y));
                        Camera.main.orthographicSize += touchPrevDist / touchNewDist;
                        transform.position-=transform.TransformDirection((Vector3)((touchPrevPos[0] + touchPrevPos[1] - screen) *
                            Camera.main.orthographicSize / screen.y));

                        //범위 지정
                        Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 2f);
                        Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize, 5f);

                        //마지막 위치 저장
                        touchPrevPos[0] = touchNewPos[0];
                        touchPrevPos[0] = touchNewPos[0];
                        touchPrevVector = touchNewVector;
                        touchPrevDist = touchNewDist;

                    }
                }
                else
                {
                    return;
                }*/



        //게임 시작 후 줌인 기능 비활성화
        if (target != null && target.gameObject != null && GameStartFlag)
        {
            TargetPosition.Set(target.transform.position.x, target.transform.position.y - 0.3f, MainCamera.transform.position.z);
            MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, TargetPosition, moveSpeed * Time.deltaTime);
            
            if (MainCamera.orthographicSize > 2.0f)
            {
                MainCamera.orthographicSize -= moveSpeed * Time.deltaTime;//moveSpeed로 줌 인 속도 조절 expandCamera.orthographicSize -= moveSpeed * Time.deltaTime; }
            }

        }
        else if (target != null && target.gameObject != null)
        {
            TargetPosition.Set(target.transform.position.x, target.transform.position.y - 2f, MainCamera.transform.position.z);
            MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, TargetPosition, moveSpeed * Time.deltaTime);
        }
        else //게임 시작 전 줌인 가능
        {
            //마우스가 UI에 머물러 있을 때는 아래 코드가 실행되지 않도록 함
            if (EventSystem.current.IsPointerOverGameObject() == true)
            {
                return;
            }

            if (target == null && GameStartFlag) //카메라 타겟이 없고 게임중이 아니면
                return;

            /*
            //마우스 스크롤
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            MainCamera.orthographicSize += scroll;
            */

            //scrren out 방지
            if (MoveFlag != true)
            {
                /*if (transform.position.x < -1.2f)
                {
                    float ScreenMove = transform.position.x;
                    ScreenMove += 0.4f;
                    transform.position = new Vector3(ScreenMove, transform.position.y, transform.position.z);
                }
                if (transform.position.x > 1.1f)
                {
                    float ScreenMove = transform.position.x;
                    ScreenMove -= 0.4f;
                    transform.position = new Vector3(ScreenMove, transform.position.y, transform.position.z);
                }
                if (transform.position.y < -2.0f)
                {
                    float ScreenMove = transform.position.y;
                    ScreenMove += 0.2f;
                    transform.position = new Vector3(transform.position.x, ScreenMove, transform.position.z);
                }
                if (transform.position.y > 1.9f)
                {
                    float ScreenMove = transform.position.y;
                    ScreenMove -= 0.2f;
                    transform.position = new Vector3(transform.position.x, ScreenMove, transform.position.z);
                }*/
               

                //화면 줌인 줌아웃시 원래 값으로 되돌림
                /*
                if (MainCamera.orthographicSize > 5.0f)
                {
                    MainCamera.orthographicSize -= 0.1f;
                }
                if (MainCamera.orthographicSize < 5.0f)
                {
                    MainCamera.orthographicSize += 0.1f;
                }
                */
            }


        }

        //카메라 흔들기
        if (ShakeTime > 0)
        {
            if (ShakeTime == 0.2f)
                initialPosition = transform.position;

            transform.position = Random.insideUnitSphere * 0.05f + initialPosition;
            ShakeTime -= Time.deltaTime;
        }
        else
        {
            //transform.position = initialPosition;
            ShakeTime = 0.0f;
        }


        //마우스가 UI에 머물러 있을 때는 아래 코드가 실행되지 않도록 함
        if (EventSystem.current.IsPointerOverGameObject() == true)
        {
            return;
        }
        //마우스 클릭으로 이동
        if (Input.GetMouseButtonDown(0))
        {

            MouseStart = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
            MouseStart = Camera.main.ScreenToWorldPoint(MouseStart);

            MoveFlag = true;

        }
        else if (Input.GetMouseButton(0))
        {
            if (MoveFlag)
            {

                var MouseMove = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
                MouseMove = Camera.main.ScreenToWorldPoint(MouseMove);

                transform.position = transform.position - (MouseMove - MouseStart);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            MoveFlag = false;
        }
    }

    public void VibrateForTime(float time)
    {
        if (time == 0f)
        {
            time = 0.3f;
        }
        ShakeTime = time;
    }


}
