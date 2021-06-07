using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private Text ItemBtnText;

    public void SelectItme(string ItemName)
    {
        switch (ItemName)
        {
            case "Weapon":
                SelectItemImg.sprite = WeaponImg.sprite;//선택한 장비에 따라 이미지 변경

                if (playerStats.stats[0].Weapon >= 11)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Weapon + " ";
                    ItmeStats.text = "공격력 +" + itemStats.Weapon[playerStats.stats[0].Weapon];
                    ItmeGold.text = "강화 불가";
                    break;
                }

                ItmeName.text = "+0 ";
                ItmeStats.text = "공격력 +"+ itemStats.Weapon[0];
                ItemBtnText.text = "구 매";
                ItmeGold.text = itemStats.WeaponGold[0] + " 원";

                if (playerStats.stats[0].Weapon >= 0)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Weapon + " ";
                    ItmeStats.text = "공격력 +"+ itemStats.Weapon[playerStats.stats[0].Weapon + 1];
                    ItemBtnText.text = "강 화";
                    ItmeGold.text = itemStats.WeaponGold[playerStats.stats[0].Weapon + 1] + " 원";
                }
                ItmeName.text += itemStats.WeaponName;
                
                break;
            case "Armor":
                SelectItemImg.sprite = ArmorImg.sprite;//선택한 장비에 따라 이미지 변경

                if (playerStats.stats[0].Armor >= 11)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Armor + " ";
                    ItmeStats.text = "방어력 +" + itemStats.Armor[playerStats.stats[0].Armor];
                    ItmeGold.text = "강화 불가";
                    break;
                }

                ItmeName.text = "+0 ";
                ItmeStats.text = "방어력 +" + itemStats.Armor[0];
                ItemBtnText.text = "구 매";
                ItmeGold.text = itemStats.ArmorGold[0] + " 원";

                if (playerStats.stats[0].Armor >= 0)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Armor + " ";
                    ItmeStats.text = "방어력 +" + itemStats.Armor[playerStats.stats[0].Armor + 1];
                    ItemBtnText.text = "강 화";
                    ItmeGold.text = itemStats.ArmorGold[playerStats.stats[0].Armor + 1] + " 원";
                }
                ItmeName.text += itemStats.ArmorName;
                break;
            case "Hat":
                SelectItemImg.sprite = HatImg.sprite;//선택한 장비에 따라 이미지 변경

                if (playerStats.stats[0].Hat >= 11)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Hat + " ";
                    ItmeStats.text = "체력 +" + itemStats.Hat[playerStats.stats[0].Hat];
                    ItmeGold.text = "강화 불가";
                    break;
                }

                ItmeName.text = "+0 ";
                ItmeStats.text = "체력 +" + itemStats.Hat[0];
                ItemBtnText.text = "구 매";
                ItmeGold.text = itemStats.HatGold[0] + " 원";

                if (playerStats.stats[0].Hat >= 0)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Hat + " ";
                    ItmeStats.text = "체력 +" + itemStats.Hat[playerStats.stats[0].Hat + 1];
                    ItemBtnText.text = "강 화";
                    ItmeGold.text = itemStats.HatGold[playerStats.stats[0].Hat + 1] + " 원";
                }
                ItmeName.text += itemStats.HatName;
                break;
            case "Gloves":
                SelectItemImg.sprite = GlovesImg.sprite;//선택한 장비에 따라 이미지 변경

                if (playerStats.stats[0].Gloves >= 11)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Gloves + " ";
                    ItmeStats.text = "치명타 확율 +" + itemStats.Gloves[playerStats.stats[0].Gloves];
                    ItmeGold.text = "강화 불가";
                    break;
                }

                ItmeName.text = "+0 ";
                ItmeStats.text = "치명타 확율 +" + itemStats.Gloves[0];
                ItemBtnText.text = "구 매";
                ItmeGold.text = itemStats.GlovesGold[0] + " 원";

                if (playerStats.stats[0].Gloves >= 0)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Gloves + " ";
                    ItmeStats.text = "치명타 확율 +" + itemStats.Gloves[playerStats.stats[0].Gloves + 1];
                    ItemBtnText.text = "강 화";
                    ItmeGold.text = itemStats.GlovesGold[playerStats.stats[0].Gloves + 1] + " 원";
                }
                ItmeName.text += itemStats.GlovesName;
                break;
            case "Boots":
                SelectItemImg.sprite = BootsImg.sprite;//선택한 장비에 따라 이미지 변경

                if (playerStats.stats[0].Boots >= 11)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Boots + " ";
                    ItmeStats.text = "회피력 +" + itemStats.Boots[playerStats.stats[0].Boots];
                    ItmeGold.text = "강화 불가";
                    break;
                }

                ItmeName.text = "+0 ";
                ItmeStats.text = "회피력 +" + itemStats.Boots[0];
                ItemBtnText.text = "구 매";
                ItmeGold.text = itemStats.BootsGold[0] + " 원";

                if (playerStats.stats[0].Boots >= 0)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Boots + " ";
                    ItmeStats.text = "회피력 +" + itemStats.Boots[playerStats.stats[0].Boots + 1];
                    ItemBtnText.text = "강 화";
                    ItmeGold.text = itemStats.BootsGold[playerStats.stats[0].Boots + 1] + " 원";
                }
                ItmeName.text += itemStats.BootsName;
                break;
            case "Shield":
                SelectItemImg.sprite = ShieldImg.sprite;//선택한 장비에 따라 이미지 변경

                if (playerStats.stats[0].Shield >= 11)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Shield + " ";
                    ItmeStats.text = "체력 흡혈 +" + itemStats.Shield[playerStats.stats[0].Shield];
                    ItmeGold.text = "강화 불가";
                    break;
                }

                ItmeName.text = "+0 ";
                ItmeStats.text = "체력 흡혈 +" + itemStats.Shield[0];
                ItemBtnText.text = "구 매";
                ItmeGold.text = itemStats.ShieldGold[0] + " 원";

                if (playerStats.stats[0].Shield >= 0)
                {
                    ItmeName.text = "+" + playerStats.stats[0].Shield + " ";
                    ItmeStats.text = "체력 흡혈 +" + itemStats.Shield[playerStats.stats[0].Shield + 1];
                    ItemBtnText.text = "강 화";
                    ItmeGold.text = itemStats.ShieldGold[playerStats.stats[0].Shield + 1] + " 원";
                }
                ItmeName.text += itemStats.ShieldName;
                break;
            default:
                break;
        }
        
    }
}
