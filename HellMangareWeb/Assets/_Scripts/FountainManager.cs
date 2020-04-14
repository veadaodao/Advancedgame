using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainManager : MonoBehaviour
{
    public FireFountainWork selectedFountain;

    public void selectFountain(FireFountainWork fountain)
    {
        selectedFountain = fountain;

        if (fountain != null)
        {
            fountain.selected = true;

        }
        else
        {

        }
        GameObject[] fountains = GameObject.FindGameObjectsWithTag("Fountain");

        for (int i = 0; i < fountains.Length; i++)
        {
            FireFountainWork Fountain = fountains[i].GetComponent<FireFountainWork>();
            if (selectedFountain != Fountain)
            {
                Fountain.selected = false;
            }

        }
    }
}
