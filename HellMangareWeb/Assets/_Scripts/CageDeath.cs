using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CageDeath : MonoBehaviour
{
    CageManager cageManager;
    public bool selected = false;
    bool justSelected = false;
    public bool isUsed;
    public bool IsSleep;
    public bool IsSetDuration = false;
    public LayerMask layerMask;
    public ChangeCenterAndLeftInfo ChangePanel;
    float DoubleClickTime = 0;


    void Start()
    {
        DoubleClickTime = Time.time;
        GameObject cageManagerObj = GameObject.Find("CageManager");
        cageManager = cageManagerObj.GetComponent<CageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        justSelected = false;
        if (Input.GetMouseButtonDown(1))
        {
            ChangePanel.BuildingLevelInfo = 0;

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
                        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Cage"))
                        {
                            if (cageManager.selectedCage = this)
                            {

                                if (Time.time - DoubleClickTime <= 0.3f)
                                {
                                    // keep warm
                                }
                                DoubleClickTime = Time.time;
                            }
                        }
                    }
                    else
                    {
                        cageManager.selectCage(null);
                    }
                }
            }
        }
    }
    public void OnMouseDown()
    {
        if (cageManager.selectedCage = this)
        {
            FindObjectOfType<AudioManager>().Play("click");
            selected = true;
            justSelected = true;
            cageManager.selectCage(this);
            ChangePanel.BuildingLevelInfo = 8;
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Unit")
        {
            Debug.Log("death");
            //other.gameObject.SetActive(true);
        }
    }
}
