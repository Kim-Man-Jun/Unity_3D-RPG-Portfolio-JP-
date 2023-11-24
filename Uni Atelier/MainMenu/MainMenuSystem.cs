using System.Collections.Generic;
using UnityEngine;

public class MainMenuSystem : MonoBehaviour
{
    Canvas mainMenu;

    public List<GameObject> menuButton = new List<GameObject>();

    public List<GameObject> activeButton = new List<GameObject>();

    public ItemDescription itemDescription;
    public InventorySystem inventorySystem;
    public BattleInventorySystem battleInven;
    public StateSystem stateSystem;
    public SaveLoadDescription saveSystem;
    public SaveLoadDescription loadSystem;
    public GameQuit gameQuitSystem;

    private void Awake()
    {
        mainMenu = GameObject.Find("Canvas_Menu").GetComponent<Canvas>();
        mainMenu.enabled = false;
    }

    private void Update()
    {
        if (mainMenu != null && AlchemySystem.alchemySystemOnOff == false
            && AlchemySystem.alchemyCombineOnOff == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
            {
                if (mainMenu.enabled == true)
                {
                    mainMenu.enabled = false;
                    Time.timeScale = 1f;
                    EffectSoundManager.instance.MainmenuOff();
                }

                //메인 메뉴 활성화
                else if (mainMenu.enabled == false)
                {
                    mainMenu.enabled = true;

                    inventorySystem.InventoryHide();
                    battleInven.InventoryHide();
                    stateSystem.InventoryHide();
                    saveSystem.InventoryHide();
                    loadSystem.InventoryHide();
                    gameQuitSystem.InventoryHide();

                    Time.timeScale = 0f;
                    EffectSoundManager.instance.MainmenuOn();
                }
            }
        }
    }

    public void InventoryBtnOn()
    {
        EffectSoundManager.instance.MainmenuButtonOn();
        switchOnOff(0);
    }

    public void ItemBtnOn()
    {
        EffectSoundManager.instance.MainmenuButtonOn();
        switchOnOff(1);
    }

    public void StateBtnOn()
    {
        EffectSoundManager.instance.MainmenuButtonOn();
        switchOnOff(2);
    }

    public void SaveBtnOn()
    {
        EffectSoundManager.instance.MainmenuButtonOn();
        switchOnOff(3);
    }

    public void LoadBtnOn()
    {
        EffectSoundManager.instance.MainmenuButtonOn();
        switchOnOff(4);
    }

    public void GameQuitBtn()
    {
        EffectSoundManager.instance.MainmenuButtonOn();
        switchOnOff(5);
    }

    private void switchOnOff(int num)
    {
        if (menuButton != null && activeButton[num] != null)
        {
            foreach (GameObject go in menuButton)
            {
                go.SetActive(false);
            }

            itemDescription.ResetDescription();

            activeButton[num].SetActive(true);
        }
    }
}
