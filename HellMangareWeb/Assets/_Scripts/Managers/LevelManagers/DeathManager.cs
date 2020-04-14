using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{

    
    public Transform LivingLevelManager;
    //public DeathSoulManager DeathSoulManager;
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

    public void GotoDeathLevel()
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
                Unit.Level = 9;
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
