using System;
using System.Collections;
using UnityEngine;
using static actionSystem;

//�÷��̾� �Ӹ� ���� �����
public class RestoreItemManager : MonoBehaviour
{
    public GameObject itemHit;
    public GameObject itemFlash;

    [Header("���� � ����")]
    private Rigidbody rb;
    public float speed;
    public float gravity;
    private bool downMoving;

    private actionSystem actionSystem;
    private Player player;

    private void Awake()
    {
        actionSystem = GameObject.FindGameObjectWithTag("ActionSystem").GetComponent<actionSystem>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
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

        yield return new WaitForSeconds(0.3f);

        rb.AddForce(Vector3.up * speed, ForceMode.Impulse);

        rb.useGravity = true;

        downMoving = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (actionSystem.restoreItem == true)
        {
            if (collision.gameObject.CompareTag("Player") && downMoving == true)
            {
                BattleSoundManager.instance.HealingPotion();

                int randomRestore = UnityEngine.Random.Range(actionSystem.itemValue - 10,
                    actionSystem.itemValue + 10);

                player.Restore(randomRestore);

                Destroy(gameObject);

                //HIT ����Ʈ ����
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

                downMoving = false;

                actionSystem.restoreItem = false;
                actionSystem.action_ItemStart = false;
                actionSystem.playerCurrent = Enum.GetName(typeof(playerStateList), 0);

                actionSystem.enemySelect = 0;
            }
        }
    }
}
