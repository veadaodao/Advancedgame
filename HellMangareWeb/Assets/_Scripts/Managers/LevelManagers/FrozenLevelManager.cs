using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenLevelManager : MonoBehaviour
{
    
    public Transform LivingLevelManager;
    public PunishManager punishManager;

    public FrozenSoulManager FrozenSoulManager;
    public LivingSoulManager LivingSoulManager;

    public Canvas UnitWholeInfoCanvas;
    public GameObject GotoHellPanel;
   // public GameObject GotoLivingPanel;
    // Start is called before the first frame update
    void Start()
    {

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
                if (Unit.ifOnFrozenLevel == true)
                {
                    FindObjectOfType<AudioManager>().Play("click");
                    Vector3 GotoDestination = new Vector3(transform.position.x, Unit.transform.position.y, transform.position.z);
                    Unit.targetDestination = GotoDestination;
                    Unit.UnitAgent.SetDestination(Unit.destAtOurHeight);
                    Unit.Level = 1;
                    Unit.TransLevel = true;
                    Debug.Log("go to the Frozen cube");
                    Unit.ifOnFrozenLevel = false;
                    //GotoLivingPanel.gameObject.SetActive(false);
                    UnitWholeInfoCanvas.gameObject.SetActive(false);
                    Unit.ChooseLevel = false;
                    //FrozenSoulNum -1
                    FrozenSoulManager.updateFrozenSoulout(1);
                    Debug.Log("checkedoutFrozen");
                }
            }
        }

    }
    public void GotoFrozenLevel()
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
                Unit.Level = 3;
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