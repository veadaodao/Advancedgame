using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitingTutorial : MonoBehaviour
{
    public UnitInfoTutorial unitTutorial;
    public Transform SoulTutorial;
    public GameObject VisitingCube;
    public RealTime WaitVisiting;
    public bool IsWaiting = false;
    public bool IsVisited = false;
    private float Timer;
    private int VisitingLevel = 0;
    public GameObject WaitingEffect;
    public GameObject VisitingEffect;
    public Transform VisitingGrave;
    public bool ifOnVisiting = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ifOnVisiting)
        {
            switch (VisitingLevel)
            {

                case 1:

                    //Debug.Log("Waiting");
                    RandomVisiting();
                    break;
                case 2:
                    unitTutorial.targetDestination = VisitingGrave.transform.position;
                    unitTutorial.UnitAgent.enabled = true;
                    Vector3 destAtOurHeight = new Vector3(unitTutorial.targetDestination.x - 2f, SoulTutorial.transform.position.y, unitTutorial.targetDestination.z - 2f);
                    unitTutorial.UnitAgent.SetDestination(destAtOurHeight);
                    unitTutorial.UnitAgent.updateRotation = false;
                    if (IsVisited)
                    {
                        unitTutorial.FatigueNumber = 1;
                        unitTutorial.MemoryNumber += 3;
                        IsVisited = false;
                        ifOnVisiting = false;
                    }
                    break;
            }
        }
    }
    public void SoulGotoVisiting()
    {
        if (!ifOnVisiting)
        {
            FindObjectOfType<AudioManager>().Play("click");
            unitTutorial.targetDestination = VisitingCube.transform.position;
            unitTutorial.UnitAgent.enabled = true;
            Vector3 destAtOurHeight = new Vector3(unitTutorial.targetDestination.x, SoulTutorial.transform.position.y, unitTutorial.targetDestination.z);
            unitTutorial.UnitAgent.SetDestination(destAtOurHeight);
            unitTutorial.UnitAgent.updateRotation = false;
            ifOnVisiting = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Grave")
        {
           // Debug.Log("arriving");
            Vector3 WaitingEffectPosition = new Vector3(SoulTutorial.transform.position.x, SoulTutorial.transform.position.y + 2f, SoulTutorial.transform.position.z);
            Destroy(Instantiate(WaitingEffect, WaitingEffectPosition, Quaternion.Euler(0f, 0f, 0f)), 5f);
            unitTutorial.UnitAgent.enabled = false;
            unitTutorial.RemindText.text = "Waiting".ToString();

            StartCoroutine(WaitingChance());

        }
        if (other.gameObject.tag == "CenterGrave")
        {
           
            Vector3 VisitingEffectPosition = new Vector3(SoulTutorial.transform.position.x, SoulTutorial.transform.position.y + 2f, SoulTutorial.transform.position.z);
            Destroy(Instantiate(VisitingEffect, VisitingEffectPosition, Quaternion.Euler(0f, 0f, 0f)), 5f);
            IsVisited = true;
            unitTutorial.RemindText.text = "Visiting".ToString();
        }
    }

    IEnumerator WaitingChance()
    {
        while (true)
        {
            if (!IsWaiting)
            {

                IsWaiting = true;
            }
            RandomVisiting();
            yield return new WaitForSeconds(5);
            yield return new WaitForSeconds(5);
            yield return new WaitForSeconds(5);
            if(VisitingLevel != 2)
            {
                VisitingLevel = 2;
            }

            yield return null;
        }
    }
    public void RandomVisiting()
    {
        Timer += Time.deltaTime;
        if (Timer >= 10)
        {
            Timer = 0;
            int index = Random.Range(1, 3);
            Debug.Log(index);
            switch (index)
            {

                case 1:
                    VisitingLevel = 1;
                    break;
                case 2:
                    VisitingLevel = 2;
                    break;
            }
        }
    }
}
