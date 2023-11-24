using UnityEngine;

public class AlchemyController : MonoBehaviour
{
    public GameObject alchemyStart;
    public GameObject alchemySystemPanel;

    private void Start()
    {
        alchemyStart.SetActive(false);
        alchemySystemPanel.SetActive(false);
    }

    private void Update()
    {
        systemShow();
        systemHide();
    }

    //�÷��̾ ���ݼ� �׾Ƹ� ��ó�� ���� ���
    //�����ϱ� �г�On
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && AlchemySystem.alchemySystemOnOff == false)
        {
            alchemyStart.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                EffectSoundManager.instance.CombineStart();
                AlchemySystem.alchemySystemOnOff = true;
                Time.timeScale = 0;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            alchemyStart.SetActive(false);
            AlchemySystem.alchemySystemOnOff = false;
        }
    }

    private void systemShow()
    {
        if (AlchemySystem.alchemySystemOnOff == true)
        {
            alchemySystemPanel.gameObject.SetActive(true);
            alchemyStart.SetActive(false);
        }
    }

    private void systemHide()
    {
        if (AlchemySystem.alchemySystemOnOff == false)
        {
            alchemySystemPanel.gameObject.SetActive(false);
        }
    }

    public void backwardButton()
    {
        if (AlchemySystem.alchemyCombineOnOff == false)
        {
            AlchemySystem.alchemySystemOnOff = false;
            Time.timeScale = 1;
        }
    }
}

