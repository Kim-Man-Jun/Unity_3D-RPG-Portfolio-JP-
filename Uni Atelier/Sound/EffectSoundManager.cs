using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EffectSoundManager : MonoBehaviour
{
    public static EffectSoundManager instance;

    AudioSource Audio;

    [Header("Main Menu Related")]
    public AudioClip selectButton;
    public AudioClip mainmenuOn;
    public AudioClip mainmenuOff;
    public AudioClip mainmenuButtonSelect;
    public AudioClip mainmenuButtonBack;

    [Header("Alchemy Combine Related")]
    public AudioClip combineStart;
    public AudioClip materialIn;
    public AudioClip materialOut;
    public AudioClip nodeComplete;
    public AudioClip combineComplete;

    [Header("Field Sound Related")]
    public AudioClip playerWandSwing;
    public AudioClip itemGet;
    public AudioClip potionHealing;
    public AudioClip itemEquip;
    public AudioClip itemUnequip;
    public AudioClip healingStone;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    public void SelectButton()
    {
        Audio.PlayOneShot(selectButton);
    }

    public void MainmenuOn()
    {
        Audio.PlayOneShot(mainmenuOn);
    }

    public void MainmenuOff()
    {
        Audio.PlayOneShot(mainmenuOff);
    }

    public void MainmenuButtonOn()
    {
        Audio.PlayOneShot(mainmenuButtonSelect);
    }

    public void MainmenuButtonBack()
    {
        Audio.PlayOneShot(mainmenuButtonBack);
    }

    public void CombineStart()
    {
        Audio.PlayOneShot(combineStart);
    }

    public void MaterialIn()
    {
        Audio.PlayOneShot(materialIn);
    }

    public void MaterialOut()
    {
        Audio.PlayOneShot(materialOut);
    }

    public void NodeComplete()
    {
        Audio.PlayOneShot(nodeComplete);
    }

    public void CombineComplete()
    {
        Audio.PlayOneShot(combineComplete);
    }

    public void PlayerWandSwing()
    {
        Audio.PlayOneShot(playerWandSwing);
    }

    public void ItemGet()
    {
        Audio.PlayOneShot(itemGet);
    }

    public void PotionHealing()
    {
        Audio.PlayOneShot(potionHealing);
    }

    public void ItemEquip()
    {
        Audio.PlayOneShot(itemEquip);
    }

    public void ItemUnequip()
    {
        Audio.PlayOneShot(itemUnequip);
    }

    public void HelingStone()
    {
        Audio.PlayOneShot(healingStone);
    }
}
