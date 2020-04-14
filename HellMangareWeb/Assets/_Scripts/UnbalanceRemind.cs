using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnbalanceRemind : MonoBehaviour
{
    public GameObject UnbalanceRemindPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void PressHeatAndCool()
    {
        if (UnbalanceRemindPanel.activeInHierarchy == false)
        {
            UnbalanceRemindPanel.SetActive(true);

        }
        else
        {
            UnbalanceRemindPanel.SetActive(false);
        }
    }
}
