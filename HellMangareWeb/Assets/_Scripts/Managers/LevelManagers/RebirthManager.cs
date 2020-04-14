using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebirthManager : MonoBehaviour
{

    
    public Transform LivingLevelManager;
    
    //public RebirthSoulManager RebirthSoulManager;
    public LivingSoulManager LivingSoulManager;

    public Canvas UnitWholeInfoCanvas;
    public GameObject GotoHellPanel;
    //public GameObject GotoLivingPanel;
    // Start is called before the first frame update
    void Start()
    {
        LivingLevelManager = GameObject.Find("FirstLevelManager").transform;


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GotoRebirthLevel()
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
                Unit.Level = 8;
                Unit.TransLevel = true;
                Debug.Log("go to the living cube");
                Debug.Log(GotoDestination);
               // GotoLivingPanel.gameObject.SetActive(false);
                UnitWholeInfoCanvas.gameObject.SetActive(false);
                Unit.ChooseLevel = false;
                //LivingSoulNum -1
                LivingSoulManager.updateLivingSoulout(1);
                Debug.Log("checkedoutLiving");
                Unit.ifOnLivingLevel = false;
            }
        }




    }

}
