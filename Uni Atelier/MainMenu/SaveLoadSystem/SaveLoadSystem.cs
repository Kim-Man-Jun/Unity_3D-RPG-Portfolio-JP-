using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


[Serializable]
public class BasicMaterialItemSave
{
    public string title;
    public float quality = 0;
    public string specificity1;
    public string specificity2;
    public string specificity3;
    public string specificity4;
}

//BasicMaterialItemSave 저장 관리를 위한 컨테이너
[Serializable]
public class BasicMaterialItemSaveContainer
{
    public List<BasicMaterialItemSave> basicItemSaves;

    public BasicMaterialItemSaveContainer(List<BasicMaterialItemSave> saves)
    {
        basicItemSaves = saves;
    }
}

[Serializable]
public class AttackItemSaveContainer
{
    public List<string> attackItemSaveJson;

    public AttackItemSaveContainer(List<string> saves)
    {
        attackItemSaveJson = saves;
    }
}

[Serializable]
public class RestoreItemSaveContainer
{
    public List<string> restoreItemSaveJson;

    public RestoreItemSaveContainer(List<string> saves)
    {
        restoreItemSaveJson = saves;
    }
}

[Serializable]
public class EquipItemSaveContainer
{
    public List<string> equipItemSaveJson;

    public EquipItemSaveContainer(List<string> saves)
    {
        equipItemSaveJson = saves;
    }
}

public class SaveLoadSystem : MonoBehaviour
{
    [Header("Player Related")]
    public Player player;
    public statSO_player playerSO;

    [Header("Inventory Related")]
    public InventorySO MainInven;
    public List<basicMaterialItemSO> basicMaterialItemSO = new List<basicMaterialItemSO>();

    //아이템 퀄리티과 특성 저장
    public List<BasicMaterialItemSave> basicItemSaves = new List<BasicMaterialItemSave>();

    public BattleInventorySO attackBattleInven;
    public List<string> attackItemSave = new List<string>();

    public BattleInventorySO restoreBattleInven;
    public List<string> restoreItemSave = new List<string>();

    public BattleInventorySO equipBattleInven;
    public List<string> equipItemSave = new List<string>();

    [Header("Component Related")]
    public MainMenuSystem mainSys;
    public BattleInventorySystem battleItemInven;

    [Header("Save & Load Related")]
    string path;

    string battleItemPath;

    //아이템 배열 파일 이름
    public string mainfileName = "MainInventoryData.txt";

    //스크립터블 오브젝트 수치 파일 이름
    public string basicItemfileName = "BasicItemData.txt";

    //전투 인벤토리 파일 이름
    public string attackItemfileName = "AttackItemData.txt";
    public string restoreItemfileName = "RestoreItemData.txt";
    public string equipItemfileName = "EquipItemData.txt";

    public string savedSceneName;

    [Header("Enemy Count Related")]
    public static string enemyName;
    public static int enemyCount;
    public static int enemyDiecount;

    private void Awake()
    {
        path = Application.dataPath + "/saveFile/MainInventoryData/";
        battleItemPath = Application.dataPath + "/saveFile/BattleInventoryData/";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        if (!Directory.Exists(battleItemPath))
        {
            Directory.CreateDirectory(battleItemPath);
        }

        //처음 게임을 시작하거나 불러오기를 했을 경우
        if (StartScene.gameStart == true)
        {
            //시작 초기 위치 지정
            playerSO.playerTransform = new Vector3(20.76f, 2.00f, 23.68f);
            PlayerStatLoad();
            PlayerTransformLoad();

            StartScene.gameStart = false;
        }

        else if (StartScene.gameContinue == true)
        {
            PlayLoad();

            StartScene.gameContinue = false;
        }
    }

    #region Save Sequence
    public void PlaySave()
    {
        PlayerStatSave();
        PlayerTransformSave();
        MainInventorySave();
        attackItemInventorySave();
        restoreItemInventorySave();
        equipItemInventorySave();
        weaponEquipSwitchSave();
    }

