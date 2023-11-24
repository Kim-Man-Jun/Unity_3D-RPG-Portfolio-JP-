using TMPro;
using UnityEngine;

public class DamagedPopup : MonoBehaviour
{
    private TextMeshPro damageText;
    private Color textColor;
    private float disappearTimer;

    private void Awake()
    {
        damageText = transform.GetComponent<TextMeshPro>();
    }

    public void DamageSetup(int damage)
    {
        damageText.SetText(damage.ToString());
        textColor = Color.red;
        disappearTimer = 0.4f;
    }

    public void RestoreSetup(int restore)
    {
        damageText.SetText(restore.ToString());
        textColor = Color.green;
        disappearTimer = 0.4f;
    }

    private void Update()
    {
        damageText.color = textColor;

        float moveYSpeed = 1.5f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;

        if (disappearTimer < 0)
        {
            float disappearSpeed = 5f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            damageText.color = textColor;

            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
