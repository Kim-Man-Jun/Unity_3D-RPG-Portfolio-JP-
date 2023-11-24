using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BattleSoundManager : MonoBehaviour
{
    public static BattleSoundManager instance;

    AudioSource Audio;

    [Header("Player Battle Related")]
    public AudioClip skill1;
    public AudioClip skill2;
    public AudioClip skill3;
    public AudioClip skill4;
    public AudioClip skill5_Punch;
    public AudioClip skill5_Kick;
    public AudioClip skill5_Finish;

    public AudioClip fram;
    public AudioClip frambe;

    public AudioClip healingPotion;

    public AudioClip levelUp;

    [Header("Slime Battle Related")]
    public AudioClip slimeHit;
    public AudioClip slimeDamaged;
    public AudioClip slimeDie;

    [Header("Cactus Battle Related")]
    public AudioClip cactusHit;
    public AudioClip cactusDamaged;
    public AudioClip cactusDie;

    [Header("Mushroom Battle Related")]
    public AudioClip mushroomHit;
    public AudioClip mushroomDamaged;
    public AudioClip mushroomDie;

    [Header("Dragon Battle Related")]
    public AudioClip dragonFireShoot;
    public AudioClip dragonFireHit;
    public AudioClip dragonDamaged;
    public AudioClip dragonDie;

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

    public void Skill1()
    {
        Audio.PlayOneShot(skill1);
    }

    public void Skill2()
    {
        Audio.PlayOneShot(skill2);
    }

    public void Skill3()
    {
        Audio.PlayOneShot(skill3);
    }

    public void Skill4()
    {
        Audio.PlayOneShot(skill4);
    }

    public void Skill5_Punch()
    {
        Audio.PlayOneShot(skill5_Punch);
    }

    public void Skill5_Kick()
    {
        Audio.PlayOneShot(skill5_Kick);
    }

    public void Skill5_Finish()
    {
        Audio.PlayOneShot(skill5_Finish);
    }

    public void Fram()
    {
        Audio.PlayOneShot(fram);
    }
    public void Frambe()
    {
        Audio.PlayOneShot(frambe);
    }
    public void HealingPotion()
    {
        Audio.PlayOneShot(healingPotion);
    }
    public void LevelUp()
    {
        Audio.PlayOneShot(levelUp);
    }

    #region enemy Sound
    public void SlimeHit()
    {
        Audio.PlayOneShot(slimeHit);
    }

    public void SlimeDamaged()
    {
        Audio.PlayOneShot(slimeDamaged);
    }

    public void SlimeDie()
    {
        Audio.PlayOneShot(slimeDie);
    }

    public void CactusHit()
    {
        Audio.PlayOneShot(cactusHit);
    }

    public void CactusDamaged()
    {
        Audio.PlayOneShot(cactusDamaged);
    }

    public void CactusDie()
    {
        Audio.PlayOneShot(cactusDie);
    }

    public void MushroomHit()
    {
        Audio.PlayOneShot(mushroomHit);
    }

    public void MushroomDamaged()
    {
        Audio.PlayOneShot(mushroomDamaged);
    }

    public void MushroomDie()
    {
        Audio.PlayOneShot(mushroomDie);
    }

    public void DragonFireShoot()
    {
        Audio.PlayOneShot(dragonFireShoot);
    }

    public void DragonFireHit()
    {
        Audio.PlayOneShot(dragonFireHit);
    }

    public void DragonDamaged()
    {
        Audio.PlayOneShot(dragonDamaged);
    }

    public void DragonDie()
    {
        Audio.PlayOneShot(dragonDie);
    }
    #endregion
}
