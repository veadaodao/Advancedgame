using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulTutorialMoveManager : MonoBehaviour
{
    public UnitInfoTutorial selectedUnit;

    public void selectUnit(UnitInfoTutorial unit)
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
            UnitInfoTutorial us = units[i].GetComponent<UnitInfoTutorial>();
            if (selectedUnit != us)
            {
                us.selected = false;
            }
            us.UnitInfoCanvasFade();
        }
    }
}