    public void PlayerStatSave()
    {
        playerSO.Lv = player.Lv;
        playerSO.MaxExp = player.MaxExp;
        playerSO.nowExp = player.nowExp;
        playerSO.MaxHp = player.MaxHp;
        playerSO.NowHp = player.NowHp;
        playerSO.MaxMp = player.MaxMp;
        playerSO.NowMp = player.NowMp;
        playerSO.Atk = player.Atk;
        playerSO.Def = player.Def;
        playerSO.Spd = player.Spd;
    }

    //메인 메뉴 save 버튼
    public void PlayerTransformSave()
    {
        playerSO.sceneName = SceneManager.GetActiveScene().name;
        playerSO.playerTransform = player.transform.position;
        playerSO.playerBattleInTransform = new Vector3(0, 0, 0);
    }

    //전투 들어가기 직전 save
    public void PlayerBattleInTransformSave()
    {
        playerSO.sceneName = SceneManager.GetActiveScene().name;
        playerSO.playerBattleInTransform = player.transform.position;
    }

    public void MainInventorySave()
    {
        //아이템 퀄리티과 특성 저장
        basicItemSaves = new List<BasicMaterialItemSave>();

        for (int i = 0; i < basicMaterialItemSO.Count; i++)
        {
            BasicMaterialItemSave itemSave = new BasicMaterialItemSave
            {
                title = basicMaterialItemSO[i].title,
                quality = basicMaterialItemSO[i].quality,
                specificity1 = basicMaterialItemSO[i].specificity1.ToString(),
                specificity2 = basicMaterialItemSO[i].specificity2.ToString(),
                specificity3 = basicMaterialItemSO[i].specificity3.ToString(),
                specificity4 = basicMaterialItemSO[i].specificity4.ToString()
            };

            basicItemSaves.Add(itemSave);
        }

        string basicItemSavesJson = JsonUtility.ToJson(new BasicMaterialItemSaveContainer(basicItemSaves),
            prettyPrint: true);
        File.WriteAllText(path + basicItemfileName, basicItemSavesJson);

        //인벤토리 아이템 순서 등 저장
        string inventoryJson = JsonUtility.ToJson(MainInven);
        File.WriteAllText(path + mainfileName, inventoryJson);

        AssetDatabase.Refresh();
    }

    public void attackItemInventorySave()
    {
        attackItemSave = new List<string>();

        for (int i = 0; i < attackBattleInven.battleInventoryItems.Count; i++)
        {
            BattleInventoryStruct item = attackBattleInven.battleInventoryItems[i];

            if (!item.IsEmpty)
            {
                attackItemSave.Add(item.battleItem.name);
            }
        }

        AttackItemSaveContainer attackItemSaveContainer
            = new AttackItemSaveContainer(attackItemSave);

        string attackItemInventoryJson = JsonUtility.ToJson(attackItemSaveContainer);
        File.WriteAllText(battleItemPath + attackItemfileName, attackItemInventoryJson);

        AssetDatabase.Refresh();
    }

    public void restoreItemInventorySave()
    {
        restoreItemSave = new List<string>();

        for (int i = 0; i < restoreBattleInven.battleInventoryItems.Count; i++)
        {
            BattleInventoryStruct item = restoreBattleInven.battleInventoryItems[i];

            if (!item.IsEmpty)
            {
                restoreItemSave.Add(item.battleItem.name);
            }
        }

        RestoreItemSaveContainer restoreItemSaveContainer
            = new RestoreItemSaveContainer(restoreItemSave);

        string restoreItemInventoryJson = JsonUtility.ToJson(restoreItemSaveContainer);
        File.WriteAllText(battleItemPath + restoreItemfileName, restoreItemInventoryJson);

        AssetDatabase.Refresh();
    }

