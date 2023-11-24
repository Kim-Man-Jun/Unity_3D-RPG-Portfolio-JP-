using UnityEngine;

public class GameQuit : MonoBehaviour
{
    [Header("Component Related")]
    public MainMenuSystem mainSys;
    public statSO_player playerbattleTransform;

    public void GameQuitOn()
    {
        EffectSoundManager.instance.SelectButton();

#if UNITY_EDITOR
        playerbattleTransform.playerBattleInTransform = new Vector3(0, 0, 0);
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        playerbattleTransform.playerBattleInTransform = new Vector3(0, 0, 0);
        Application.Quit();
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
