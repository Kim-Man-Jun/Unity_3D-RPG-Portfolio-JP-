using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new CombineList", menuName = "Alchemy/new CombineList")]
public class CombineListSO : ScriptableObject
{
    [field: SerializeField]
    public string title { get; set; }

    [field: SerializeField]
    public Sprite itemImage { get; set; }

    [field: SerializeField]
    [field: TextArea]
    public string description { get; set; }

    [field: SerializeField]
    public basicMaterialItemSO.Effect effect1 { get; set; }
    [field: SerializeField]
    public basicMaterialItemSO.Effect effect2 { get; set; }
    [field: SerializeField]
    public basicMaterialItemSO.Effect effect3 { get; set; }
    [field: SerializeField]
    public basicMaterialItemSO.Effect effect4 { get; set; }

    [field: SerializeField]
    public CombineMaterial material1 { get; set; }
    [field: SerializeField]
    public CombineMaterial material2 { get; set; }
    [field: SerializeField]
    public CombineMaterial material3 { get; set; }
    [field: SerializeField]
    public CombineMaterial material4 { get; set; }

    public enum CombineMaterial
    {
        none,
        water,
        plant,
        stone,
        ore,
        fuel,
        monster,
        mystery
    };
}
