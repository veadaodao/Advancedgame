using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayVisitingSoul : Observer
{
    public override void OnNotify(object o, NotificationType n)
    {
        if (n == NotificationType.VisitingSoulUpdated)
        {
            GetComponent<TextMeshProUGUI>().text = "VisitingSoul: " + o;
        }
    }
}
