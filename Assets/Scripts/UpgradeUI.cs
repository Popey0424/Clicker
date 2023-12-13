using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    public Image Image;
    public TextMeshProUGUI Text;
    public TextMeshProUGUI TextCost;
    public Upgrade _upgrade;
    



    public void Initialize(Upgrade upgrade)
    {
        _upgrade = upgrade;
        Image.sprite = upgrade.Sprite;
        Text.text = upgrade.Name + System.Environment.NewLine + upgrade.Description;
        TextCost.text = upgrade.Cost + "$";
    }
    public void Onclick()
    {
        MainGame.Instance.AddUpgrade(_upgrade);
    }
}
