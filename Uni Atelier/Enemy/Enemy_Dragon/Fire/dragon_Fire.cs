using UnityEngine;

public class dragon_Fire : MonoBehaviour
{
    public float speed = 15f;
    public GameObject hit;
    public GameObject flash;
    private Rigidbody rb;

    private Player player;
    private Enemy_Dragon enemy;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemy = GameObject.FindGameObjectWithTag("Enemy_Battle").GetComponent<Enemy_Dragon>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (flash != null)
        {
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
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
        Destroy(gameObject, 5);
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;

            rb.velocity = direction * speed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (hit != null)
            {
                BattleSoundManager.instance.DragonFireHit();

                var hitinstance = Instantiate(hit, collision.transform.position, collision.transform.rotation);

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
            }

            int randomDamage = UnityEngine.Random.Range((int)enemy.Atk - 10,
                (int)enemy.Atk + 30);

            collision.gameObject.GetComponent<Player>().Damaged(randomDamage);

            Destroy(gameObject);
        }
    }
}
