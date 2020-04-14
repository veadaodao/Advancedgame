using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayFrozenSoul : Observer
{
    public override void OnNotify(object o, NotificationType n)
    {
        if (n == NotificationType.FrozenSoulUpdated)
        {
            GetComponent<TextMeshProUGUI>().text = "FrozenSoul: " + o;
        }
    }
}
