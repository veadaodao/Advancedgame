using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TipButton : MonoBehaviour
{
    public TextMeshProUGUI TipNameText;
    public TextMeshProUGUI TipDesText;


    public int buttonId;


    // Clike button to display each level Info
    public void PressTipButton()
    {
        FindObjectOfType<AudioManager>().Play("click");
        TipManager.instance.activeTip = transform.GetComponent<Tip>();
        TipNameText.text = TipManager.instance.Tips[buttonId].TipName;
        TipDesText.text = TipManager.instance.Tips[buttonId].TipDes;

    }
}
