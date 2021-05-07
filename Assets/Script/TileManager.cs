using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    private int getMonsterStats = 0; // 0 아무것도 없음 1 처음 배치된 몬스터 2 배치한 몬스터 3 회복/버프
    private int number;

    public void setMonsterFlag(int getMonsterStats, int number)
    {
        this.getMonsterStats = getMonsterStats;
        this.number = number;
    }

    public int getMonsterFlag()
    {
        return getMonsterStats;
    }

    public int getnumber()
    {
        return number;
    }
}
