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
    private Image CloakImg;
    [SerializeField]
    private Text CloakPlus;

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
            case "weapon":
                SelectItemImg= WeaponImg;
                break;
            case "Armor":
                SelectItemImg = ArmorImg;
                break;
            case "Hat":
                SelectItemImg = HatImg;
                break;
            case "Gloves":
                SelectItemImg = GlovesImg;
                break;
            case "Boots":
                SelectItemImg = BootsImg;
                break;
            case "Cloak":
                SelectItemImg = CloakImg;
                break;
            default:
                break;
        }
        
    }
}
