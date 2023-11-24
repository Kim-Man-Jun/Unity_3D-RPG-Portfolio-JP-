using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static basicMaterialItemSO;

public class battleVictory : MonoBehaviour
{
    [Header("Action System Related")]
    public actionSystem ActionSystem;

    [Header("Battle Victory Appear Panel Related")]
    public GameObject victoryCanvas;
    public GameObject battleResultPanel;
    public GameObject playerLvUpPanel;
    public TMP_Text playerGainExp;

    [Header("Battle Result Panel Related")]
    public statSO_player playerStat;
    public TMP_Text playerLv;
    public TMP_Text playerNextValue;
    public Slider playerNextSlider;

    //이번 전투에서 얻은 경험치
    private int targetExp;
    private float nowTime;
    private float durationTime = 2f;

    public bool playerLvUp = false;

    [Header("Victory Get Item Panel Related")]
    public GameObject getItemBase;
    public RectTransform content;

    [Header("Player Level Up Panel Related")]
    //TMP_text 컴포넌트
    public TMP_Text playerPreLvText;
    public TMP_Text playerNextLvText;
    public TMP_Text playerNextExpText;
    public TMP_Text playerPreHpText;
    public TMP_Text playerNextHpText;
    public TMP_Text playerPreMpText;
    public TMP_Text playerNextMpText;
    public TMP_Text playerPreAtkText;
    public TMP_Text playerNextAtkText;
    public TMP_Text playerPreDefText;
    public TMP_Text playerNextDefText;
    public TMP_Text playerPreSpdText;
    public TMP_Text playerNextSpdText;

    private int prePlayerLv;
    private int prePlayerNextExp;
    private int prePlayerMaxHp;
    private int prePlayerMaxMp;
    private int prePlayerAtk;
    private int prePlayerDef;
    private int prePlayerSpd;

    [Header("Enemy State Related")]
    public GameObject enemy1Pos;
    public GameObject enemy2Pos;
    public GameObject enemy3Pos;

    public int enemy1Exp = 0;
    public int enemy2Exp = 0;
    public int enemy3Exp = 0;

    public basicMaterialItemSO enemy1DropItem;
    public basicMaterialItemSO enemy2DropItem;
    public basicMaterialItemSO enemy3DropItem;

    [Header("Save & Load System Related")]
    public SaveLoadSystem saveLoadSystem;
    public Player player;

    private int qualityNum;
    private int specificity1Num;
    private int specificity2Num;
    private int specificity3Num;
    private int specificity4Num;

    void Start()
    {
        enemyNameToGetComponent();

        dropItemAdd();

        playerLv.text = playerStat.Lv.ToString();
        playerNextValue.text = (playerStat.MaxExp - playerStat.nowExp).ToString();

        //플레이어 레벨업 전 초기 정보 저장
        prePlayerLv = playerStat.Lv;
        prePlayerMaxHp = (int)playerStat.MaxHp;
        prePlayerMaxMp = (int)playerStat.MaxMp;
        prePlayerAtk = (int)playerStat.Atk;
        prePlayerDef = (int)playerStat.Def;
        prePlayerSpd = (int)playerStat.Spd;

        //이번 전투로 얻을 경험치
        targetExp = playerStat.nowExp + enemy1Exp + enemy2Exp + enemy3Exp;
        playerGainExp.text = (enemy1Exp + enemy2Exp + enemy3Exp).ToString();

        playerNextSlider.maxValue = playerStat.MaxExp;
        playerNextSlider.value = playerStat.nowExp;

        victoryCanvas.SetActive(false);
    }

    void Update()
    {
        if (ActionSystem.battleVictory == true)
        {
            if (playerLvUp == false)
            {
                nowTime += Time.deltaTime;
                float t = Mathf.Clamp01(nowTime / durationTime);
                //슬라이더 바 선형보간을 이용한 애니메이션 효과
                playerNextSlider.value = Mathf.Lerp(playerStat.nowExp, targetExp, t);

                if (t >= 1)
                {
                    playerStat.nowExp = targetExp;
                }
            }

            //플레이어가 레벨업 했을 경우
            if (playerStat.nowExp >= playerStat.MaxExp)
            {
                BattleSoundManager.instance.LevelUp();

                playerLvUp = true;

                playerStat.Lv += 1;
                playerLv.text = playerStat.Lv.ToString();

                playerStat.MaxExp += 100;

                playerStat.nowExp = playerStat.nowExp - playerStat.MaxExp;

                playerNextSlider.maxValue = playerStat.MaxExp;
                playerNextSlider.value = playerStat.nowExp;

                playerPreLvText.text = prePlayerLv.ToString();
                playerNextLvText.text = playerStat.Lv.ToString();

                playerNextExpText.text = (playerStat.MaxExp - playerStat.nowExp).ToString();

                playerPreHpText.text = prePlayerMaxHp.ToString();
                playerNextHpText.text = (playerStat.MaxHp + 4).ToString();

                playerPreMpText.text = prePlayerMaxMp.ToString();
                playerNextMpText.text = (playerStat.MaxMp + 3).ToString();

                playerPreAtkText.text = prePlayerAtk.ToString();
                playerNextAtkText.text = (playerStat.Atk + 3).ToString();

                playerPreDefText.text = prePlayerDef.ToString();
                playerNextDefText.text = (playerStat.Def + 2).ToString();

                playerPreSpdText.text = prePlayerSpd.ToString();
                playerNextSpdText.text = (playerStat.Spd + 1).ToString();

                playerStat.NowHp += 4;
                playerStat.MaxHp += 4;

                playerStat.NowMp += 3;
                playerStat.MaxMp += 3;

                playerStat.Atk += 3;
                playerStat.Def += 2;
                playerStat.Spd += 1;

                playerPreLvText.text = prePlayerLv.ToString();
            }

            playerNextValue.text = (playerStat.MaxExp - playerStat.nowExp).ToString();
        }
    }

