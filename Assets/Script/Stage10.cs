using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Stage10 : ScriptableObject
{

    public Stage[] stage; //케릭터 정보

    [System.Serializable]
    public struct Stage
    {
        public string StageName;//스테이지명
        public int MonsterNumberMin;//스테이지에 등장할 몬스터 최소값 
        public int MonsterNumberMax;//스테이지에 등장할 몬스터 최대값
        public float experience;//경험치
        public float gold;//골드
    }

}
