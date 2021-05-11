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


}
