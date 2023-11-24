using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using static basicMaterialItemSO;

[Serializable]
[CreateAssetMenu(fileName = "New SpendableItem", menuName = "Item/Spendable_Item")]
public class spendableItemSO : ScriptableObject
{
    //����, ������, ȸ�� ���� 3��
    public enum Target
    {
        none,
        //�Ʊ�
        ally,
        //����
        enemy,
        //���� ��ü
        enemyAll,
        //���� ������
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

        //������ �̸��� ���� ��� ����� ������
        switch (itemName)
        {
            case "����":
                itemAsset.target = Target.enemy;
                break;
            case "������":
                itemAsset.target = Target.enemyAll;
                break;
            case "ü�� ����":
                itemAsset.target = Target.ally;
                break;
            case "���� ����":
                itemAsset.target = Target.ally;
                break;
            case "���� ����":
                itemAsset.target = Target.ally;
                break;
            case "��� ���� �ϵ�":
                itemAsset.target = Target.equipable;
                break;
            case "������ ����":
                itemAsset.target = Target.equipable;
                break;
            case "�׳�����":
                itemAsset.target = Target.equipable;
                break;
        }

        //������ �̸��� ���� ������ �� ȸ����, ���ݷ� ���� ���� ������
        switch (itemName)
        {
            case "����":
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

            case "������":
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

            case "ü�� ����":
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

            case "���� ����":
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

            case "���� ����":
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

            case "��� ���� �ϵ�":
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

            case "������ ����":
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

            case "�׳�����":
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
                    case "������ ��ġ ����":
                        itemAsset.DmgValue *= 1.2f;
                        return itemAsset;
                    case "ȸ�� ��ġ ����":
                        itemAsset.ResValue *= 1.2f;
                        return itemAsset;
                    case "���ݷ� ��ġ ����":
                        itemAsset.atkValue += 10f;
                        return itemAsset;
                    case "���� ��ġ ����":
                        itemAsset.defValue += 10f;
                        return itemAsset;
                    case "�ӵ� ��ġ ����":
                        itemAsset.spdValue += 10f;
                        return itemAsset;
                    case "���� ��ġ ����":
                        itemAsset.atkValue += 10f;
                        itemAsset.defValue += 10f;
                        return itemAsset;
                    case "���� ��ġ ����":
                        itemAsset.atkValue += 10f;
                        itemAsset.spdValue += 10f;
                        return itemAsset;
                    case "��� ��ġ ����":
                        itemAsset.defValue += 10f;
                        itemAsset.spdValue += 10f;
                        return itemAsset;
                    case "���ɷ� ��ġ ����":
                        itemAsset.atkValue += 10f;
                        itemAsset.defValue += 10f;
                        itemAsset.spdValue += 10f;
                        return itemAsset;
                }
            }
        }

        #region ǰ���� ���� ������ ��ġ ���� �ֱ�
        //ǰ���� ���� ������ �� ����
        itemAsset.DmgValue *= itemAsset.quality / 100;
        //float�� ���� �Ҽ��� �ڸ��� �ݿø����� ����
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
            case "������ ��":
                return Effect.damageSmall;
            case "������ ��":
                return Effect.damageMedium;
            case "������ ��":
                return Effect.damageLarge;
            case "������ Ư��":
                return Effect.damageExtraLarge;

            case "ȸ���� ��":
                return Effect.restoreSmall;
            case "ȸ���� ��":
                return Effect.restoreMedium;
            case "ȸ���� ��":
                return Effect.restoreLarge;
            case "ȸ���� Ư��":
                return Effect.resotreExtraLarge;

            case "���ݷ� ���� ��":
                return Effect.atkSmall;
            case "���ݷ� ���� ��":
                return Effect.atkMedium;
            case "���ݷ� ���� ��":
                return Effect.atkLarge;
            case "���ݷ� ���� Ư��":
                return Effect.atkExtraLarge;

            case "���� ���� ��":
                return Effect.defSmall;
            case "���� ���� ��":
                return Effect.defMedium;
            case "���� ���� ��":
                return Effect.defLarge;
            case "���� ���� Ư":
                return Effect.defExtraLarge;

            case "�ӵ� ���� ��":
                return Effect.spdSmall;
            case "�ӵ� ���� ��":
                return Effect.spdMedium;
            case "�ӵ� ���� ��":
                return Effect.spdLarge;
            case "�ӵ� ���� Ư��":
                return Effect.spdExtraLarge;

            default:
                return Effect.none;
        }
    }

    public static Speccificity GetSpeccificityEnumFromText(TMP_Text textContent)
    {
        switch (textContent.text)
        {
            case "������ ��ġ ����":
                return Speccificity.damagePowerUp;
            case "ȸ�� ��ġ ����":
                return Speccificity.restorePowerUp;

            case "���ݷ� ��ġ ����":
                return Speccificity.atkUp;
            case "���� ��ġ ����":
                return Speccificity.defUp;
            case "�ӵ� ��ġ ����":
                return Speccificity.spdUp;
            case "���� ��ġ ����":
                return Speccificity.atkdefUp;
            case "���� ��ġ ����":
                return Speccificity.atkspdUp;
            case "��� ��ġ ����":
                return Speccificity.defspdUp;
            case "���ɷ� ��ġ ����":
                return Speccificity.allUp;

            default:
                return Speccificity.none;
        }
    }
}
