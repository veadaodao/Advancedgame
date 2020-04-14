using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayAvariceSoul : Observer
{
    public override void OnNotify(object o, NotificationType n)
    {
        if (n == NotificationType.AvariceSoulUpdated)
        {
            GetComponent<TextMeshProUGUI>().text = "AvariceSoul: " + o;

        }
    }
}