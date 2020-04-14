using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceController : MonoBehaviour
{
    public GameObject ProducePanel;

    public void Start()
    {

    }

    public void PressMenu()
    {
        FindObjectOfType<AudioManager>().Play("click");
        if (ProducePanel.activeInHierarchy == false)
        {
            ProducePanel.SetActive(true);
        }

        else
        {
            if (Input.GetMouseButtonDown(1))
            {

                ProducePanel.SetActive(false);

            }
        }
    }

    public void Update()
    {
        MouseSelect();
    }

    private void MouseSelect()
    {
        if (Input.GetMouseButtonDown(1) && ProducePanel.activeInHierarchy)
        {
            ProducePanel.SetActive(false);
        }
    }
}
