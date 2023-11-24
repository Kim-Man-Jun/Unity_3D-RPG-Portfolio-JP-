using TMPro;
using UnityEngine;

public class SaveLoadDescription : MonoBehaviour
{
    [Header("TMP_Text Related")]
    public TMP_Text playerLV;
    public TMP_Text playerHP;
    public TMP_Text playerMP;
    public TMP_Text playerLocation = null;

    [Header("Scriptable Object Related")]
    public statSO_player playerSO;

    public MainMenuSystem mainSys;

    void Update()
    {
        playerLV.text = $"Lv : {playerSO.Lv}";
        playerHP.text = $"HP : {playerSO.MaxHp} / {playerSO.NowHp}";
        playerMP.text = $"MP : {playerSO.MaxMp} / {playerSO.NowMp}";
    }

    //�κ��丮 ����� ��ư
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
