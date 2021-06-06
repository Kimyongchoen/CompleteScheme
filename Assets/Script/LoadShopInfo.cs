using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadShopInfo : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;//플래이어 정보

    [SerializeField]
    private ItemStats itemStats;//아이템 정보

    //플레이어정보
    [SerializeField]
    private Text Level;
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
/*
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
    private Text ItemBtnText;*/

    //타일 구매
    [SerializeField]
    private Text RecoveryCnt;
    [SerializeField]
    private Text RecoveryGold;

    [SerializeField]
    private Text AttackDamageUpCnt;
    [SerializeField]
    private Text AttackDamageUpGold;

    [SerializeField]
    private Text DefenseUpCnt;
    [SerializeField]
    private Text DefenseUpGold;

    [SerializeField]
    private Text MonsterCnt;
    [SerializeField]
    private Text MonsterGold;

    // Start is called before the first frame update
    void Start()
    {
        Level.text = "Level " + playerStats.stats[0].level.ToString();
        Gold.text = "Gold " + playerStats.stats[0].gold.ToString();

        //투구
        if (playerStats.stats[0].hat >= 0)
        {
            Color color = HatImg.color;
            color.a = 1;
            HatImg.color = color;
            if (playerStats.stats[0].hat > 0)
            {
                HatPlus.text = "+" + playerStats.stats[0].hat.ToString();
            }
        }

        //무기
        if (playerStats.stats[0].weapon >= 0)
        {
            Color color = WeaponImg.color;
            color.a = 1;
            WeaponImg.color = color;
            if (playerStats.stats[0].weapon > 0)
            {
                WeaponPlus.text = "+" + playerStats.stats[0].weapon.ToString();
            }
        }

        //갑옷
        if (playerStats.stats[0].Armor >= 0)
        {
            Color color = ArmorImg.color;
            color.a = 1;
            ArmorImg.color = color;
            if (playerStats.stats[0].Armor > 0)
            {
                ArmorPlus.text = "+" + playerStats.stats[0].Armor.ToString();
            }
        }

        //장갑
        if (playerStats.stats[0].Gloves >= 0)
        {
            Color color = GlovesImg.color;
            color.a = 1;
            GlovesImg.color = color;
            if (playerStats.stats[0].Gloves > 0)
            {
                GlovesPlus.text = "+" + playerStats.stats[0].Gloves.ToString();
            }
        }

        //망토 -> 방패
        if (playerStats.stats[0].Cloak >= 0)
        {
            Color color = CloakImg.color;
            color.a = 1;
            CloakImg.color = color;
            if (playerStats.stats[0].Cloak > 0)
            {
                CloakPlus.text = "+" + playerStats.stats[0].Cloak.ToString();
            }
        }

        //신발
        if (playerStats.stats[0].Boots >= 0)
        {
            Color color = BootsImg.color;
            color.a = 1;
            BootsImg.color = color;
            if (playerStats.stats[0].Boots > 0)
            {
                BootsPlus.text = "+" + playerStats.stats[0].Boots.ToString();
            }
        }

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
