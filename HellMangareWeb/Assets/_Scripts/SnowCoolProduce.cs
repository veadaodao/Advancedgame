using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SnowCoolProduce : MonoBehaviour
{
    SnowCampManager snowCoolProduceManager;
    public bool selected = false;
    bool justSelected = false;
    public bool isUsed;
    public bool IsSleep;
    public bool IsSetDuration = false;
    public LayerMask layerMask;
    public ChangeCenterAndLeftInfo ChangePanel;
    public int CoolingTime = 0;
    float DoubleClickTime = 0;

    void Start()
    {
        DoubleClickTime = Time.time;
        GameObject snowCampManagerObj = GameObject.Find("SnowCampManager");
        snowCoolProduceManager = snowCampManagerObj.GetComponent<SnowCampManager>();
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
                        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("SnowCamp"))
                        {
                            if (snowCoolProduceManager.selectedSnowCamp = this)
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
                        snowCoolProduceManager.selectSnowCamp(null);
                    }
                }
            }
        }
    }
    public void OnMouseDown()
    {
        if (snowCoolProduceManager.selectedSnowCamp = this)
        {
            FindObjectOfType<AudioManager>().Play("click");
            selected = true;
            justSelected = true;
            snowCoolProduceManager.selectSnowCamp(this);
            ChangePanel.BuildingLevelInfo = 4;
        }

    }
}
