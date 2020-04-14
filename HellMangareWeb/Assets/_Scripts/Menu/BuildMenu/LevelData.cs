using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public GameObject DataPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Update()
    {
        MouseSelect();
    }

    private void MouseSelect()
    {
        FindObjectOfType<AudioManager>().Play("click");
        if (Input.GetMouseButtonDown(1) && DataPanel.activeInHierarchy)
        {
            DataPanel.SetActive(false);
        }

    }

}
