using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMoveManager : MonoBehaviour
{
    public UnitInfo selectedUnit;

    public void selectUnit(UnitInfo unit)
    {
        selectedUnit = unit;

        if (unit != null)
        {

            unit.selected = true;

        }
        else
        {

        }


        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        for (int i = 0; i < units.Length; i++)
        {
            UnitInfo us = units[i].GetComponent<UnitInfo>();
            if (selectedUnit != us)
            {
                us.selected = false;
            }
            us.UnitInfoCanvasFade();
        }
    }
}