using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using static basicMaterialItemSO;

[Serializable]
[CreateAssetMenu(fileName = "New SpendableItem", menuName = "Item/Spendable_Item")]
public class spendableItemSO : ScriptableObject
{
    //프람, 프람베, 회복 포션 3종
    public enum Target
    {
        none,
        //아군
        ally,
        //적군
        enemy,
        //적군 전체
        enemyAll,
        //장착 아이템
        equipable
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
    public Target target { get; set; }

    [field: SerializeField]
    public float DmgValue { get; set; }

    [field: SerializeField]
    public float ResValue { get; set; }

    [field: SerializeField]
    public float atkValue { get; set; }

    [field: SerializeField]
    public float defValue { get; set; }

    [field: SerializeField]
    public float spdValue { get; set; }


    public static spendableItemSO CreateItemAssetInstance(string itemName, Sprite itemImage, float itemquality,
        Effect effect1, Effect effect2, Effect effect3, Effect effect4,
        Speccificity specificity1, Speccificity specificity2,
        Speccificity specificity3, Speccificity specificity4)
    {
        var itemAsset = CreateInstance<spendableItemSO>();

        itemAsset.title = itemName;
        itemAsset.itemImage = itemImage;
        itemAsset.quality = itemquality;
        itemAsset.effect1 = effect1;
        itemAsset.effect2 = effect2;
        itemAsset.effect3 = effect3;
        itemAsset.effect4 = effect4;
        itemAsset.specificity1 = specificity1;
        itemAsset.specificity2 = specificity2;
        itemAsset.specificity3 = specificity3;
        itemAsset.specificity4 = specificity4;

        //아이템 이름에 따라 사용 대상을 정해줌
        switch (itemName)
        {
            case "프람":
                itemAsset.target = Target.enemy;
                break;
            case "프람베":
                itemAsset.target = Target.enemyAll;
                break;
            case "체력 물약":
                itemAsset.target = Target.ally;
                break;
            case "마나 물약":
                itemAsset.target = Target.ally;
                break;
            case "종합 물약":
                itemAsset.target = Target.ally;
                break;
            case "루비 박힌 완드":
                itemAsset.target = Target.equipable;
                break;
            case "가벼운 갑옷":
                itemAsset.target = Target.equipable;
                break;
            case "그나데링":
                itemAsset.target = Target.equipable;
                break;
        }

        //아이템 이름에 따라 데미지 및 회복량, 공격력 방어력 등을 조정함
        switch (itemName)
        {
            case "프람":
                itemAsset.DmgValue = 30;

                if (effect1 != Effect.none)
                {
                    itemAsset.DmgValue *= 1.1f;

                    if (effect2 != Effect.none)
                    {
                        itemAsset.DmgValue *= 1.2f;

                        if (effect3 != Effect.none)
                        {
                            itemAsset.DmgValue *= 1.4f;
                        }
                    }
                }
                break;

            case "프람베":
                itemAsset.DmgValue = 40;

                if (effect1 != Effect.none)
                {
                    itemAsset.DmgValue *= 1.1f;

                    if (effect2 != Effect.none)
                    {
                        itemAsset.DmgValue *= 1.2f;

                        if (effect3 != Effect.none)
                        {
                            itemAsset.DmgValue *= 1.2f;

                            if (effect4 != Effect.none)
                            {
                                itemAsset.DmgValue *= 1.3f;
                            }
                        }
                    }
                }
                break;

            case "체력 물약":
                itemAsset.ResValue = 10;

                if (effect1 != Effect.none)
                {
                    itemAsset.ResValue *= 2f;

                    if (effect2 != Effect.none)
                    {
                        itemAsset.ResValue *= 1.5f;

                        if (effect3 != Effect.none)
                        {
                            itemAsset.ResValue *= 2f;
                        }
                    }
                }
                break;

            case "마나 물약":
                itemAsset.ResValue = 10;

                if (effect1 != Effect.none)
                {
                    itemAsset.ResValue *= 2f;

                    if (effect2 != Effect.none)
                    {
                        itemAsset.ResValue *= 1.5f;

                        if (effect3 != Effect.none)
                        {
                            itemAsset.ResValue *= 2f;
                        }
                    }
                }
                break;

            case "종합 물약":
                itemAsset.ResValue = 20;

                if (effect1 != Effect.none)
                {
                    itemAsset.ResValue *= 2f;

                    if (effect2 != Effect.none)
                    {
                        itemAsset.ResValue *= 1.5f;

                        if (effect3 != Effect.none)
                        {
                            itemAsset.ResValue *= 1.5f;

                            if (effect4 != Effect.none)
                            {
                                itemAsset.ResValue *= 2f;
                            }
                        }
                    }
                }
                break;

            case "루비 박힌 완드":
                itemAsset.atkValue = 10;

                if (effect1 != Effect.none)
                {
                    itemAsset.atkValue *= 1.5f;

                    if (effect2 != Effect.none)
                    {
                        itemAsset.atkValue *= 2f;

                        if (effect3 != Effect.none)
                        {
                            itemAsset.atkValue *= 2f;

                            if (effect4 != Effect.none)
                            {
                                itemAsset.atkValue *= 1.5f;
                            }
                        }
                    }
                }
                break;

            case "가벼운 갑옷":
                itemAsset.defValue = 10;

                if (effect1 != Effect.none)
                {
                    itemAsset.defValue *= 1.5f;

                    if (effect2 != Effect.none)
                    {
                        itemAsset.defValue *= 2f;

                        if (effect3 != Effect.none)
                        {
                            itemAsset.defValue *= 2f;
                        }
                    }
                }
                break;

            case "그나데링":
                itemAsset.atkValue = 10;
                itemAsset.defValue = 10;
                itemAsset.spdValue = 10;

                if (effect1 != Effect.none)
                {
                    itemAsset.atkValue *= 2f;

                    if (effect2 != Effect.none)
                    {
                        itemAsset.defValue *= 2f;

                        if (effect3 != Effect.none)
                        {
                            itemAsset.spdValue *= 2f;

                            if (effect4 != Effect.none)
                            {
                                itemAsset.atkValue *= 2f;
                            }
                        }
                    }
                }
                break;
        }

        for (int i = 1; i < 5; i++)
        {
            var specificittyField = itemAsset.GetType().GetField($"specificity{i}");

            if (specificittyField != null)
            {
                switch (specificittyField.Name)
                {
                    case "데미지 수치 증가":
                        itemAsset.DmgValue *= 1.2f;
                        return itemAsset;
                    case "회복 수치 증가":
                        itemAsset.ResValue *= 1.2f;
                        return itemAsset;
                    case "공격력 수치 증가":
                        itemAsset.atkValue += 10f;
                        return itemAsset;
                    case "방어력 수치 증가":
                        itemAsset.defValue += 10f;
                        return itemAsset;
                    case "속도 수치 증가":
                        itemAsset.spdValue += 10f;
                        return itemAsset;
                    case "공방 수치 증가":
                        itemAsset.atkValue += 10f;
                        itemAsset.defValue += 10f;
                        return itemAsset;
                    case "공속 수치 증가":
                        itemAsset.atkValue += 10f;
                        itemAsset.spdValue += 10f;
                        return itemAsset;
                    case "방속 수치 증가":
                        itemAsset.defValue += 10f;
                        itemAsset.spdValue += 10f;
                        return itemAsset;
                    case "전능력 수치 증가":
                        itemAsset.atkValue += 10f;
                        itemAsset.defValue += 10f;
                        itemAsset.spdValue += 10f;
                        return itemAsset;
                }
            }
        }

        #region 품질에 따라 데미지 수치 변동 주기
        //품질에 따라 데미지 값 변경
        itemAsset.DmgValue *= itemAsset.quality / 100;
        //float에 딸린 소숫점 자리를 반올림으로 삭제
        itemAsset.DmgValue = Mathf.RoundToInt(itemAsset.DmgValue);

        itemAsset.ResValue *= itemAsset.quality / 100;
        itemAsset.ResValue = Mathf.RoundToInt(itemAsset.ResValue);

        itemAsset.atkValue *= itemAsset.quality / 100;
        itemAsset.atkValue = Mathf.RoundToInt(itemAsset.atkValue);

        itemAsset.defValue *= itemAsset.quality / 100;
        itemAsset.defValue = Mathf.RoundToInt(itemAsset.defValue);

        itemAsset.spdValue *= itemAsset.quality / 100;
        itemAsset.spdValue = Mathf.RoundToInt(itemAsset.spdValue);
        #endregion

        AssetDatabase.CreateAsset(itemAsset, $"Assets/Resources/spendableItem/{itemName}.asset");

        AssetDatabase.Refresh();

        return itemAsset;
    }

