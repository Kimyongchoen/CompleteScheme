using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Levelinfo : ScriptableObject
{

    public LevelInfo[] levelInfo; //케릭터 정보

    [System.Serializable]
    public struct LevelInfo
    {
        public int Level; //레벨
        public float experience;//필요 경험치
        public int Stat; //추가 스텟
    }

}
