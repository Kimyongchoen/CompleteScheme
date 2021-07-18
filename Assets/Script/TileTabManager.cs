using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TileTabManager : MonoBehaviour
{
    [SerializeField]
    private Text TileInfomationText;

    [SerializeField]
    private GameObject TileRecoveryfrefab;
    [SerializeField]
    private GameObject TileAttackDamageUpfrefab;
    [SerializeField]
    private GameObject TileDefenseUpfrefab;
    [SerializeField]
    private GameObject TileMonsterfrefab;
    [SerializeField]
    private TextMeshProUGUI MainMessageBox;

    private GameObject TileRecovery = null;
    private GameObject TileAttackDamageUp = null;
    private GameObject TileDefenseUp = null;
    private GameObject TileMonster = null;

    [SerializeField]
    private AudioClip AudioInfo;//정보 버튼 소리

    [SerializeField]
    private AudioClip AudioFail;//실패 버튼 소리

    private AudioSource audioSource;
    public void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

    }
    //회복 배치
    public void SetRecovery(Transform Tile)
    {
        TileManager tileManager = Tile.GetComponent<TileManager>();

        if (tileManager.getMonsterFlag() == 0)
        {
            TileRecovery = Instantiate(TileRecoveryfrefab);//Tile 선택 오브젝트 생성
            TileRecovery.transform.position = Tile.transform.position;//타일리스트에 있는 위치로 이동
            TileRecovery.transform.position = new Vector3(TileRecovery.transform.position.x, TileRecovery.transform.position.y, 1);//한단계 뒤로 이동

            TileRecovery.GetComponent<Buff>().setBuff(1,0.5f);//회복 1 // 50%
            tileManager.setMonsterFlag(3, 1, TileRecovery); // 상태 , 넘버
            SetRecoveryInfo();//회복 정보 표시
        }
        else
        {
            audioSource.clip = AudioFail;//실패 버튼 소리
            StartCoroutine("OnAudio");
            SetMainMessageBox("이미 몬스터나 버프가 있습니다");
        }


    }
    //회복 정보
    public void SetRecoveryInfo()
    {
        audioSource.clip = AudioInfo;//정보 버튼 소리
        StartCoroutine("OnAudio");
        TileInfomationText.text = "";
        TileInfomationText.DOText("회복\n\n배치한 곳에 플레이어가 지나가면 체력이 50% 회복 됩니다.\n체력이 회복량이 100%가 넘으면 100%까지만 회복됩니다.", 0.3f);

    }

    //공력력증가 배치
    public void SetAttackDamageUp(Transform Tile)
    {
        TileManager tileManager = Tile.GetComponent<TileManager>();

        if (tileManager.getMonsterFlag() == 0)
        {
            TileAttackDamageUp = Instantiate(TileAttackDamageUpfrefab);//Tile 선택 오브젝트 생성
            TileAttackDamageUp.transform.position = Tile.transform.position;//타일리스트에 있는 위치로 이동
            TileAttackDamageUp.GetComponent<Buff>().setBuff(2, 0.3f);//2 공격력 증가 
            TileAttackDamageUp.transform.position = new Vector3(TileAttackDamageUp.transform.position.x, TileAttackDamageUp.transform.position.y, 1);//한단계 뒤로 이동
            tileManager.setMonsterFlag(3, 2, TileAttackDamageUp); // 상태 , 넘버
            SetAttackDamageUpInfo();//공격력 증가 정보 표시
        }
        else
        {
            audioSource.clip = AudioFail;//실패 버튼 소리
            StartCoroutine("OnAudio");

            SetMainMessageBox("이미 몬스터나 버프가 있습니다");
        }
    }
    //공격력증가 정보
    public void SetAttackDamageUpInfo()
    {
        audioSource.clip = AudioInfo;//정보 버튼 소리
        StartCoroutine("OnAudio");
        TileInfomationText.text = "";
        TileInfomationText.DOText("공격력증가\n\n배치한 곳에 플레이어가 지나가면 공격력이 10초간 30% 증가 됩니다.(중첩 가능)", 0.3f);
    }

    //방어력증가 배치
    public void SetDefenseUp(Transform Tile)
    {
        TileManager tileManager = Tile.GetComponent<TileManager>();

        if (tileManager.getMonsterFlag() == 0)
        {
            TileDefenseUp = Instantiate(TileDefenseUpfrefab);//Tile 선택 오브젝트 생성
            TileDefenseUp.transform.position = Tile.transform.position;//타일리스트에 있는 위치로 이동
            TileDefenseUp.GetComponent<Buff>().setBuff(3, 0.3f);//3 방어력 증가
            TileDefenseUp.transform.position = new Vector3(TileDefenseUp.transform.position.x, TileDefenseUp.transform.position.y, 1);//한단계 뒤로 이동

            tileManager.setMonsterFlag(3, 3, TileDefenseUp); // 상태 , 넘버
            SetDefenseUpInfo();//방어력 증가 정보 표시
        }
        else
        {
            audioSource.clip = AudioFail;//실패 버튼 소리
            StartCoroutine("OnAudio");
            SetMainMessageBox("이미 몬스터나 버프가 있습니다");
        }
    }
    //방어력증가 정보
    public void SetDefenseUpInfo()
    {
        audioSource.clip = AudioInfo;//정보 버튼 소리
        StartCoroutine("OnAudio");
        TileInfomationText.text = "";
        TileInfomationText.DOText("방어력증가\n\n배치한 곳에 플레이어가 지나가면 방어력이 10초간 30% 증가 됩니다.(중첩 가능)", 0.3f);
    }

    //몬스터 배치
    public void SetMonster(Transform Tile)
    {
        TileManager tileManager = Tile.GetComponent<TileManager>();

        if (tileManager.getMonsterFlag() == 0)
        {
            TileMonster = Instantiate(TileMonsterfrefab);//Tile 선택 오브젝트 생성
            TileMonster.transform.position = Tile.transform.position;//타일리스트에 있는 위치로 이동
            TileMonster.GetComponent<Buff>().setBuff(4, 0.5f);
            TileMonster.transform.position = new Vector3(TileMonster.transform.position.x, TileMonster.transform.position.y, 1);//한단계 뒤로 이동

            tileManager.setMonsterFlag(2, 1, TileMonster); // 상태 , 넘버
            SetMonsterInfo();//몬스터 정보 표시
        }
        else
        {

            audioSource.clip = AudioFail;//실패 버튼 소리
            StartCoroutine("OnAudio");
            SetMainMessageBox("이미 몬스터나 버프가 있습니다");
        }
    }
    //몬스터 정보
    public void SetMonsterInfo()
    {
        audioSource.clip = AudioInfo;//정보 버튼 소리
        StartCoroutine("OnAudio");
        TileInfomationText.text = "";
        TileInfomationText.DOText("몬스터\n\n배치한 곳에 몬스터가 랜덤 생성 됩니다.", 0.3f);
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
    private IEnumerator OnAudio()
    {
        audioSource.Play();
        yield return new WaitForSeconds(1f);
    }


}


