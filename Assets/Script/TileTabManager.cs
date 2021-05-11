using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private GameObject TileRecovery = null;
    private GameObject TileAttackDamageUp = null;
    private GameObject TileDefenseUp = null;
    private GameObject TileMonster = null;

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
            Debug.Log("Tile이 비어있지 않습니다.");
        }


    }
    //회복 정보
    public void SetRecoveryInfo()
    {
        TileInfomationText.text = "회복\n\n배치한 곳에 플레이어가 지나가면 체력이 50% 회복 됩니다.\n체력이 회복량이 100%가 넘으면 100%까지만 회복됩니다.";
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
            Debug.Log("Tile이 비어있지 않습니다.");
        }
    }
    //공격력증가 정보
    public void SetAttackDamageUpInfo()
    {
        TileInfomationText.text = "공격력증가\n\n배치한 곳에 플레이어가 지나가면 공격력이 10초간 30% 증가 됩니다.\n중복 습득 시 지속시간 동일 * 30% 추가 증가";
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
            Debug.Log("Tile이 비어있지 않습니다.");
        }
    }
    //방어력증가 정보
    public void SetDefenseUpInfo()
    {
        TileInfomationText.text = "방어력증가\n\n배치한 곳에 플레이어가 지나가면 방어력이 10초간 30% 증가 됩니다.\n중복 습득 시 지속시간 동일 * 30% 추가 증가";
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
            Debug.Log("Tile이 비어있지 않습니다.");
        }
    }
    //몬스터 정보
    public void SetMonsterInfo()
    {
        TileInfomationText.text = "몬스터\n\n배치한 곳에 몬스터가 랜덤 생성 됩니다.";
    }





}


