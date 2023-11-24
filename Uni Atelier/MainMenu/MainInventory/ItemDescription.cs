using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescription : MonoBehaviour
{
    public TMP_Text itemTitle;
    public Image itemImage;
    public TMP_Text itemQuality;

    public TMP_Text itemEffect1;
    public TMP_Text itemEffect2;
    public TMP_Text itemEffect3;
    public TMP_Text itemEffect4;

    public TMP_Text itemSpecificity1;
    public TMP_Text itemSpecificity2;
    public TMP_Text itemSpecificity3;
    public TMP_Text itemSpecificity4;

    private void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        this.itemImage.gameObject.SetActive(false);
        this.itemTitle.text = "";
        this.itemQuality.text = "";
        this.itemEffect1.text = "";
        this.itemEffect2.text = "";
        this.itemEffect3.text = "";
        this.itemEffect4.text = "";

        this.itemSpecificity1.text = "";
        this.itemSpecificity2.text = "";
        this.itemSpecificity3.text = "";
        this.itemSpecificity4.text = "";
    }

    public void SetDescription(string title, Sprite sprite, float quality,
        string effect1, string effect2, string effect3, string effect4,
        string specificity1, string specificity2, string specificity3, string specificity4)
    {
        this.itemTitle.text = title;
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.itemQuality.text = quality + "";
        this.itemEffect1.text = effect1;
        this.itemEffect2.text = effect2;
        this.itemEffect3.text = effect3;
        this.itemEffect4.text = effect4;
        this.itemSpecificity1.text = specificity1;
        this.itemSpecificity2.text = specificity2;
        this.itemSpecificity3.text = specificity3;
        this.itemSpecificity4.text = specificity4;
    }
}
