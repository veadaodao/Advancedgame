using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class BuildingController : MonoBehaviour
{
    

    public GameObject BuildingPanel;
    public GameObject LevelPanel;

    public GameObject DataPanel;

    public Canvas canvas;
    public Canvas UnitWholeInfo;


    public SoulScoreManager soulScore;
    public UIManager uIManager;

    int choicebutton;

    

    // Start is called before the first frame update
    void Start()
    {
        GameObject UIObject = GameObject.Find("UIManager");
        uIManager = UIObject.GetComponent<UIManager>();

        GameObject ScoreObject = GameObject.Find("SoulScoreManager");
        soulScore = ScoreObject.GetComponent<SoulScoreManager>();

    }

    private void Update()
    {
       
        MouseSelect();


    }

    // Update is called once per frame
    public void PressBuild()
    {
        FindObjectOfType<AudioManager>().Play("click");
        if (BuildingPanel.activeInHierarchy == false)
        {
            BuildingPanel.SetActive(true);
            switch (choicebutton)
            {
                case 1:
                    LevelInfo();
                    break;
               
                case 8:
                    LevelData();
                    break;
            }
        }
    }
    private void MouseSelect()
    {
        if (Input.GetMouseButtonDown(1) && BuildingPanel.activeInHierarchy)
        {
            BuildingPanel.SetActive(false);
        }
        else if (Input.GetMouseButtonDown(1) && DataPanel.activeInHierarchy)
        {
            DataPanel.SetActive(false);
        }

    }

    public void LevelInfo()
    {

        FindObjectOfType<AudioManager>().Play("click");
        if (!LevelPanel.activeInHierarchy)
         {
            LevelPanel.SetActive(true);
            DataPanel.SetActive(false);
            BuildingPanel.SetActive(false);

        }

    }



    public void LevelData()
    {
        FindObjectOfType<AudioManager>().Play("click");
        if (!DataPanel.activeInHierarchy)
        {
            DataPanel.SetActive(true);
            LevelPanel.SetActive(false);
            BuildingPanel.SetActive(false);
        }

    }
}

