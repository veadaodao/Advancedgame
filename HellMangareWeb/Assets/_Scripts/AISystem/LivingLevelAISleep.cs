using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LivingLevelAISleep : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private bool arriving;
    Vector3 startPosition;
    Quaternion startRotation;

    private int selectedDestination;
    public UnitInfo unitinfo;
    public SoulGetSleep soulgetsleep;
    public Transform NewMoreSoul;
    bool IsRelax = false;

    public GameObject LivingCampRemindPanel;
    public TMP_Text RemindText;
    public List<GameObject> AITransformPoint;

    RaycastHit hit;
    Vector3 rayDirection;

    enum STATE { S_WALK, S_IDLE, S_SLEEP};
    STATE state;

    void Start()
    {
        state = STATE.S_IDLE;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        arriving = false;
    }


    void Update()
    {
        if (unitinfo.ifOnLivingLevel)
        {
            switch (state)
            {
                case STATE.S_IDLE:
                    if (unitinfo.FatigueNumber>= 6)
                    {
                        unitinfo.UnitAgent.enabled = true;
                        Vector3 GotoNewSoul = new Vector3(NewMoreSoul.transform.position.x, NewMoreSoul.transform.position.y, NewMoreSoul.transform.position.z);
                        unitinfo.targetDestination = GotoNewSoul;
                        unitinfo.UnitAgent.SetDestination(unitinfo.targetDestination);
                        arriving = false;
                    }
                    else
                    {
                        state = STATE.S_WALK;
                    }                   
                    break;
                case STATE.S_WALK:
                    if (!arriving && !unitinfo.selected)
                    {
                        unitinfo.UnitAgent.enabled = true;
                        selectedDestination = Random.Range(0, AITransformPoint.Count);
                        transform.rotation = startRotation;
                        unitinfo.UnitAgent.updateRotation = false;
                        while (AITransformPoint[selectedDestination].GetComponent<SoulGetSleep>().isUsed == true)
                        {
                            //Debug.Log("changeDestination");
                            selectedDestination = Random.Range(0, AITransformPoint.Count);
                            state = STATE.S_WALK;
                        }
                        Vector3 GotoDestination = new Vector3(AITransformPoint[selectedDestination].transform.position.x - 1, unitinfo.transform.position.y, AITransformPoint[selectedDestination].transform.position.z - 2);
                        unitinfo.targetDestination = GotoDestination;
                        unitinfo.UnitAgent.SetDestination(unitinfo.targetDestination);
                        RemindText.text = "Need to Relax!".ToString();
                    }
                    
                    if (unitinfo.UnitAgent.enabled == true && arriving == true)
                    {
                        state = STATE.S_SLEEP;
                    }
                    if (unitinfo.selected)
                    {
                        state = STATE.S_IDLE;
                    }
                    break;

                case STATE.S_SLEEP:
                    arriving = true;
                    unitinfo.UnitAgent.enabled = false;
                    RemindText.text = "".ToString();
                    if (soulgetsleep.RelaxTime == 5)
                    {
                        spriteRenderer.enabled = false;
                        unitinfo.UnitAgent.enabled = false;
                        StartCoroutine(RelaxFive());
                    }
                    else if (soulgetsleep.RelaxTime == 8)
                    {
                        spriteRenderer.enabled = false;
                        unitinfo.UnitAgent.enabled = false;
                        StartCoroutine(RelaxEight());
                    }
                    else
                    {

                        LivingCampRemindPanel.gameObject.SetActive(true);
                    }
                    break;
            }
        }        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Camp"&& !other.gameObject.GetComponent<SoulGetSleep>().isUsed)
        {
            state = STATE.S_SLEEP;
            other.gameObject.GetComponent<SoulGetSleep>().isUsed = true;           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Camp")
        {
            other.gameObject.GetComponent<SoulGetSleep>().isUsed = false;            
            arriving = false;
            state = STATE.S_WALK;
        }
    }
    IEnumerator RelaxFive()
    {
        LivingCampRemindPanel.gameObject.SetActive(false);
        yield return new WaitForSeconds(25);
        if (!IsRelax)
        {
            spriteRenderer.enabled = true;

            unitinfo.FatigueNumber += 4;
            IsRelax = true;
            soulgetsleep.RelaxTime = 0;
        }
        state = STATE.S_IDLE;
    }
    IEnumerator RelaxEight()
    {
        LivingCampRemindPanel.gameObject.SetActive(false);
        yield return new WaitForSeconds(40);
        if (!IsRelax)
        {
            spriteRenderer.enabled = true;
            
            unitinfo.FatigueNumber = 10;
            IsRelax = true;
            soulgetsleep.RelaxTime = 0;
        }
        state = STATE.S_IDLE;
    }
}