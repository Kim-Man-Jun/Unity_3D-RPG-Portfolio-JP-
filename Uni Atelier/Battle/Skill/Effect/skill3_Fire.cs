using System;
using UnityEngine;
using static actionSystem;

public class skill3_Fire : MonoBehaviour
{
    public GameObject Hit;

    private actionSystem actionSystem;

    private Player player;
    private Enemy enemy;

    private void Awake()
    {
        actionSystem = GameObject.FindGameObjectWithTag("ActionSystem").GetComponent<actionSystem>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    private void FixedUpdate()
    {
        gameObject.transform.position = player.skillFirePos.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy_Battle")
        {
            BattleSoundManager.instance.Skill3();

            GameObject go = Instantiate(Hit, other.transform);

            int randomDamage = UnityEngine.Random.Range(player.Atk - 10, player.Atk + 4);

            enemy = other.gameObject.GetComponent<Enemy>();
            enemy.Damaged(randomDamage);

            var hitinstance = Instantiate(Hit,
                other.transform.position, other.transform.rotation);

            var hitPs = hitinstance.GetComponent<ParticleSystem>();

            if (hitPs != null)
            {
                Destroy(hitinstance, hitPs.main.duration);
            }
            else
            {
                var hitPsParts = hitinstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitinstance, hitPsParts.main.duration);
            }

            actionSystem.skillNum3 = false;
            actionSystem.action_skill3Start = false;
            actionSystem.playerCurrent = Enum.GetName(typeof(playerStateList), 0);

            actionSystem.enemySelect = 0;
        }
    }
}