    public void equipItemInventorySave()
    {
        equipItemSave = new List<string>();

        for (int i = 0; i < equipBattleInven.battleInventoryItems.Count; i++)
        {
            BattleInventoryStruct item = equipBattleInven.battleInventoryItems[i];

            if (!item.IsEmpty)
            {
                equipItemSave.Add(item.battleItem.name);
            }
        }

        EquipItemSaveContainer equipItemSaveContainer
            = new EquipItemSaveContainer(equipItemSave);

        string equipItemInventoryJson = JsonUtility.ToJson(equipItemSaveContainer);
        File.WriteAllText(battleItemPath + equipItemfileName, equipItemInventoryJson);

        AssetDatabase.Refresh();
    }

    //장비 장착 여부 저장
    public void weaponEquipSwitchSave()
    {
        playerSO.WeaponEquip = battleItemInven.weaponEquip;
        playerSO.ArmorEquip = battleItemInven.armorEquip;
        playerSO.RingEquip = battleItemInven.ringEquip;
    }

    //지팡이 휘둘렀을 때 맞은 대상의 이름과 적 숫자 저장
    public void enemyCountSave(Collider hitEnemy)
    {
        if (hitEnemy.name == "Enemy_Slime")
        {
            enemyName = hitEnemy.name;
            enemyCount = hitEnemy.GetComponent<Enemy_Slime>().enemyCount;
        }

        else if (hitEnemy.name == "Enemy_Cactus")
        {
            enemyName = hitEnemy.name;
            enemyCount = hitEnemy.GetComponent<Enemy_Cactus>().enemyCount;
        }

        else if (hitEnemy.name == "Enemy_Mushroom")
        {
            enemyName = hitEnemy.name;
            enemyCount = hitEnemy.GetComponent<Enemy_Mushroom>().enemyCount;
        }

        else if (hitEnemy.name == "Enemy_Dragon")
        {
            enemyName = hitEnemy.name;
            enemyCount = hitEnemy.GetComponent<Enemy_Dragon>().enemyCount;
        }
    }
    #endregion

    #region Load Sequence
    public void PlayLoad()
    {
        PlayerStatLoad();
        PlayerTransformLoad();
        MainInventoryLoad();
        attackItemInventoryLoad();
        restoreItemInventoryLoad();
        equipItemInventoryLoad();
        weaponEquipSwitchLoad();
    }

    public void PlayerStatLoad()
    {
        player.Lv = playerSO.Lv;
        player.MaxExp = playerSO.MaxExp;
        player.nowExp = playerSO.nowExp;
        player.MaxHp = playerSO.MaxHp;
        player.NowHp = playerSO.NowHp;
        player.MaxMp = playerSO.MaxMp;
        player.NowMp = playerSO.NowMp;
        player.Atk = playerSO.Atk;
        player.Def = playerSO.Def;
        player.Spd = playerSO.Spd;
    }

    public void PlayerTransformLoad()
    {
        savedSceneName = playerSO.sceneName;
        player.transform.position = playerSO.playerTransform;
    }

    public void PlayerBattleInTransformLoad()
    {
        savedSceneName = playerSO.sceneName;
        player.transform.position = playerSO.playerBattleInTransform;
    }


