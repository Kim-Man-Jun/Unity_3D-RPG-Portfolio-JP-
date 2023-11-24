using System;
using UnityEngine;

[CreateAssetMenu(fileName = "stat", menuName = "stat/statSO_enemy")]

[System.Serializable]
public class statSO_enemy : ScriptableObject
{
    public int Lv;

    public float MaxHp;
    public float NowHp;

    public float MaxMp;
    public float NowMp;

    public float Atk;

    public float Def;

    public float Spd;

    [Range(0, 1000)]
    public int Gold;

    [Range(0, 1000)]
    public int Exp;

    //Save & Load ฐüทร
    public int enemyCount;
}
