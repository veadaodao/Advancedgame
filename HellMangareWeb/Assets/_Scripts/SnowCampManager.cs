using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowCampManager : MonoBehaviour
{
    public SnowCoolProduce selectedSnowCamp;

    public void selectSnowCamp(SnowCoolProduce snowCamp)
    {
        selectedSnowCamp = snowCamp;

        if (snowCamp != null)
        {
            snowCamp.selected = true;

        }
        else
        {

        }
        GameObject[] snowCamps = GameObject.FindGameObjectsWithTag("SnowCamp");

        for (int i = 0; i < snowCamps.Length; i++)
        {
            SnowCoolProduce SnowCamp = snowCamps[i].GetComponent<SnowCoolProduce>();
            if (selectedSnowCamp != SnowCamp)
            {
                SnowCamp.selected = false;
            }

        }
    }
}
