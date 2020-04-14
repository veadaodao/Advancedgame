using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoPunish : MonoBehaviour
{
    public GameObject GotoHellPanel;
    public Transform SoulTutorial;
    public UnitInfoTutorial unitTutorial;

    private bool AlreadyInPunish = false;

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
            FindObjectOfType<AudioManager>().Play("click");
        }
        if (!AlreadyInPunish)
        {
            unitTutorial.targetDestination = transform.position;
            unitTutorial.UnitAgent.enabled = true;
            Vector3 destAtOurHeight = new Vector3(unitTutorial.targetDestination.x, SoulTutorial.transform.position.y, unitTutorial.targetDestination.z);
            unitTutorial.UnitAgent.SetDestination(destAtOurHeight);
            unitTutorial.UnitAgent.updateRotation = false;

        }
    }

    private void MouseSelect()
    {
        if (Input.GetMouseButtonDown(1) && GotoHellPanel.activeInHierarchy)
        {
            GotoHellPanel.SetActive(false);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Unit")
        {
            AlreadyInPunish = true;
        }
    }
}
