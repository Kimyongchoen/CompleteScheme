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

    public int AttackDamageUp; //공격력 증가 버프
    public int DefenseUp; //방어력 증가 버프

    public int[] weapon; //무기
    public int[] weaponGold; //무기 가격 & 강화 비

    public int[] Armor; //갑옷
    public int[] ArmorGold; //갑옷 가격 & 강화 비

    public int[] hat; //투구
    public int[] hatGold; //투구 가격 & 강화 비

    public float[] Gloves; //장갑
    public int[] GlovesGold; //장갑 가격 & 강화 비

    public float[] Boots; //부츠
    public int[] BootsGold; //부츠 가격 & 강화 비

    public float[] Cloak; //망토
    public int[] CloakGold; //망토 가격 & 강화 비
}
