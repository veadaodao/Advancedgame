using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightHouse: MonoBehaviour
{
    public GameObject HouseLight;
    public GameObject PolicyPanel;
    PolicyManager policyManager;


   
    public int LampOnIndex;
    
    public float x;

    // Start is called before the first frame update
    void Start()
    {

        GameObject PolicyObject = GameObject.Find("PolicyManager");
        policyManager = PolicyObject.GetComponent<PolicyManager>();
        HouseLight.SetActive(false);

        

    }


    // Update is called once per frame
    void Update()
    {
        MouseSelect();

        if (LampOnIndex - x == 3)
        {
            HouseLight.SetActive(true);
        }
        else
        {
            HouseLight.SetActive(false);
            PolicyPanel.SetActive(false);
        }
    }

    public void OnMouseDown()
    {
        if (HouseLight.activeInHierarchy && !PolicyPanel.activeInHierarchy && LampOnIndex >= 3)
        {
            policyManager.ChoosePolicy();
            FindObjectOfType<AudioManager>().Play("click");
        }

    }
    //Click Mouse right key to close Policypanel and jump current policy.
    private void MouseSelect()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (HouseLight.activeInHierarchy && PolicyPanel.activeInHierarchy)
            {
                PolicyPanel.SetActive(false);

            }

        }
    }
}
