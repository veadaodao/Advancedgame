using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SoulGetSleep : MonoBehaviour
{
    CampManager campManager;
    public bool selected = false;
    bool justSelected = false;
    bool hover = false;
    public bool isUsed;
    private bool IsSleep = false;
    public bool IsSetDuration = false;
    public bool CampInfo;
    public bool TGoRelax = false;
    public GameObject SleepEffect;
    public LayerMask layerMask;
    public ChangeCenterAndLeftInfo ChangePanel;
    public int RelaxTime = 0;
    public GameObject HoverEffect;
    public UnitInfoTutorial unitTutorial;
    void Start()
    {
        GameObject campManagerObj = GameObject.Find("CampManager");
        campManager = campManagerObj.GetComponent<CampManager>();


    }

    // Update is called once per frame
    void Update()
    {
        justSelected = false;
        if (Input.GetMouseButtonDown(1))
        {
            CampInfo = false;
            ChangePanel.BuildingLevelInfo = 0;

        }
        switch (RelaxTime)
        {
            case 0:

                IsSetDuration = false;
                break;
            case 5:
                
                if (IsSetDuration)
                {
                    Vector3 EffectPosition = new Vector3(transform.position.x - 1, transform.position.y + 3, transform.position.z - 2);
                    Destroy(Instantiate(SleepEffect, EffectPosition, Quaternion.identity), 25f);
                    IsSetDuration = false;
                }

                break;
            case 8:
                
                if (IsSetDuration)
                {
                    Vector3 EffectPosition = new Vector3(transform.position.x - 1, transform.position.y + 3, transform.position.z - 2);
                    Destroy(Instantiate(SleepEffect, EffectPosition, Quaternion.identity), 40f);
                    IsSetDuration = false;
                }
                break;
        }
        if (selected && !justSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, layerMask))
                    {
                        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Camp"))
                        {
                            if(campManager.selectedCamp = this)
                            {

                            }
                        }
                    }
                    else
                    {
                        campManager.selectCamp(null);
                    }
                }
            }
        }
    }
    public void OnMouseDown()
    {
        if (campManager.selectedCamp = this)
        {
            FindObjectOfType<AudioManager>().Play("click");
            selected = true;
            justSelected = true;
            campManager.selectCamp(this);
            CampInfo = true;
            ChangePanel.BuildingLevelInfo = 2;
        }
     
    }
    void OnMouseOver()
    {
        hover = true;
        HoverEffect.gameObject.SetActive(true);
    }
    void OnMouseExit()
    {
        hover = false;
        HoverEffect.gameObject.SetActive(false);
    }
    public void GetRelaxFive()
    {
        FindObjectOfType<AudioManager>().Play("click");
        RelaxTime = 5;
        IsSetDuration = true;
    }
    public void GetRelaxEight()
    {
        FindObjectOfType<AudioManager>().Play("click");
        RelaxTime = 8;
        IsSetDuration = true;
    }



    //Tutorial 
    public void TutorialRelax()
    {
        if (unitTutorial.TutorialLevel)
        {
            unitTutorial.targetDestination = new Vector3(transform.position.x - 2f, transform.position.y, transform.position.z - 2.5f);
            unitTutorial.UnitAgent.enabled = true;
            Vector3 destAtOurHeight = new Vector3(unitTutorial.targetDestination.x, unitTutorial.targetDestination.y + 1f, unitTutorial.targetDestination.z);
            unitTutorial.UnitAgent.SetDestination(destAtOurHeight);
            unitTutorial.UnitAgent.updateRotation = false;
            TGoRelax = true;
        }
    }
    public void OnTriggerStay (Collider other)
    {
        if (other.gameObject.tag == "Unit")
        {
            if (TGoRelax)
            {
                if (RelaxTime == 5)
                {
                    unitTutorial.spriteRenderer.enabled = false;
                    unitTutorial.UnitAgent.enabled = false;
                    StartCoroutine(RelaxFive());
                }
                else if (RelaxTime == 8)
                {
                    unitTutorial.spriteRenderer.enabled = false;
                    unitTutorial.UnitAgent.enabled = false;
                    StartCoroutine(RelaxEight());
                }
            }
        }
    }
    IEnumerator RelaxFive()
    {
        
        yield return new WaitForSeconds(25);
        if (!IsSleep)
        {
            unitTutorial.spriteRenderer.enabled = true;

            unitTutorial.FatigueNumber += 4;
            IsSleep = true;
            RelaxTime = 0;
        }

    }
    IEnumerator RelaxEight()
    {
        
        yield return new WaitForSeconds(40);
        if (!IsSleep)
        {
            unitTutorial.spriteRenderer.enabled = true;

            unitTutorial.FatigueNumber = 10;
            IsSleep = true;
            RelaxTime = 0;
        }

    }
}
