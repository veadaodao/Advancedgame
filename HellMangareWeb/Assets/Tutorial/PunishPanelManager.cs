using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunishPanelManager : MonoBehaviour
{
    public GameObject GotoHellPanel;

    // Start is called before the first frame update
    void Start()
    {

        GotoHellPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MouseSelect();
    }
    public void TurnOnPunishPanel()
    {
        if (!GotoHellPanel.activeInHierarchy)
        {
            GotoHellPanel.SetActive(true);

        }


    }

    private void MouseSelect()
    {
        if (Input.GetMouseButtonDown(1) && GotoHellPanel.activeInHierarchy)
        {
            GotoHellPanel.SetActive(false);
        }
    }

}
