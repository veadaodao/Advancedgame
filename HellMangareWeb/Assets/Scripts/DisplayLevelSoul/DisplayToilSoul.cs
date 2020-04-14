using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayToilSoul : Observer
{
    public override void OnNotify(object o, NotificationType n)
    {
        if (n == NotificationType.ToilSoulUpdated)
        {
            GetComponent<TextMeshProUGUI>().text = "ToilSoul: " + o;

        }
    }
}
