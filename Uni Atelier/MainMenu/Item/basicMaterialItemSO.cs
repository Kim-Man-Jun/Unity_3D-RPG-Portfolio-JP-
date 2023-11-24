using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Basic_Material")]
public class basicMaterialItemSO : ScriptableObject
{
    //아이템 효과
    public enum Effect
    {
        none,

        damageSmall,
        damageMedium,
        damageLarge,
        damageExtraLarge,

        restoreSmall,
        restoreMedium,
        restoreLarge,
        resotreExtraLarge,

        atkSmall,
        atkMedium,
        atkLarge,
        atkExtraLarge,

        defSmall,
        defMedium,
        defLarge,
        defExtraLarge,

        spdSmall,
        spdMedium,
        spdLarge,
        spdExtraLarge
    };

    //아이템 특성
    public enum Speccificity
    {
        none,
        damagePowerUp,
        restorePowerUp,
        atkUp,
        defUp,
        spdUp,
        atkdefUp,
        defspdUp,
        atkspdUp,
        allUp
    };

    //아이템 속성
    public enum Element
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

    public int ID => GetInstanceID();

    [field: SerializeField]
    public string title { get; set; }

    [field: SerializeField]
    public Sprite itemImage { get; set; }

    [field: SerializeField]
    public float quality;

    [field: SerializeField]
    public Effect effect1 { get; set; }

    [field: SerializeField]
    public Effect effect2 { get; set; }

    [field: SerializeField]
    public Effect effect3 { get; set; }

    [field: SerializeField]
    public Effect effect4 { get; set; }

    [field: SerializeField]
    public Speccificity specificity1;

    [field: SerializeField]
    public Speccificity specificity2;

    [field: SerializeField]
    public Speccificity specificity3;
    [field: SerializeField]
    public Speccificity specificity4;

    [field: SerializeField]
    public Element element1 { get; set; }

    [field: SerializeField]
    public Element element2 { get; set; }

    [field: SerializeField]
    public Element element3 { get; set; }

    [field: SerializeField]
    public Element element4 { get; set; }

    public void InitializeRandomValues()
    {
        System.Random random = new System.Random();

        quality = random.Next(0, 31);

        specificity1 = (Speccificity)random.Next(0, 10);
        specificity2 = (Speccificity)random.Next(0, 10);
        specificity3 = (Speccificity)random.Next(0, 10);
        specificity4 = (Speccificity)random.Next(0, 10);
    }
}
