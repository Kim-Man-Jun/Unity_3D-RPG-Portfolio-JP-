using UnityEngine;

public class HealingStone : MonoBehaviour
{
    public GameObject HealingPanel;
    public Player player;

    private void Start()
    {
        HealingPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HealingPanel.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EffectSoundManager.instance.HelingStone();

            player.NowHp += 10000;
            player.NowMp += 10000;

            if (player.NowHp > player.MaxHp)
            {
                player.NowHp = player.MaxHp;
            }

            if (player.NowMp > player.MaxMp)
            {
                player.NowMp = player.MaxMp;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HealingPanel.SetActive(false);
    }
}
