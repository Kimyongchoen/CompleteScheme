using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class ButtonColtroller : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;//플래이어 정보

    [SerializeField]
    private ItemStats itemStats;//아이템 정보

    //플레이어 골드
    [SerializeField]
    private Text Gold;

    //아이템 정보
    [SerializeField]
    private Image HatImg;
    [SerializeField]
    private Text HatPlus;

    [SerializeField]
    private Image WeaponImg;
    [SerializeField]
    private Text WeaponPlus;

    [SerializeField]
    private Image ArmorImg;
    [SerializeField]
    private Text ArmorPlus;

    [SerializeField]
    private Image GlovesImg;
    [SerializeField]
    private Text GlovesPlus;

    [SerializeField]
    private Image ShieldImg;
    [SerializeField]
    private Text ShieldPlus;

    [SerializeField]
    private Image BootsImg;
    [SerializeField]
    private Text BootsPlus;


    //아이템 구매 및 강화
    [SerializeField]
    private Image SelectItemImg;
    [SerializeField]
    private Text ItmeName;
    [SerializeField]
    private Text ItmeStats;
    [SerializeField]
    private Text ItmeGold;

    [SerializeField]
    private Text ItmeNameUp;
    [SerializeField]
    private Text ItmeStatsUp;
    [SerializeField]
    private Text ItemBtnText;
    [SerializeField]
    private Text ItmePercentage;

    //메시지 박스
    [SerializeField]
    private TextMeshProUGUI MainMessageBox;

    //정보 새로 고침
    [SerializeField]
    private LoadShopInfo loadShopInfo;

    //아이템 이름
    private string ItemName;

    //강화 확률
    private float[] Percentage = { 100, 80, 60, 40, 20 };


    [SerializeField]
    private AudioClip AudioInfo;//정보 버튼 소리

    [SerializeField]
    private AudioClip AudioFail;//실패 버튼 소리

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void SelectItme(string ItemName)
    {
        this.ItemName = ItemName;

        loadShopInfo.LoadInfo();

        ItmeNameUp.text = "";
        ItmeStatsUp.text = "";
        ItmePercentage.text = "";
        ItemBtnText.text = "구 매";
        
        Color color = SelectItemImg.color;
        color.a = 1f;
        SelectItemImg.color = color;

        switch (ItemName)
        {
            case "Weapon":
                SelectItemImg.sprite = WeaponImg.sprite;//선택한 장비에 따라 이미지 변경

                if (playerStats.stats[0].Weapon >= 11)//강화 수치가 max 일경우
                {
                    ItmeName.text = "+" + playerStats.stats[0].Weapon + " ";
                    ItmeStats.text = "공격력\n+" + itemStats.Weapon[playerStats.stats[0].Weapon];
                    ItmeGold.text = "강화 불가";
                    ItemBtnText.text = "불 가";
                    break;
                }
                ItmeName.text = "손";
                ItmeStats.text = "공격력\n+0";
                ItemBtnText.text = "구 매";
                ItmeGold.text = itemStats.WeaponGold[playerStats.stats[0].Weapon+1] + " 원";

                if (playerStats.stats[0].Weapon >= 0)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Weapon + " " + itemStats.WeaponName;
                    ItmeStats.text = "공격력\n+" + itemStats.Weapon[playerStats.stats[0].Weapon];
                    ItemBtnText.text = "강 화";

                    ItmePercentage.text = Percentage[4].ToString() + "%";
                    if (playerStats.stats[0].Weapon < 5)//강화 확율
                        ItmePercentage.text = Percentage[playerStats.stats[0].Weapon].ToString() + "%";
                }
                
                ItmeNameUp.text = "+" + (playerStats.stats[0].Weapon + 1) + " "+ itemStats.WeaponName;
                ItmeStatsUp.text = "공격력\n+" + itemStats.Weapon[playerStats.stats[0].Weapon+1];
                
                break;
            case "Armor":
                SelectItemImg.sprite = ArmorImg.sprite;//선택한 장비에 따라 이미지 변경

                if (playerStats.stats[0].Armor >= 11)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Armor + " ";
                    ItmeStats.text = "방어력\n+" + itemStats.Armor[playerStats.stats[0].Armor];
                    ItmeGold.text = "강화 불가";
                    ItemBtnText.text = "불 가";
                    break;
                }
                ItmeName.text = "몸";
                ItmeStats.text = "방어력\n+0";
                ItemBtnText.text = "구 매";
                ItmeGold.text = itemStats.ArmorGold[playerStats.stats[0].Armor + 1] + " 원";

                if (playerStats.stats[0].Armor >= 0)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Armor + " "+ itemStats.ArmorName;
                    ItmeStats.text = "방어력\n+" + itemStats.Armor[playerStats.stats[0].Armor];
                    ItemBtnText.text = "강 화";

                    ItmePercentage.text = Percentage[4].ToString() + "%";
                    if (playerStats.stats[0].Armor < 5)//강화 확율
                        ItmePercentage.text = Percentage[playerStats.stats[0].Armor].ToString() + "%";
                }
                ItmeNameUp.text = "+" + (playerStats.stats[0].Armor + 1) + " " + itemStats.ArmorName;
                ItmeStatsUp.text = "방어력\n+" + itemStats.Armor[playerStats.stats[0].Armor + 1];
                break;
            case "Hat":
                SelectItemImg.sprite = HatImg.sprite;//선택한 장비에 따라 이미지 변경

                if (playerStats.stats[0].Hat >= 11)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Hat + " ";
                    ItmeStats.text = "체력\n+" + itemStats.Hat[playerStats.stats[0].Hat];
                    ItmeGold.text = "강화 불가";
                    ItemBtnText.text = "불 가";
                    break;
                }
                ItmeName.text = "머 리";
                ItmeStats.text = "체력\n+0";
                ItemBtnText.text = "구 매";
                ItmeGold.text = itemStats.HatGold[playerStats.stats[0].Hat + 1] + " 원";

                if (playerStats.stats[0].Hat >= 0)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Hat + " " + itemStats.HatName;
                    ItmeStats.text = "체력\n+" + itemStats.Hat[playerStats.stats[0].Hat];
                    ItemBtnText.text = "강 화";

                    ItmePercentage.text = Percentage[4].ToString() + "%";
                    if (playerStats.stats[0].Hat < 5)//강화 확율
                        ItmePercentage.text = Percentage[playerStats.stats[0].Hat].ToString() + "%";
                }
                ItmeNameUp.text = "+" + (playerStats.stats[0].Hat + 1) + " " + itemStats.HatName;
                ItmeStatsUp.text = "체력\n+" + itemStats.Hat[playerStats.stats[0].Hat + 1];
                break;
            case "Gloves":
                SelectItemImg.sprite = GlovesImg.sprite;//선택한 장비에 따라 이미지 변경

                if (playerStats.stats[0].Gloves >= 11)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Gloves + " ";
                    ItmeStats.text = "치명타 확율\n+" + itemStats.Gloves[playerStats.stats[0].Gloves] + "%";
                    ItmeGold.text = "강화 불가";
                    ItemBtnText.text = "불 가";
                    break;
                }
                ItmeName.text = "손";
                ItmeStats.text = "치명타 확율\n+0%";
                ItemBtnText.text = "구 매";
                ItmeGold.text = itemStats.GlovesGold[playerStats.stats[0].Gloves + 1] + " 원";

                if (playerStats.stats[0].Gloves >= 0)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Gloves + " " + itemStats.GlovesName;
                    ItmeStats.text = "치명타 확율\n+" + itemStats.Gloves[playerStats.stats[0].Gloves] + "%";
                    ItemBtnText.text = "강 화";

                    ItmePercentage.text = Percentage[4].ToString() + "%";
                    if (playerStats.stats[0].Gloves < 5)//강화 확율
                        ItmePercentage.text = Percentage[playerStats.stats[0].Gloves].ToString() + "%";
                }
                ItmeNameUp.text = "+" + (playerStats.stats[0].Gloves + 1) + " " + itemStats.GlovesName;
                ItmeStatsUp.text = "치명타 확율\n+" + itemStats.Gloves[playerStats.stats[0].Gloves + 1] + "%";
                break;
            case "Boots":
                SelectItemImg.sprite = BootsImg.sprite;//선택한 장비에 따라 이미지 변경

                if (playerStats.stats[0].Boots >= 11)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Boots + " ";
                    ItmeStats.text = "회피력\n+" + itemStats.Boots[playerStats.stats[0].Boots];
                    ItmeGold.text = "강화 불가";
                    ItemBtnText.text = "불 가";
                    break;
                }
                ItmeName.text = "발";
                ItmeStats.text = "회피력\n+0";
                ItemBtnText.text = "구 매";
                ItmeGold.text = itemStats.BootsGold[playerStats.stats[0].Boots + 1] + " 원";

                if (playerStats.stats[0].Boots >= 0)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Boots + " " + itemStats.BootsName;
                    ItmeStats.text = "회피력\n+" + itemStats.Boots[playerStats.stats[0].Boots];
                    ItemBtnText.text = "강 화";

                    ItmePercentage.text = Percentage[4].ToString() + "%";
                    if (playerStats.stats[0].Boots < 5)//강화 확율
                        ItmePercentage.text = Percentage[playerStats.stats[0].Boots].ToString() + "%";
                }
                ItmeNameUp.text = "+" + (playerStats.stats[0].Boots + 1) + " " + itemStats.BootsName;
                ItmeStatsUp.text = "회피력\n+" + itemStats.Boots[playerStats.stats[0].Boots + 1];
                break;
            case "Shield":
                SelectItemImg.sprite = ShieldImg.sprite;//선택한 장비에 따라 이미지 변경

                if (playerStats.stats[0].Shield >= 11)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Shield + " ";
                    ItmeStats.text = "체력 흡혈\n+" + (itemStats.Shield[playerStats.stats[0].Shield]*100) + "%";
                    ItmeGold.text = "강화 불가";
                    ItemBtnText.text = "불 가";
                    break;
                }
                ItmeName.text = "손";
                ItmeStats.text = "체력 흡혈\n+0%";
                ItemBtnText.text = "구 매";
                ItmeGold.text = itemStats.ShieldGold[playerStats.stats[0].Shield + 1] + " 원";

                if (playerStats.stats[0].Shield >= 0)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Shield + " " + itemStats.ShieldName;
                    ItmeStats.text = "체력 흡혈\n+" + (itemStats.Shield[playerStats.stats[0].Shield] * 100) + "%";
                    ItemBtnText.text = "강 화";

                    ItmePercentage.text = Percentage[4].ToString() + "%";
                    if (playerStats.stats[0].Shield < 5)//강화 확율
                        ItmePercentage.text = Percentage[playerStats.stats[0].Shield].ToString()+"%";
                }
                ItmeNameUp.text = "+" + (playerStats.stats[0].Shield + 1) + " " + itemStats.ShieldName;
                ItmeStatsUp.text = "체력 흡혈\n+" + (itemStats.Shield[playerStats.stats[0].Shield + 1] * 100) + "%";
                break;
            default:
                SelectItemImg.sprite = null;//선택한 장비에 따라 이미지 변경
                ItmeName.text = "";
                ItmeStats.text = "";
                ItemBtnText.text = "구 매";
                ItmeGold.text = "";
                color.a = 0f;
                SelectItemImg.color = color;
                break;
        }
        
    }
    public void UpGrade()
    {
        int ItmeGold;
        float ItmePercentage;
        string msg = "";

        if (ItemName == null)
        {
            audioSource.clip = AudioFail;//실패 버튼 소리
            StartCoroutine("OnAudio");
            SetMainMessageBox("장비를 선택 해주세요");
            return;
        }
        switch (ItemName)
        {
            case "Weapon":
                if (playerStats.stats[0].Weapon >= 11)//강화 수치가 max 일경우
                {
                    msg = "강화 할 수 없습니다";
                    audioSource.clip = AudioFail;//실패 버튼 소리
                }
                else
                {
                    //처음 구매
                    ItmeGold = itemStats.WeaponGold[playerStats.stats[0].Weapon + 1];
                    ItmePercentage = 100;
                    msg = "구매 성공";
                    audioSource.clip = AudioInfo;//정보 버튼 소리

                    //강화
                    if (playerStats.stats[0].Weapon >= 0)
                    {
                        msg = "강화 성공";
                        audioSource.clip = AudioInfo;//정보 버튼 소리
                        ItmePercentage = Percentage[4];//강화 확율 100%, 80%, 60%, 40%, 20%, 20% .... 20%
                        if (playerStats.stats[0].Weapon < 5)
                            ItmePercentage = Percentage[playerStats.stats[0].Weapon];
                    }

                    if (playerStats.stats[0].gold >= ItmeGold)
                    {
                        //골드 차감
                        playerStats.stats[0].gold -= ItmeGold;

                        if (Random.Range(1f, 100f) < ItmePercentage) // 강화 확율 (0-100)
                        {
                            //성공
                            playerStats.stats[0].Weapon += 1;
                        }
                        else
                        {
                            audioSource.clip = AudioFail;//실패 버튼 소리
                            msg = "강화 실패";
                        }
                    }
                    else
                    {
                        audioSource.clip = AudioFail;//실패 버튼 소리
                        msg = "골드가 부족합니다";
                    }
                   

                }
                break;
            case "Armor":
                if (playerStats.stats[0].Armor >= 11)//강화 수치가 max 일경우
                {
                    audioSource.clip = AudioFail;//실패 버튼 소리
                    msg = "강화 할 수 없습니다";
                }
                else
                {
                    //처음 구매
                    ItmeGold = itemStats.ArmorGold[playerStats.stats[0].Armor + 1];
                    ItmePercentage = 100f;
                    msg = "구매 성공";
                    audioSource.clip = AudioInfo;//정보 버튼 소리
                    //강화
                    if (playerStats.stats[0].Armor >= 0)
                    {
                        audioSource.clip = AudioInfo;//정보 버튼 소리
                        msg = "강화 성공";
                        ItmePercentage = Percentage[4];//강화 확율 100%, 80%, 60%, 40%, 20%, 20% .... 20%
                        if (playerStats.stats[0].Armor < 5)
                            ItmePercentage = Percentage[playerStats.stats[0].Armor];
                    }

                    if (playerStats.stats[0].gold >= ItmeGold)
                    {
                        //골드 차감
                        playerStats.stats[0].gold -= ItmeGold;

                        if (Random.Range(1f, 100f) < ItmePercentage) // 강화 확율 (0-100)
                        {
                            //성공
                            playerStats.stats[0].Armor += 1;
                        }
                        else
                        {
                            audioSource.clip = AudioFail;//실패 버튼 소리
                            msg = "강화 실패";
                        }
                    }
                    else
                    {
                        audioSource.clip = AudioFail;//실패 버튼 소리
                        msg = "골드가 부족합니다";
                    }

                }
                break;
            case "Hat":
                if (playerStats.stats[0].Hat >= 11)//강화 수치가 max 일경우
                {
                    msg = "강화 할 수 없습니다";
                    audioSource.clip = AudioFail;//실패 버튼 소리
                }
                else
                {
                    //처음 구매
                    ItmeGold = itemStats.HatGold[playerStats.stats[0].Hat+1];
                    ItmePercentage = 100f;
                    msg = "구매 성공";
                    audioSource.clip = AudioInfo;//정보 버튼 소리

                    //강화
                    if (playerStats.stats[0].Hat >= 0)
                    {
                        audioSource.clip = AudioInfo;//정보 버튼 소리
                        msg = "강화 성공";
                        ItmePercentage = Percentage[4];//강화 확율 100%, 80%, 60%, 40%, 20%, 20% .... 20%
                        if (playerStats.stats[0].Hat < 5)
                            ItmePercentage = Percentage[playerStats.stats[0].Hat];
                    }

                    if (playerStats.stats[0].gold >= ItmeGold)
                    {
                        //골드 차감
                        playerStats.stats[0].gold -= ItmeGold;

                        if (Random.Range(1f, 100f) < ItmePercentage) // 강화 확율 (0-100)
                        {
                            //성공
                            playerStats.stats[0].Hat += 1;
                        }
                        else
                        {
                            audioSource.clip = AudioFail;//실패 버튼 소리
                            msg = "강화 실패";
                        }
                    }
                    else
                    {
                        audioSource.clip = AudioFail;//실패 버튼 소리
                        msg = "골드가 부족합니다";
                    }

                }
                break;
            case "Gloves":
                if (playerStats.stats[0].Gloves >= 11)//강화 수치가 max 일경우
                {
                    audioSource.clip = AudioFail;//실패 버튼 소리
                    msg = "강화 할 수 없습니다";
                }
                else
                {
                    //처음 구매
                    ItmeGold = itemStats.GlovesGold[playerStats.stats[0].Gloves+1];
                    ItmePercentage = 100f;
                    audioSource.clip = AudioInfo;//정보 버튼 소리
                    msg = "구매 성공";

                    //강화
                    if (playerStats.stats[0].Gloves >= 0)
                    {
                        audioSource.clip = AudioInfo;//정보 버튼 소리
                        msg = "강화 성공";
                        ItmePercentage = Percentage[4];//강화 확율 100%, 80%, 60%, 40%, 20%, 20% .... 20%
                        if (playerStats.stats[0].Gloves < 5)
                            ItmePercentage = Percentage[playerStats.stats[0].Gloves];
                    }

                    if (playerStats.stats[0].gold >= ItmeGold)
                    {
                        //골드 차감
                        playerStats.stats[0].gold -= ItmeGold;

                        if (Random.Range(1f, 100f) < ItmePercentage) // 강화 확율 (0-100)
                        {
                            //성공
                            playerStats.stats[0].Gloves += 1;
                        }
                        else
                        {
                            audioSource.clip = AudioFail;//실패 버튼 소리
                            msg = "강화 실패";
                        }
                    }
                    else
                    {
                        audioSource.clip = AudioFail;//실패 버튼 소리
                        msg = "골드가 부족합니다";
                    }

                }
                break;
            case "Boots":
                if (playerStats.stats[0].Boots >= 11)//강화 수치가 max 일경우
                {
                    msg = "강화 할 수 없습니다";
                    audioSource.clip = AudioFail;//실패 버튼 소리
                }
                else
                {
                    //처음 구매
                    ItmeGold = itemStats.BootsGold[playerStats.stats[0].Boots+1];
                    ItmePercentage = 100f;
                    msg = "구매 성공";
                    audioSource.clip = AudioInfo;//정보 버튼 소리

                    //강화
                    if (playerStats.stats[0].Boots >= 0)
                    {
                        audioSource.clip = AudioInfo;//정보 버튼 소리
                        msg = "강화 성공";
                        ItmePercentage = Percentage[4];//강화 확율 100%, 80%, 60%, 40%, 20%, 20% .... 20%
                        if (playerStats.stats[0].Boots < 5)
                            ItmePercentage = Percentage[playerStats.stats[0].Boots];
                    }

                    if (playerStats.stats[0].gold >= ItmeGold)
                    {
                        //골드 차감
                        playerStats.stats[0].gold -= ItmeGold;

                        if (Random.Range(1f, 100f) < ItmePercentage) // 강화 확율 (0-100)
                        {
                            //성공
                            playerStats.stats[0].Boots += 1;
                        }
                        else
                        {
                            msg = "강화 실패";
                            audioSource.clip = AudioFail;//실패 버튼 소리
                        }
                    }
                    else
                    {
                        msg = "골드가 부족합니다";
                        audioSource.clip = AudioFail;//실패 버튼 소리
                    }

                }
                break;
            case "Shield":
                if (playerStats.stats[0].Shield >= 11)//강화 수치가 max 일경우
                {
                    audioSource.clip = AudioFail;//실패 버튼 소리
                    msg = "강화 할 수 없습니다";
                }
                else
                {
                    //처음 구매
                    ItmeGold = itemStats.ShieldGold[playerStats.stats[0].Shield+1];
                    ItmePercentage = 100f;
                    msg = "구매 성공";
                    audioSource.clip = AudioInfo;//정보 버튼 소리
                    //강화
                    if (playerStats.stats[0].Shield >= 0)
                    {
                        audioSource.clip = AudioInfo;//정보 버튼 소리
                        msg = "강화 성공";
                        ItmePercentage = Percentage[4];//강화 확율 100%, 80%, 60%, 40%, 20%, 20% .... 20%
                        if (playerStats.stats[0].Shield < 5)
                            ItmePercentage = Percentage[playerStats.stats[0].Shield];
                    }

                    if (playerStats.stats[0].gold >= ItmeGold)
                    {
                        //골드 차감
                        playerStats.stats[0].gold -= ItmeGold;

                        if (Random.Range(1f, 100f) < ItmePercentage) // 강화 확율 (0-100)
                        {
                            //성공
                            playerStats.stats[0].Shield += 1;
                        }
                        else
                        {
                            audioSource.clip = AudioFail;//실패 버튼 소리
                            msg = "강화 실패";
                        }
                    }
                    else
                    {
                        audioSource.clip = AudioFail;//실패 버튼 소리
                        msg = "골드가 부족합니다";
                    }

                }
                break;
            default:
                break;
        }


        StartCoroutine("OnAudio");
        EditorUtility.SetDirty(playerStats); //데이터 변경후 저장 강제 로직 추가
        SetMainMessageBox(msg);
        SelectItme(ItemName);//재로드

    }

    public void buyTile(string TileName)
    {
        int ItmeGold;
        audioSource.clip = AudioInfo;//정보 버튼 소리
 
        switch (TileName)
        {
            case "Recovery"://회복
                if (itemStats.RecoveryCnt>=2)
                {
                    audioSource.clip = AudioFail;//실패 버튼 소리
                    SetMainMessageBox("더이상 구매 할 수 없습니다");
                    break;
                }

                ItmeGold = itemStats.RecoveryGold;

                if (playerStats.stats[0].gold >= ItmeGold)
                {
                    playerStats.stats[0].gold -= ItmeGold;
                    itemStats.RecoveryCnt += 1;
                    SetMainMessageBox("구매 성공");
                }
                else
                {
                    audioSource.clip = AudioFail;//실패 버튼 소리
                    SetMainMessageBox("골드가 부족합니다");
                }

                break;
            case "AttackDamageUp"://공격력 강화
                if (itemStats.AttackDamageUpCnt >= 2)
                {
                    audioSource.clip = AudioFail;//실패 버튼 소리
                    SetMainMessageBox("더이상 구매 할 수 없습니다");
                    break;
                }

                ItmeGold = itemStats.AttackDamageUpGold;

                if (playerStats.stats[0].gold >= ItmeGold)
                {
                    playerStats.stats[0].gold -= ItmeGold;
                    itemStats.AttackDamageUpCnt += 1;
                    SetMainMessageBox("구매 성공");
                }
                else
                {
                    audioSource.clip = AudioFail;//실패 버튼 소리
                    SetMainMessageBox("골드가 부족합니다");
                }

                break;
            case "DefenseUp"://방어력 강화
                if (itemStats.DefenseUpCnt >= 2)
                {
                    audioSource.clip = AudioFail;//실패 버튼 소리
                    SetMainMessageBox("더이상 구매 할 수 없습니다");
                    break;
                }

                ItmeGold = itemStats.DefenseUpGold;

                if (playerStats.stats[0].gold >= ItmeGold)
                {
                    playerStats.stats[0].gold -= ItmeGold;
                    itemStats.DefenseUpCnt += 1;
                    SetMainMessageBox("구매 성공");
                }
                else
                {
                    audioSource.clip = AudioFail;//실패 버튼 소리
                    SetMainMessageBox("골드가 부족합니다");
                }

                break;
            case "Monster"://몬스터 배치
                if (itemStats.MonsterCnt >= 99)
                {
                    audioSource.clip = AudioFail;//실패 버튼 소리
                    SetMainMessageBox("더이상 구매 할 수 없습니다");
                    break;
                }

                ItmeGold = itemStats.MonsterGold;

                if (playerStats.stats[0].gold >= ItmeGold)
                {
                    playerStats.stats[0].gold -= ItmeGold;
                    itemStats.MonsterCnt += 1;
                    SetMainMessageBox("구매 성공");
                }
                else
                {
                    audioSource.clip = AudioFail;//실패 버튼 소리
                    SetMainMessageBox("골드가 부족합니다");
                }

                break;

            default:
                break;
        }

        StartCoroutine("OnAudio");
        EditorUtility.SetDirty(itemStats); //데이터 변경후 저장 강제 로직 추가
        SelectItme(ItemName);//재로드
    }

    public void ResetGold()
    {
        audioSource.clip = AudioInfo;//정보 버튼 소리
        StartCoroutine("OnAudio");

        //골드 초기화
        playerStats.stats[0].gold = playerStats.stats[0].goldMax;

        // 장비 초기화
        playerStats.stats[0].Weapon = -1;
        playerStats.stats[0].Armor = -1;
        playerStats.stats[0].Hat = -1;
        playerStats.stats[0].Gloves = -1;
        playerStats.stats[0].Boots = -1;
        playerStats.stats[0].Shield = -1;

        //타일 초기화
        itemStats.RecoveryCnt = 0;
        itemStats.AttackDamageUpCnt = 0;
        itemStats.DefenseUpCnt = 0;
        itemStats.MonsterCnt = 0;

        loadShopInfo.LoadInfo();

        SelectItme("");
        EditorUtility.SetDirty(playerStats); //데이터 변경후 저장 강제 로직 추가
        EditorUtility.SetDirty(itemStats); //데이터 변경후 저장 강제 로직 추가
        SetMainMessageBox("골드가 초기화 되었습니다");
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
        float lerpTime = 1f;

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
