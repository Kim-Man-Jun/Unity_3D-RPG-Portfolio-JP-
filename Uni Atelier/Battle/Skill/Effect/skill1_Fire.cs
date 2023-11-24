using System;
using UnityEngine;
using static actionSystem;

public class skill1_Fire : MonoBehaviour
{
    public float speed = 30f;
    public GameObject hit;
    public GameObject flash;

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
        if (flash != null)
        {
            var flashInstance = Instantiate(flash, player.skillFirePos.transform.position,
                Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;

            var flashPs = flashInstance.GetComponent<ParticleSystem>();

            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }
    }

    void FixedUpdate()
    {
        gameObject.transform.position = Vector3.MoveTowards(transform.position,
            actionSystem.temporaryGO.transform.position, speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy_Battle")
        {
            BattleSoundManager.instance.Skill1();

            int randomDamage = UnityEngine.Random.Range(player.Atk - 5, player.Atk + 5);

            enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.Damaged(randomDamage);

            Destroy(gameObject);

            var hitinstance = Instantiate(hit,
                collision.transform.position, collision.transform.rotation);

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

            actionSystem.skillNum1 = false;
            actionSystem.action_skill1Start = false;
            actionSystem.playerCurrent = Enum.GetName(typeof(playerStateList), 0);

            actionSystem.enemySelect = 0;
        }
    }
}