    public static void LoadItemAssetInstance(string itemName)
    {
        var itemAsset = AssetDatabase.LoadAssetAtPath
            <spendableItemSO>($"Assets/Resources/spendableItem/{itemName}.asset");
    }

    public static Effect GetEffectEnumFromText(TMP_Text textContent)
    {
        switch (textContent.text)
        {
            case "데미지 소":
                return Effect.damageSmall;
            case "데미지 중":
                return Effect.damageMedium;
            case "데미지 대":
                return Effect.damageLarge;
            case "데미지 특대":
                return Effect.damageExtraLarge;

            case "회복력 소":
                return Effect.restoreSmall;
            case "회복력 중":
                return Effect.restoreMedium;
            case "회복력 대":
                return Effect.restoreLarge;
            case "회복력 특대":
                return Effect.resotreExtraLarge;

            case "공격력 증가 소":
                return Effect.atkSmall;
            case "공격력 증가 중":
                return Effect.atkMedium;
            case "공격력 증가 대":
                return Effect.atkLarge;
            case "공격력 증가 특대":
                return Effect.atkExtraLarge;

            case "방어력 증가 소":
                return Effect.defSmall;
            case "방어력 증가 중":
                return Effect.defMedium;
            case "방어력 증가 대":
                return Effect.defLarge;
            case "방어력 증가 특":
                return Effect.defExtraLarge;

            case "속도 증가 소":
                return Effect.spdSmall;
            case "속도 증가 중":
                return Effect.spdMedium;
            case "속도 증가 대":
                return Effect.spdLarge;
            case "속도 증가 특대":
                return Effect.spdExtraLarge;

            default:
                return Effect.none;
        }
    }

    public static Speccificity GetSpeccificityEnumFromText(TMP_Text textContent)
    {
        switch (textContent.text)
        {
            case "데미지 수치 증가":
                return Speccificity.damagePowerUp;
            case "회복 수치 증가":
                return Speccificity.restorePowerUp;

            case "공격력 수치 증가":
                return Speccificity.atkUp;
            case "방어력 수치 증가":
                return Speccificity.defUp;
            case "속도 수치 증가":
                return Speccificity.spdUp;
            case "공방 수치 증가":
                return Speccificity.atkdefUp;
            case "공속 수치 증가":
                return Speccificity.atkspdUp;
            case "방속 수치 증가":
                return Speccificity.defspdUp;
            case "전능력 수치 증가":
                return Speccificity.allUp;

            default:
                return Speccificity.none;
        }
    }
}
