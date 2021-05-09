using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    private int BuffStats = 0; // 1 회복 2 공격력 증가 3 방어력 증가
    private float BuffValue;

    public void setBuff(int BuffStats, float BuffValue)
    {
        this.BuffStats = BuffStats;
        this.BuffValue = BuffValue;
    }

    public int getBuffStats()
    {
        return BuffStats;
    }

    public float getBuffValue()
    {
        return BuffValue;
    }
}
