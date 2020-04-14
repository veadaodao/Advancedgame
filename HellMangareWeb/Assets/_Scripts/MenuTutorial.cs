using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class MenuTutorial : MonoBehaviour
{
    public GameObject MenuPanel;

    void Update()
    {
        MouseSelect();
    }


    public void PressMenu()
    {
        FindObjectOfType<AudioManager>().Play("click");
        if (!MenuPanel.activeInHierarchy)
        {
            MenuPanel.SetActive(true);
            
        }
    }
    private void MouseSelect()
    {
        if (Input.GetMouseButtonDown(1) && MenuPanel.activeInHierarchy)
        {
            MenuPanel.SetActive(false);
        }
    }
    public void Back()
    {
        FindObjectOfType<AudioManager>().Play("click");
        SceneManager.LoadScene("SampleScene");
    }
}

   