using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampManager : MonoBehaviour
{
    public SoulGetSleep selectedCamp;
    public GameObject LivingCampRemindCanvas;

    public void selectCamp(SoulGetSleep camp)
    {
        selectedCamp = camp;

        if (camp != null)
        {
            camp.selected = true;
            if (selectedCamp.RelaxTime == 0)
            {
                LivingCampRemindCanvas.gameObject.SetActive(true);
            }
            else
            {
                LivingCampRemindCanvas.gameObject.SetActive(false);
            }
            
        }
        else
        {

        }
        GameObject[] camps = GameObject.FindGameObjectsWithTag("Camp");

        for (int i = 0; i < camps.Length; i++)
        {
            SoulGetSleep Camp = camps[i].GetComponent<SoulGetSleep>();
            if (selectedCamp != Camp)
            {
                Camp.selected = false;
            }

        }
    }
}
