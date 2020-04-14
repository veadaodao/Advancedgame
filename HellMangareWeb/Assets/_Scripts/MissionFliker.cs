using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionFliker : MonoBehaviour
{
    public Animator missionAnim;
    public RealTime realTime;
    public int index=0;
    void Start()
    {
        missionAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        for (int i = 0; i < units.Length; i++)
        {
            UnitInfo unit = units[i].GetComponent<UnitInfo>();
            if (realTime.currentDay < 1)
            {
                index = 0;
            }
            else
            {
                index = 1;
            }
            switch (index)
            {
                case 0:
                    missionAnim.SetBool("Flicker", true);
                    if (!unit.Convicted)
                    {
                        index = 1;
                    }
                    else
                    {
                        index = 2;
                    }
                    break;
                case 1:
                    missionAnim.SetBool("Flicker", true);
                    if (unit.SpiritNumber <= 1)
                    {
                        index = 2;
                    }
                    else
                    {
                        index = 3;
                    }
                    break;
                case 2:
                    missionAnim.SetBool("Flicker", true);
                    break;
                case 3:
                    missionAnim.SetBool("Flicker", false);
                    break;
            }
        }
    }


}

