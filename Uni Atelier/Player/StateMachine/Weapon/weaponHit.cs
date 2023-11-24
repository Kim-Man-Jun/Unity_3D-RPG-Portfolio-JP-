using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class weaponHit : MonoBehaviour
{
    public GameObject Hit;

    Player player;
    Enemy enemy;

    SaveLoadSystem saveLoadSystem;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        saveLoadSystem = GameObject.Find("SaveLoadSystem").GetComponent<SaveLoadSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.tag == "Enemy_Field")
            {
                StartCoroutine(enemy_FieldHit(other));
            }

            if (other.gameObject.tag == "Enemy_Battle")
            {
                GameObject go = Instantiate(Hit, other.transform);
                Destroy(go, 1f);

                //데미지 처리
                int randomDamage = Random.Range(player.Atk - 3, player.Atk + 3);

                enemy = other.gameObject.GetComponent<Enemy>();
                enemy.Damaged(randomDamage);

                player.playerBusy = true;
            }
        }
    }

    IEnumerator enemy_FieldHit(Collider other)
    {
        saveLoadSystem.enemyCountSave(other);

        GameObject go = Instantiate(Hit, other.transform);
        Destroy(go, 1f);

        yield return new WaitForSeconds(0.2f);

        other.GetComponent<Enemy>().battleStart = true;
        Player.playerBattle = true;

        //전투 들어가기 전에 플레이어 위치 저장
        saveLoadSystem.PlayerBattleInTransformSave();

        SceneSoundManager.instance.musicStop();
        SceneSoundManager.instance.BattleSound();

        SceneManager.LoadScene("Battle");
    }
}
