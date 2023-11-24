using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static actionSystem;

public class FramItemManager : MonoBehaviour
{
    public GameObject itemHit;
    public GameObject itemFlash;

    [Header("Parabola Action Related")]
    private Rigidbody rb;
    private Transform myTransform;
    public Transform Target;
    public GameObject ItemThrowingPosition;

    public float speed;
    public float gravity;

    private float durationTime;

    private actionSystem actionSystem;
    private Enemy enemy;

    private void Awake()
    {
        actionSystem = GameObject.FindGameObjectWithTag("ActionSystem").GetComponent<actionSystem>();

        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        ItemThrowingPosition = GameObject.Find("ItemThrowingPostion");

        durationTime = 0f;

        myTransform = transform;

        if (actionSystem.attackSingleItem == true)
        {
            Target = actionSystem.temporaryGO.transform;
        }
        else if (actionSystem.attackGroupItem == true)
        {
            Target = actionSystem.enemyPos2.transform;
        }

        StartCoroutine(Launch());

        if (itemFlash != null)
        {
            var flashInstance = Instantiate(itemFlash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;

            var flashPs = flashInstance.GetComponent<ParticleSystem>();

            if (flashPs != null)
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }
    }

    IEnumerator Launch()
    {
        rb.useGravity = false;

        yield return new WaitForSeconds(0.5f);

        rb.useGravity = true;

        Vector3 toTarget = Target.position - myTransform.position;

        float distanceToTarget = toTarget.magnitude;
        float timeToreachTarget = distanceToTarget / speed;

        Vector3 horizontalVelocity = toTarget / timeToreachTarget;
        horizontalVelocity.y = 0;

        //초기 속도
        rb.velocity = horizontalVelocity;

        //중력을 고려한 수직 방향의 속도
        float verticalVelocity = (distanceToTarget / 2) / (timeToreachTarget / 2) * gravity;

        //수직 속도 추가
        rb.velocity += Vector3.up * verticalVelocity;
    }

    //단일 공격 아이템
    private void OnCollisionEnter(Collision collision)
    {
        if (actionSystem.attackSingleItem == true)
        {
            if (collision.gameObject.CompareTag("Enemy_Battle"))
            {
                BattleSoundManager.instance.Fram();

                int randomDamage = UnityEngine.Random.Range(actionSystem.itemValue - 10,
                    actionSystem.itemValue + 10);

                enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.Damaged(randomDamage);

                Destroy(gameObject);

                var hitinstance = Instantiate(itemHit,
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

                durationTime = 0;

                actionSystem.attackSingleItem = false;
                actionSystem.action_ItemStart = false;
                actionSystem.playerCurrent = Enum.GetName(typeof(playerStateList), 0);

                actionSystem.enemySelect = 0;
            }
        }
    }

    //전체 공격 아이템
    private void OnTriggerEnter(Collider collision)
    {
        if (actionSystem.attackGroupItem == true)
        {
            if (collision.gameObject.name == "EnemySpawn2")
            {
                BattleSoundManager.instance.Frambe();

                int randomDamage = UnityEngine.Random.Range(actionSystem.itemValue - 30,
                    actionSystem.itemValue + 30);

                List<GameObject> allEnemy = new List<GameObject>
                {
                    actionSystem.enemy1,
                    actionSystem.enemy2,
                    actionSystem.enemy3
                };

                for (int i = 0; i < allEnemy.Count; i++)
                {
                    if (allEnemy[i] != null)
                    {
                        allEnemy[i].GetComponent<Enemy>().Damaged(randomDamage);
                    }
                }

                Destroy(gameObject);

                var hitinstance = Instantiate(itemHit,
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

                durationTime = 0;

                actionSystem.attackGroupItem = false;
                actionSystem.action_ItemStart = false;
                actionSystem.playerCurrent = Enum.GetName(typeof(playerStateList), 0);
                    
                actionSystem.enemySelect = 0;
            }
        }
    }
}
