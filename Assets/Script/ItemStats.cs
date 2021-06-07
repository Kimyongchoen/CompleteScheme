using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemStats : ScriptableObject
{
    public int RecoveryCnt; //회복 타일 갯수
    public int AttackDamageUpCnt; //공격력증가 타일 갯수
    public int DefenseUpCnt; //방어력증가 타일 갯수
    public int MonsterCnt; //몬스터 타일 갯수

    public int RecoveryGold; //회복 타일 가격
    public int AttackDamageUpGold; //공격력증가 타일 가격
    public int DefenseUpGold; //방어력증가 타일 가격
    public int MonsterGold; //몬스터 타일 가격

    public int AttackDamageUp; //공격력 증가 버프
    public int DefenseUp; //방어력 증가 버프

    public int[] Weapon; //무기
    public string WeaponName; //무기
    public int[] WeaponGold; //무기 가격 & 강화 비

    public int[] Armor; //갑옷
    public string ArmorName; //갑옷
    public int[] ArmorGold; //갑옷 가격 & 강화 비

    public int[] Hat; //투구
    public string HatName; //투구
    public int[] HatGold; //투구 가격 & 강화 비

    public float[] Gloves; //장갑
    public string GlovesName; //장갑
    public int[] GlovesGold; //장갑 가격 & 강화 비

    public float[] Boots; //부츠
    public string BootsName; //부츠
    public int[] BootsGold; //부츠 가격 & 강화 비

    public float[] Shield; //방패
    public string ShieldName; //방패
    public int[] ShieldGold; //방패 가격 & 강화 비
}
