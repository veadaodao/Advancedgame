using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolitudeLevelManager : MonoBehaviour
{
    
    public Transform LivingLevelManager;
    public PunishManager punishManager;

    public SolitudeSoulManager SolitudeSoulManager;
    public LivingSoulManager LivingSoulManager;
    // Start is called before the first frame update
    void Start()
    {
        LivingLevelManager = GameObject.Find("FirstLevelManager").transform;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GotoLivingLevel()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        for (int i = 0; i < units.Length; i++)
        {
            UnitInfo Unit = units[i].GetComponent<UnitInfo>();
            if (Unit.ChooseLevel == true)
            {
                if (Unit.ifOnSolitudeLevel == true)
                {
                    Vector3 GotoDestination = new Vector3(transform.position.x, Unit.transform.position.y, transform.position.z);
                    Unit.targetDestination = GotoDestination;
                    Unit.UnitAgent.SetDestination(Unit.destAtOurHeight);
                    Unit.Level = 1;
                    Unit.TransLevel = true;
                    Debug.Log("go to the Solitude cube");
                    //SolitudeSoulNum -1
                    SolitudeSoulManager.updateSolitudeSoulout(1);
                    Debug.Log("checkedoutSolitude");
                    Unit.ifOnSolitudeLevel = false;
                }
            }
        }

    }
    public void GotoSolitudeLevel()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        for (int i = 0; i < units.Length; i++)
        {
            UnitInfo Unit = units[i].GetComponent<UnitInfo>();
            if (Unit.ChooseLevel == true)
            {
                Vector3 GotoDestination = new Vector3(LivingLevelManager.transform.position.x, Unit.transform.position.y, LivingLevelManager.transform.position.z);
                Unit.targetDestination = GotoDestination;
                Unit.UnitAgent.SetDestination(Unit.destAtOurHeight);
                Unit.Level = 6;
                Unit.TransLevel = true;
                Debug.Log("go to the living cube");
                Debug.Log(GotoDestination);
                //LivingSoulNum -1
                LivingSoulManager.updateLivingSoulout(1);
                Debug.Log("checkedoutLiving");
                Unit.ifOnLivingLevel = false;
            }
        }


    }

}

