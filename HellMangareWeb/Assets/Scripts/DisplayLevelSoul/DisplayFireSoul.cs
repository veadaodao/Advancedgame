using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayFireSoul : Observer
{
    public override void OnNotify(object o, NotificationType n)
    {
        if (n == NotificationType.FireSoulUpdated)
        {
            GetComponent<TextMeshProUGUI>().text = "FireSoul: " + o;

        }
    }
}
