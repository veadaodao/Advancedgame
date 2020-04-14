using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitingLevelManager : MonoBehaviour
{

    
    public Transform LivingLevelManager;
    public PunishManager punishManager;

    public VisitingSoulManager VisitingSoulManager;
    public LivingSoulManager LivingSoulManager;

    public Canvas UnitWholeInfoCanvas;
    public GameObject GotoHellPanel;

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
                if (Unit.ifOnVisitingLevel == true)
                {
                    FindObjectOfType<AudioManager>().Play("click");
                    Vector3 GotoDestination = new Vector3(transform.position.x, Unit.transform.position.y, transform.position.z);
                    Unit.targetDestination = GotoDestination;
                    Unit.UnitAgent.SetDestination(Unit.destAtOurHeight);
                    Unit.Level = 1;
                    Unit.TransLevel = true;
                    Debug.Log("go to the Visiting cube");
                    Unit.ifOnVisitingLevel = false;
                    //GotoLivingPanel.gameObject.SetActive(false);
                    UnitWholeInfoCanvas.gameObject.SetActive(false);
                    Unit.ChooseLevel = false;
                    //VisitingSoulNum -1
                    VisitingSoulManager.updateVisitingSoulout(1);
                    Debug.Log("checkedoutVisiting");
                }
            }
        }

    }
    public void GotoVisitingLevel()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        for (int i = 0; i < units.Length; i++)
        {
            UnitInfo Unit = units[i].GetComponent<UnitInfo>();
            if (Unit.ChooseLevel && Unit.Convicted)
            {
                FindObjectOfType<AudioManager>().Play("click");
                Vector3 GotoDestination = new Vector3(LivingLevelManager.transform.position.x, Unit.transform.position.y, LivingLevelManager.transform.position.z);
                Unit.targetDestination = GotoDestination;
                Unit.UnitAgent.SetDestination(Unit.destAtOurHeight);
                Unit.Level = 7;
                Unit.TransLevel = true;
                Debug.Log("go to the living cube");
                Debug.Log(GotoDestination);
                Unit.ifOnLivingLevel = false;
                GotoHellPanel.gameObject.SetActive(false);
                UnitWholeInfoCanvas.gameObject.SetActive(false);
                Unit.ChooseLevel = false;
                //LivingSoulNum -1
                LivingSoulManager.updateLivingSoulout(1);
                Debug.Log("checkedoutLiving");
            }

        }




    }

}