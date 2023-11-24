using TMPro;
using UnityEngine;

public class StateSystem : MonoBehaviour
{
    [Header("Player Stat")]
    public TMP_Text playerLvText;
    public TMP_Text playerLvNextText;
    public TMP_Text playerHpText;
    public TMP_Text playerMpText;
    public TMP_Text playerAtkText;
    public TMP_Text playerDefText;
    public TMP_Text playerSpdText;

    [Header("Component Related")]
    public Player player;
    public MainMenuSystem mainSys;

    [Header("TextColor Related")]
    public Color nowHpMpColor = new Color(200, 30, 30);

    void Update()
    {
        playerStatToDisplay();
    }

    public void playerStatToDisplay()
    {
        playerLvText.text = player.Lv.ToString();
        playerLvNextText.text = (player.MaxExp - player.nowExp).ToString();

        string playerHptext = $"{player.MaxHp} / <color=#{ColorUtility.ToHtmlStringRGB(nowHpMpColor)}>{player.NowHp}</color>";
        playerHpText.text = playerHptext;
        string playerMptext = $"{player.MaxMp} / <color=#{ColorUtility.ToHtmlStringRGB(nowHpMpColor)}>{player.NowMp}</color>";
        playerMpText.text = playerMptext;

        playerAtkText.text = player.Atk.ToString();
        playerDefText.text = player.Def.ToString();
        playerSpdText.text = player.Spd.ToString();
    }

    //인벤토리 숨기기 버튼
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
}