    public void BattleResultNextButton()
    {
        //드롭 아이템 랜덤 수치 조정
        if (enemy1DropItem != null)
        {
            InventorySO inventory = player.GetComponent<Player>().playerinventoty;
            inventory.AddItem(enemy1DropItem);

            InitializeRandomValues();

            enemy1DropItem.quality = qualityNum;

            enemy1DropItem.specificity1 = (Speccificity)specificity1Num;
            enemy1DropItem.specificity2 = (Speccificity)specificity2Num;
            enemy1DropItem.specificity3 = (Speccificity)specificity3Num;
            enemy1DropItem.specificity4 = (Speccificity)specificity4Num;
        }

        if (enemy2DropItem != null)
        {
            InventorySO inventory = player.GetComponent<Player>().playerinventoty;
            inventory.AddItem(enemy2DropItem);

            InitializeRandomValues();

            enemy2DropItem.quality = qualityNum;

            enemy2DropItem.specificity1 = (Speccificity)specificity1Num;
            enemy2DropItem.specificity2 = (Speccificity)specificity2Num;
            enemy2DropItem.specificity3 = (Speccificity)specificity3Num;
            enemy2DropItem.specificity4 = (Speccificity)specificity4Num;
        }

        if (enemy3DropItem != null)
        {
            InventorySO inventory = player.GetComponent<Player>().playerinventoty;
            inventory.AddItem(enemy3DropItem);

            InitializeRandomValues();

            enemy3DropItem.quality = qualityNum;

            enemy3DropItem.specificity1 = (Speccificity)specificity1Num;
            enemy3DropItem.specificity2 = (Speccificity)specificity2Num;
            enemy3DropItem.specificity3 = (Speccificity)specificity3Num;
            enemy3DropItem.specificity4 = (Speccificity)specificity4Num;
        }

        //전투 종료 시 사용한 공격, 회복 아이템 저장
        saveLoadSystem.attackItemInventorySave();
        saveLoadSystem.restoreItemInventorySave();

        //레벨업 안 했을 때
        if (playerLvUp == false)
        {
            EffectSoundManager.instance.SelectButton();

            SceneSoundManager.instance.musicStop();
            SceneSoundManager.instance.MainFieldSound();

            //체력과 마나 저장
            saveLoadSystem.PlayerStatSave();

            battleResultPanel.SetActive(false);
            Player.playerBattle = false;
            SaveLoadSystem.enemyCount = 0;
            SaveLoadSystem.enemyDiecount = 0;
            SceneManager.LoadScene(playerStat.sceneName);
        }

        //레벨업 했을 때
        if (playerLvUp == true)
        {
            playerLvUpPanel.SetActive(true);
            battleResultPanel.SetActive(false);
        }
    }

    public void LvUpNextButton()
    {
        EffectSoundManager.instance.SelectButton();

        SceneSoundManager.instance.musicStop();
        SceneSoundManager.instance.MainFieldSound();

        //플레이어의 현재 상태 수치 저장
        saveLoadSystem.PlayerStatSave();

        playerLvUp = false;
        Player.playerBattle = false;
        SaveLoadSystem.enemyCount = 0;
        SaveLoadSystem.enemyDiecount = 0;
        SceneManager.LoadScene(playerStat.sceneName);
    }

