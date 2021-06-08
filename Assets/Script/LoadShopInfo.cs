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
    private Image ShieldImg;
    [SerializeField]
    private Text ShieldPlus;

    [SerializeField]
    private Image BootsImg;
    [SerializeField]
    private Text BootsPlus;


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
        LoadInfo();
    }

    public void LoadInfo()
    {
        Level.text = "Level " + playerStats.stats[0].level.ToString();
        Gold.text = "Gold " + playerStats.stats[0].gold.ToString();

        //투구
        if (playerStats.stats[0].Hat >= 0)
        {
            Color color = HatImg.color;
            color.a = 1;
            HatImg.color = color;
            if (playerStats.stats[0].Hat > 0)
            {
                HatPlus.text = "+" + playerStats.stats[0].Hat.ToString();
            }
        }

        //무기
        if (playerStats.stats[0].Weapon >= 0)
        {
            Color color = WeaponImg.color;
            color.a = 1;
            WeaponImg.color = color;
            if (playerStats.stats[0].Weapon > 0)
            {
                WeaponPlus.text = "+" + playerStats.stats[0].Weapon.ToString();
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
        if (playerStats.stats[0].Shield >= 0)
        {
            Color color = ShieldImg.color;
            color.a = 1;
            ShieldImg.color = color;
            if (playerStats.stats[0].Shield > 0)
            {
                ShieldPlus.text = "+" + playerStats.stats[0].Shield.ToString();
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


        RecoveryCnt.text = itemStats.RecoveryCnt.ToString();
        RecoveryGold.text = itemStats.RecoveryGold.ToString() + " 원";

        AttackDamageUpCnt.text = itemStats.AttackDamageUpCnt.ToString();
        AttackDamageUpGold.text = itemStats.AttackDamageUpGold.ToString() + " 원";

        DefenseUpCnt.text = itemStats.DefenseUpCnt.ToString();
        DefenseUpGold.text = itemStats.DefenseUpGold.ToString() + " 원";

        MonsterCnt.text = itemStats.MonsterCnt.ToString();
        MonsterGold.text = itemStats.MonsterGold.ToString() + " 원";

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
