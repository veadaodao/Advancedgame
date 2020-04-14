using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsController : MonoBehaviour
{
    public GameObject TipPanel;

    public void Start()
    {

    }

    public void PressMenu()

    {
        FindObjectOfType<AudioManager>().Play("click");
        if (TipPanel.activeInHierarchy == false)
        {
            TipPanel.SetActive(true);
        }

        else
        {
            if (Input.GetMouseButtonDown(1))
            {

                TipPanel.SetActive(false);

            }
        }
    }

    public void Update()
    {
        MouseSelect();
    }

    private void MouseSelect()
    {
        if (Input.GetMouseButtonDown(1) && TipPanel.activeInHierarchy)
        {
            TipPanel.SetActive(false);
        }
    }
}

