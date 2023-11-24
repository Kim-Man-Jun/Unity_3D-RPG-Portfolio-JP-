using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static basicMaterialItemSO;

[CreateAssetMenu(fileName = "New Parts", menuName = "Item/equipable_Item")]
public class equipableItemSO : ScriptableObject
{
    //무기, 방어구, 반지
    public enum Parts
    {
        hand,
        body,
        Finger
    };

    public int ID => GetInstanceID();

    [field: SerializeField]
    public string title { get; set; }

    [field: SerializeField]
    public Sprite itemImage { get; set; }

    [field: SerializeField]
    public float quality { get; set; }

    [field: SerializeField]
    public Effect effect1 { get; set; }

    [field: SerializeField]
    public Effect effect2 { get; set; }

    [field: SerializeField]
    public Effect effect3 { get; set; }

    [field: SerializeField]
    public Effect effect4 { get; set; }

    [field: SerializeField]
    public Speccificity specificity1 { get; set; }

    [field: SerializeField]
    public Speccificity specificity2 { get; set; }

    [field: SerializeField]
    public Speccificity specificity3 { get; set; }

    [field: SerializeField]
    public Speccificity specificity4 { get; set; }

    [field: SerializeField]
    public Parts parts { get; set; }
}
