using UnityEngine;
using static basicMaterialItemSO;

public class hitDrop : MonoBehaviour
{
    //드롭 아이템 남은 정도
    public int remainPoint;

    public float shakeDuration = 0.5f;
    public float shakePower = 0.12f;
    private float shakeTimer = 0;

    private Vector3 oriPos;

    public basicMaterialItemSO item;
    GameObject player;

    bool hitOn = false;

    public GameObject twinkle;

    Light light;

    private int qualityNum;
    private int specificity1Num;
    private int specificity2Num;
    private int specificity3Num;
    private int specificity4Num;

    // Start is called before the first frame update
    void Start()
    {
        if (twinkle != null)
        {
            var sparkStartTime = twinkle.GetComponent<ParticleSystem>();
            var bubbleStartTime = twinkle.transform.GetChild(0).GetComponent<ParticleSystem>();

            if (sparkStartTime != null)
            {
                float randomTime = Random.Range(0f, 1.5f);

                var main = sparkStartTime.main;
                var main2 = bubbleStartTime.main;

                main.startDelay = randomTime;
                main2.startDelay = randomTime;
            }
        }

        remainPoint = Random.Range(1, 4);

        oriPos = transform.position;

        twinkle = Instantiate(twinkle, transform.position, Quaternion.Euler(270, 0, 0));
        twinkle.transform.SetParent(transform);

        player = GameObject.FindGameObjectWithTag("Player");

        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0)
        {
            transform.position = oriPos + Random.insideUnitSphere * shakePower;

            shakeTimer -= Time.deltaTime;
        }
        else
        {
            shakeTimer = 0;
            transform.position = oriPos;
        }

        if (twinkle != null && remainPoint == 0)
        {
            Destroy(twinkle);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("playerWeapon") && remainPoint > 0)
        {
            EffectSoundManager.instance.ItemGet();
            shakeTimer = shakeDuration;
            remainPoint--;

            if (player != null)
            {
                InventorySO inventory = player.GetComponent<Player>().playerinventoty;

                inventory.AddItem(item);

                InitializeRandomValues();

                item.quality = qualityNum;

                item.specificity1 = (Speccificity)specificity1Num;
                item.specificity2 = (Speccificity)specificity2Num;
                item.specificity3 = (Speccificity)specificity3Num;
                item.specificity4 = (Speccificity)specificity4Num;

                hitOn = false;
            }

            if (light != null)
            {
                light.intensity = 30;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("playerWeapon"))
        {
            if (light != null)
            {
                light.intensity = 0;
            }
        }
    }

    public void InitializeRandomValues()
    {
        qualityNum = UnityEngine.Random.Range(0, 31);

        specificity1Num = UnityEngine.Random.Range(0, 10);
        specificity2Num = UnityEngine.Random.Range(0, 10);
        specificity3Num = UnityEngine.Random.Range(0, 10);
        specificity4Num = UnityEngine.Random.Range(0, 10);
    }
}
