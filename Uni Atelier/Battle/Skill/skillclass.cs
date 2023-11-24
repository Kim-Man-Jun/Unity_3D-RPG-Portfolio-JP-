using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillclass
{
    //스킬명
    public string skillName { get; set; }

    //스킬 마나 드는 정도
    public int skillCost { get; set; }

    //스킬 효과
    public string skillEffect { get; set; }

    //스킬 설명
    public string skillDescription { get; set; }

    //스킬 데미지나 효과 배율 정도
    public float skillmagnification { get; set; }

    //0일 때 상대방 단일 대상, 1일 때 상대방 전체 대상, 2일 때 자기자신, 3일 때 아군 전체
    public int skillSpecify { get; set; }
}