    //적들 경험치와 아이템 설정
    public void enemyNameToGetComponent()
    {
        if (ActionSystem.enemy1 != null)
        {
            if (enemy1Pos.transform.GetChild(1).gameObject != null)
            {
                if (enemy1Pos.transform.GetChild(1).gameObject.name == $"{SaveLoadSystem.enemyName}(Clone)")
                {
                    if ($"{SaveLoadSystem.enemyName}(Clone)" == "Enemy_Slime(Clone)")
                    {
                        enemy1Exp = enemy1Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Slime>().Exp;
                        enemy1DropItem = enemy1Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Slime>().dropItme;
                    }
                    else if ($"{SaveLoadSystem.enemyName}(Clone)" == "Enemy_Cactus(Clone)")
                    {
                        enemy1Exp = enemy1Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Cactus>().Exp;
                        enemy1DropItem = enemy1Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Cactus>().dropItme;
                    }
                    else if ($"{SaveLoadSystem.enemyName}(Clone)" == "Enemy_Mushroom(Clone)")
                    {
                        enemy1Exp = enemy1Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Mushroom>().Exp;
                        enemy1DropItem = enemy1Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Mushroom>().dropItme;
                    }
                    else if ($"{SaveLoadSystem.enemyName}(Clone)" == "Enemy_Dragon(Clone)")
                    {
                        enemy1Exp = enemy1Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Dragon>().Exp;
                        enemy1DropItem = enemy1Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Dragon>().dropItme;
                    }
                }
            }
        }

        if (ActionSystem.enemy2 != null)
        {
            if (enemy2Pos.transform.GetChild(1).gameObject != null)
            {
                if (enemy2Pos.transform.GetChild(1).gameObject.name == $"{SaveLoadSystem.enemyName}(Clone)")
                {
                    if ($"{SaveLoadSystem.enemyName}(Clone)" == "Enemy_Slime(Clone)")
                    {
                        enemy2Exp = enemy2Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Slime>().Exp;
                        enemy2DropItem = enemy2Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Slime>().dropItme;
                    }
                    else if ($"{SaveLoadSystem.enemyName}(Clone)" == "Enemy_Cactus(Clone)")
                    {
                        enemy2Exp = enemy2Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Cactus>().Exp;
                        enemy2DropItem = enemy2Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Cactus>().dropItme;
                    }
                    else if ($"{SaveLoadSystem.enemyName}(Clone)" == "Enemy_Mushroom(Clone)")
                    {
                        enemy2Exp = enemy2Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Mushroom>().Exp;
                        enemy2DropItem = enemy2Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Mushroom>().dropItme;
                    }
                    else if ($"{SaveLoadSystem.enemyName}(Clone)" == "Enemy_Dragon(Clone)")
                    {
                        enemy2Exp = enemy2Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Dragon>().Exp;
                        enemy2DropItem = enemy2Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Dragon>().dropItme;
                    }
                }
            }
        }

        if (ActionSystem.enemy3 != null)
        {
            if (enemy3Pos.transform.GetChild(1).gameObject != null)
            {
                if (enemy3Pos.transform.GetChild(1).gameObject.name == $"{SaveLoadSystem.enemyName}(Clone)")
                {
                    if ($"{SaveLoadSystem.enemyName}(Clone)" == "Enemy_Slime(Clone)")
                    {
                        enemy3Exp = enemy3Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Slime>().Exp;
                        enemy3DropItem = enemy3Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Slime>().dropItme;
                    }
                    else if ($"{SaveLoadSystem.enemyName}(Clone)" == "Enemy_Cactus(Clone)")
                    {
                        enemy3Exp = enemy3Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Cactus>().Exp;
                        enemy3DropItem = enemy3Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Cactus>().dropItme;
                    }
                    else if ($"{SaveLoadSystem.enemyName}(Clone)" == "Enemy_Mushroom(Clone)")
                    {
                        enemy3Exp = enemy3Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Mushroom>().Exp;
                        enemy3DropItem = enemy3Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Mushroom>().dropItme;
                    }
                    else if ($"{SaveLoadSystem.enemyName}(Clone)" == "Enemy_Dragon(Clone)")
                    {
                        enemy3Exp = enemy3Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Dragon>().Exp;
                        enemy3DropItem = enemy3Pos.transform.GetChild(1).gameObject.GetComponent<Enemy_Dragon>().dropItme;
                    }
                }
            }
        }
    }

    //드롭 아이템 이미지만 따로 설정
    public void dropItemAdd()
    {
        if (enemy1DropItem != null)
        {
            GameObject go = Instantiate(getItemBase, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(content);

            go.transform.GetChild(0).GetComponent<Image>().sprite = enemy1DropItem.itemImage;
        }

        if (enemy2DropItem != null)
        {
            GameObject go = Instantiate(getItemBase, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(content);

            go.transform.GetChild(0).GetComponent<Image>().sprite = enemy2DropItem.itemImage;
        }

        if (enemy3DropItem != null)
        {
            GameObject go = Instantiate(getItemBase, Vector3.zero, Quaternion.identity);
            go.transform.SetParent(content);

            go.transform.GetChild(0).GetComponent<Image>().sprite = enemy3DropItem.itemImage;
        }
    }

    public void InitializeRandomValues()
    {
        qualityNum = UnityEngine.Random.Range(0, 31);

        specificity1Num = UnityEngine.Random.Range(0, 10);
        specificity2Num = UnityEngine.Random.Range(0, 10);
        specificity3Num = UnityEngine.Random.Range(0, 10);
        specificity4Num = UnityEngine.Random.Range(0, 10);
    }
}