    public void MainInventoryLoad()
    {
        string basicItemData = File.ReadAllText(path + basicItemfileName);
        BasicMaterialItemSaveContainer container
            = JsonUtility.FromJson<BasicMaterialItemSaveContainer>(basicItemData);
        basicItemSaves = container.basicItemSaves;

        for (int i = 0; i < basicItemSaves.Count; i++)
        {
            basicMaterialItemSO[i].quality = basicItemSaves[i].quality;
            basicMaterialItemSO[i].specificity1 =
                (basicMaterialItemSO.Speccificity)Enum.Parse(typeof(basicMaterialItemSO.Speccificity),
                basicItemSaves[i].specificity1);
            basicMaterialItemSO[i].specificity2 =
                (basicMaterialItemSO.Speccificity)Enum.Parse(typeof(basicMaterialItemSO.Speccificity),
                basicItemSaves[i].specificity2);
            basicMaterialItemSO[i].specificity3 =
                (basicMaterialItemSO.Speccificity)Enum.Parse(typeof(basicMaterialItemSO.Speccificity),
                basicItemSaves[i].specificity3);
            basicMaterialItemSO[i].specificity4 =
                (basicMaterialItemSO.Speccificity)Enum.Parse(typeof(basicMaterialItemSO.Speccificity),
                basicItemSaves[i].specificity4);
        }

        string basicItemLoadJson = File.ReadAllText(path + basicItemfileName);
        JsonUtility.FromJsonOverwrite(basicItemLoadJson, basicMaterialItemSO);

        string inventoryJson = File.ReadAllText(path + mainfileName);
        JsonUtility.FromJsonOverwrite(inventoryJson, MainInven);

        AssetDatabase.Refresh();
    }

    public void attackItemInventoryLoad()
    {
        attackBattleInven.battleInventoryItems.Clear();

        attackBattleInven.Initialized();

        string attackItemInventoryJson = File.ReadAllText(battleItemPath + attackItemfileName);

        AttackItemSaveContainer attackItemSaveContainer
            = JsonUtility.FromJson<AttackItemSaveContainer>(attackItemInventoryJson);

        attackItemSave = attackItemSaveContainer.attackItemSaveJson;

        for (int i = 0; i < attackItemSave.Count; i++)
        {
            attackBattleInven.
                AddItem(Resources.Load<spendableItemSO>($"spendableItem/{attackItemSave[i]}"));
        }

        AssetDatabase.Refresh();
    }

    public void restoreItemInventoryLoad()
    {
        restoreBattleInven.battleInventoryItems.Clear();

        restoreBattleInven.Initialized();

        string restoreItemInventoryJson = File.ReadAllText(battleItemPath + restoreItemfileName);

        RestoreItemSaveContainer restoreItemSaveContainer
            = JsonUtility.FromJson<RestoreItemSaveContainer>(restoreItemInventoryJson);

        restoreItemSave = restoreItemSaveContainer.restoreItemSaveJson;

        for (int i = 0; i < restoreItemSave.Count; i++)
        {
            restoreBattleInven.
                AddItem(Resources.Load<spendableItemSO>($"spendableItem/{restoreItemSave[i]}"));
        }

        AssetDatabase.Refresh();
    }

    public void equipItemInventoryLoad()
    {
        equipBattleInven.battleInventoryItems.Clear();

        equipBattleInven.Initialized();

        string equipItemInventoryJson = File.ReadAllText(battleItemPath + equipItemfileName);

        EquipItemSaveContainer equipItemSaveContainer
            = JsonUtility.FromJson<EquipItemSaveContainer>(equipItemInventoryJson);

        equipItemSave = equipItemSaveContainer.equipItemSaveJson;

        for (int i = 0; i < equipItemSave.Count; i++)
        {
            equipBattleInven.
                AddItem(Resources.Load<spendableItemSO>($"spendableItem/{equipItemSave[i]}"));
        }

        AssetDatabase.Refresh();
    }

    public void weaponEquipSwitchLoad()
    {
        battleItemInven.weaponEquip = playerSO.WeaponEquip;
        battleItemInven.armorEquip = playerSO.ArmorEquip;
        battleItemInven.ringEquip = playerSO.RingEquip;
    }

    #endregion

    public void InventoryHide()
    {
        if (mainSys.menuButton != null && mainSys.activeButton != null)
        {
            foreach (GameObject go in mainSys.menuButton)
            {
                go.SetActive(true);
            }

            EffectSoundManager.instance.MainmenuButtonBack();
            gameObject.SetActive(false);
        }
    }

    public void saveLoadButtonSound()
    {
        EffectSoundManager.instance.SelectButton();
    }
}
