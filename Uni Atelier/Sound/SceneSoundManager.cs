using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SceneSoundManager : MonoBehaviour
{
    public static SceneSoundManager instance;

    AudioSource Audio;

    public AudioClip openingSound;
    public AudioClip mainFieldSound;
    public AudioClip battleSound;
    public AudioClip battleVictorySound;
    public AudioClip gameOversound;

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

    public void OpeningSound()
    {
        Audio.clip = openingSound;
        Audio.loop = true;

        Audio.Play();
    }

    public void MainFieldSound()
    {
        Audio.clip = mainFieldSound;
        Audio.loop = true;

        Audio.Play();
    }

    public void BattleSound()
    {
        Audio.clip = battleSound;
        Audio.loop = true;

        Audio.Play();
    }

    public void BattleVicrotyWin()
    {
        Audio.clip = battleVictorySound;
        Audio.loop = true;

        Audio.Play();   
    }

    public void GameOverSound()
    {
        Audio.clip = gameOversound;
        Audio.loop = true;

        Audio.Play();
    }

    public void musicStop()
    {
        Audio.Stop();
    }
}
