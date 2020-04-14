using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageManager : MonoBehaviour
{
    public CageDeath selectedCage;

    public void selectCage(CageDeath cage)
    {
        selectedCage = cage;

        if (cage != null)
        {
            cage.selected = true;

        }
        else
        {

        }
        GameObject[] Cages = GameObject.FindGameObjectsWithTag("Cage");

        for (int i = 0; i < Cages.Length; i++)
        {
            CageDeath Cage = Cages[i].GetComponent<CageDeath>();
            if (selectedCage != Cage)
            {
                Cage.selected = false;
            }
        }
    }
}