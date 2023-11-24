using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject Hit;

    Player player;
    float enemyAtk;

    actionSystem actionSystem;

    SaveLoadSystem saveLoadSystem;

    public bool hitOnce = false;

    [Header("Dragon Attack Related")]
    public Transform dragonAttackPos;
    public GameObject dragonFire;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        actionSystem = GameObject.Find("ActionSystem").GetComponent<actionSystem>();
        saveLoadSystem = GameObject.Find("SaveLoadSystem").GetComponent<SaveLoadSystem>();

        switch (transform.parent.name)
        {
            case "Enemy_Slime(Clone)":
                enemyAtk = transform.parent.GetComponent<Enemy_Slime>().Atk;
                break;
            case "Enemy_Cactus(Clone)":
                enemyAtk = transform.parent.GetComponent<Enemy_Cactus>().Atk;
                break;
            case "Enemy_Mushroom(Clone)":
                enemyAtk = transform.parent.GetComponent<Enemy_Mushroom>().Atk;
                break;
            case "Enemy_Dragon(Clone)":
                enemyAtk = transform.parent.GetComponent<Enemy_Dragon>().Atk;
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && hitOnce == false)
        {
            GameObject go = Instantiate(Hit, other.transform);
            Destroy(go, 1f);

            switch (transform.parent.name)
            {
                case "Enemy_Slime(Clone)":
                    BattleSoundManager.instance.SlimeHit();
                    break;
                case "Enemy_Cactus(Clone)":
                    BattleSoundManager.instance.CactusHit();
                    break;
                case "Enemy_Mushroom(Clone)":
                    BattleSoundManager.instance.MushroomHit();
                    break;
            }

            int randomDamage = Random.Range((int)enemyAtk - 5, (int)enemyAtk + 5);

            player.Damaged(randomDamage);

            hitOnce = true;
        }
    }

    public void dragonAttack()
    {
        BattleSoundManager.instance.DragonFireShoot();
        GameObject go = Instantiate(dragonFire, dragonAttackPos);
    }
}
