using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayLivingSoul : Observer
{
    public override void OnNotify(object o, NotificationType n)
    {
        if (n == NotificationType.LivingSoulUpdated)
        {
            GetComponent<TextMeshProUGUI>().text = "LivingSoul: " + o;
            
        }
    }
}
