using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillclass
{
    //��ų��
    public string skillName { get; set; }

    //��ų ���� ��� ����
    public int skillCost { get; set; }

    //��ų ȿ��
    public string skillEffect { get; set; }

    //��ų ����
    public string skillDescription { get; set; }

    //��ų �������� ȿ�� ���� ����
    public float skillmagnification { get; set; }

    //0�� �� ���� ���� ���, 1�� �� ���� ��ü ���, 2�� �� �ڱ��ڽ�, 3�� �� �Ʊ� ��ü
    public int skillSpecify { get; set; }
}
