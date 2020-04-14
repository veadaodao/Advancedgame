using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplaySolitudeSoul : Observer
{
    public override void OnNotify(object o, NotificationType n)
    {
        if (n == NotificationType.SolitudeSoulUpdated)
        {
            GetComponent<TextMeshProUGUI>().text = "SolitudeSoul: " + o;
        }
    }
}
