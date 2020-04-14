using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayRebirthSoul : Observer
{
    public override void OnNotify(object o, NotificationType n)
    {
        if (n == NotificationType.RebirthSoulUpdated)
        {
            GetComponent<TextMeshProUGUI>().text = "RebirthSoul: " + o;

        }
    }
}
