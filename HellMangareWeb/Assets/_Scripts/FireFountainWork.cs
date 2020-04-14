using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireFountainWork : MonoBehaviour
{
    FountainManager fountainManager;
    public bool selected = false;
    bool justSelected = false;
    public bool isUsed;
    public bool IsSleep;
    public bool IsSetDuration = false;
    public LayerMask layerMask;
    public ChangeCenterAndLeftInfo ChangePanel;
    public int HeatingTime = 0;
    float DoubleClickTime = 0;

    void Start()
    {
        DoubleClickTime = Time.time;
        GameObject fountainManagerObj = GameObject.Find("FountainManager");
        fountainManager = fountainManagerObj.GetComponent<FountainManager>();
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
                        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Fountain"))
                        {
                            if (fountainManager.selectedFountain = this)
                            {

                                if (Time.time - DoubleClickTime <= 0.3f)
                                {
                                    if (HeatingTime < 14)
                                    {
                                        HeatingTime += 1;
                                    }
                                }
                                DoubleClickTime = Time.time;
                            }
                        }
                    }
                    else
                    {
                        fountainManager.selectFountain(null);
                    }
                }
            }
        }
    }
    public void OnMouseDown()
    {
        if (fountainManager.selectedFountain = this)
        {
            FindObjectOfType<AudioManager>().Play("click");
            selected = true;
            justSelected = true;
            fountainManager.selectFountain(this);
            ChangePanel.BuildingLevelInfo = 3;
        }

    }
}
