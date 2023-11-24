using UnityEngine;

public class skill5_Fire : MonoBehaviour
{
    public GameObject Hit;

    Player player;
    Enemy enemy;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy_Battle")
        {
            GameObject go = Instantiate(Hit, other.transform);

            int randomDamage = Random.Range(player.Atk - 5, player.Atk + 1);

            enemy = other.gameObject.GetComponent<Enemy>();
            enemy.Damaged(randomDamage);

            Destroy(go, 0.3f); 
        }
    }
}
