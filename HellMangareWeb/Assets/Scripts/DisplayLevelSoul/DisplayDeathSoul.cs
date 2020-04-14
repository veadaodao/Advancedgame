using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayDeathSoul : Observer
{
    public override void OnNotify(object o, NotificationType n)
    {
        if (n == NotificationType.DeathSoulUpdated)
        {
            GetComponent<TextMeshProUGUI>().text = "DeathSoul: " + o;

        }
    }
}
