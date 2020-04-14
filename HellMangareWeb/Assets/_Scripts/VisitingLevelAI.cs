using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class VisitingLevelAI : MonoBehaviour
{
    private bool arriving;
    Vector3 startPosition;
    Quaternion startRotation;

    private int selectedDestination;
    public UnitInfo unitinfo;
    public RealTime realTime;
    private float Timer;
    private float WaitToVisiting;
    private bool IsVisited = false;
    private bool IsWaiting = false;
    private bool IncreaseMemory = false;
    private bool IsChooseDes;
    private bool IsEffectVisiting;
    private bool IsEffectWaiting;
    public GameObject VisitingEffect;
    public GameObject WaitingEffect;
    public Transform VisitingLevelManager;
    public TMP_Text RemindText;
    public List<GameObject> AITransformPoint;

    enum STATE { S_VISITING, S_IDLE, S_WAITING, S_ARRIVING };
    STATE state;
    void Start()
    {
        arriving = false;
        state = STATE.S_ARRIVING;
    }
    void Update()
    {
        WaitToVisiting = realTime.SecondsInAFullDay;

        if (unitinfo.ifOnVisitingLevel)
        {

            startPosition = transform.position;
            startRotation = transform.rotation;

            switch (state)
            {

                case STATE.S_IDLE:
                    if (IsVisited)
                    {
                        RemindText.text = "Go back".ToString();
                        unitinfo.UnitAgent.enabled = true;
                        Vector3 ReadyToGo = new Vector3(VisitingLevelManager.transform.position.x + 5f, VisitingLevelManager.transform.position.y, VisitingLevelManager.transform.position.z);
                        unitinfo.UnitAgent.SetDestination(ReadyToGo);
                    }
                    else
                    {
                        RemindText.text = "No vistors".ToString();
                        unitinfo.UnitAgent.enabled = true;
                        Vector3 ReadyToGo = new Vector3(VisitingLevelManager.transform.position.x+5f, VisitingLevelManager.transform.position.y, VisitingLevelManager.transform.position.z);
                        unitinfo.UnitAgent.SetDestination(ReadyToGo);
                    }
                    break;
                case STATE.S_VISITING:
                    IsVisited = true;
                    if (!arriving && !unitinfo.selected)
                    {
                        unitinfo.UnitAgent.enabled = true;
                        transform.rotation = startRotation;
                        unitinfo.UnitAgent.updateRotation = false;
                        if (!AITransformPoint[0].gameObject.GetComponent<VisitingGrave>().isUsed)
                        {
                            Vector3 GotoDestination1 = new Vector3(AITransformPoint[0].transform.position.x - 2f, unitinfo.transform.position.y, AITransformPoint[0].transform.position.z - 2f);
                            unitinfo.targetDestination = GotoDestination1;
                            unitinfo.UnitAgent.SetDestination(unitinfo.targetDestination);
                            Debug.Log(unitinfo.MemoryNumber);
                        }

                    }
                    break;

                case STATE.S_ARRIVING:
                    IsVisited = false;
                    if (!arriving && !unitinfo.selected)
                    {
                        unitinfo.UnitAgent.enabled = true;
                        transform.rotation = startRotation;
                        unitinfo.UnitAgent.updateRotation = false;
                        if (!IsChooseDes)
                        {
                            selectedDestination = Random.Range(1, AITransformPoint.Count);
                            IsChooseDes = true;
                        }
                        if (!AITransformPoint[selectedDestination].gameObject.GetComponent<VisitingGrave>().isUsed)
                        {
                            Vector3 GotoDestination = new Vector3(AITransformPoint[selectedDestination].transform.position.x, unitinfo.transform.position.y, AITransformPoint[selectedDestination].transform.position.z);
                            unitinfo.targetDestination = GotoDestination;
                            unitinfo.UnitAgent.SetDestination(unitinfo.targetDestination);

                        }
                    }
                    if ( arriving == true)
                    {
                        state = STATE.S_VISITING;
                    }
                    if (unitinfo.selected)
                    {
                        state = STATE.S_IDLE;
                    }
                    break;

                case STATE.S_WAITING:
                    //arriving = true;
                    unitinfo.UnitAgent.enabled = false;
                    RemindText.text = "Waiting".ToString();
                    StartCoroutine(WaitingChance());
                    break;

            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Grave" && !other.gameObject.GetComponent<VisitingGrave>().isUsed)
        {
            if (IsChooseDes)
            {
                IsChooseDes = false;
            }
            if (!IsEffectWaiting)
            {
                Vector3 EffectPosition = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
                Destroy(Instantiate(WaitingEffect, EffectPosition, Quaternion.Euler(0f, 0f, 0f)), WaitToVisiting);
                IsEffectWaiting = true;
            }
            state = STATE.S_WAITING;
            //arriving = true;
            other.gameObject.GetComponent<VisitingGrave>().isUsed = true;
        }
        if (other.gameObject.tag == "CenterGrave" && !other.gameObject.GetComponent<VisitingGrave>().isUsed)
        {
            other.gameObject.GetComponent<VisitingGrave>().isUsed = true;
            RemindText.text = "Visiting".ToString();
            if (!IsEffectVisiting)
            {
                Vector3 EffectPosition = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
                Destroy(Instantiate(VisitingEffect, EffectPosition, Quaternion.Euler(0f, 0f, 0f)), WaitToVisiting);
                IsEffectVisiting = true;
            }
            if (!IncreaseMemory)
            {
                unitinfo.MemoryNumber += Random.Range(1, 3);
                IncreaseMemory = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Grave")
        {
            other.gameObject.GetComponent<VisitingGrave>().isUsed = false;
            arriving = false;
        }
        if (other.gameObject.tag == "CenterGrave")
        {
            other.gameObject.GetComponent<VisitingGrave>().isUsed = false;
            arriving = false;
        }
    }
    IEnumerator WaitingChance()
    {
        while (true)
        {
            //Debug.Log(firefountainwork.HeatingTime);
            if (!IsWaiting)
            {

                IsWaiting = true;
            }
            RandomVisiting();
            yield return new WaitForSeconds(WaitToVisiting / 6);
            yield return new WaitForSeconds(WaitToVisiting / 6);
            yield return new WaitForSeconds(WaitToVisiting / 6);
            yield return new WaitForSeconds(WaitToVisiting / 6);
            yield return new WaitForSeconds(WaitToVisiting / 6);
            yield return new WaitForSeconds(WaitToVisiting / 6);
            if (IsVisited)
            {
                yield return new WaitForSeconds(6);
                unitinfo.FatigueNumber = 1;
                state = STATE.S_IDLE;

            }
            else
            {
                state = STATE.S_IDLE;
            }
        }
    }
    public void RandomVisiting()
    {
        Timer += Time.deltaTime;
        if (Timer >= WaitToVisiting / 6)
        {
            Timer = 0;
            int index = Random.Range(1, 6);
            Debug.Log(index);
            switch (index)
            {

                case 1:
                    state = STATE.S_VISITING;
                    break;
                case 2:
                    state = STATE.S_WAITING;
                    break;
                case 3:
                    state = STATE.S_VISITING;
                    break;
                case 4:
                    state = STATE.S_WAITING;
                    break;
                case 5:
                    state = STATE.S_WAITING;
                    break;
                case 6:
                    state = STATE.S_VISITING;
                    break;

            }
        }
    }
}
