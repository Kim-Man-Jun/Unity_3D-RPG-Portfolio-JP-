using UnityEngine;

[CreateAssetMenu(fileName = "stat", menuName = "stat/statSO_player")]

[System.Serializable]
public class statSO_player : ScriptableObject
{
    public int Lv;

    public int MaxExp;
    public int nowExp;

    public float MaxHp;
    public float NowHp;

    public float MaxMp;
    public float NowMp;

    public int Atk;

    public int Def;

    public int Spd;

    //플레이어 위치
    public string sceneName;
    public Vector3 playerTransform;

    public Vector3 playerBattleInTransform;

    public bool WeaponEquip;

    public bool ArmorEquip;

    public bool RingEquip;
}