using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AvariceLevelAI : MonoBehaviour
{
    private bool arriving;
    Vector3 startPosition;
    Quaternion startRotation;
    private int selectedDestination;
    public int WorkingTimeCount = 0;
    public UnitInfo unitinfo;
    public RealTime realTime;
    private bool IsProduce = false;
    private bool IsEffect;
    private bool IsChooseDes = false;
    public SoulScoreManager CoinScore;
    public GameObject AvariceEffect;
    public TMP_Text RemindText;
    public List<GameObject> AITransformPoint;

    enum STATE { S_AVARICE1, S_AVARICE2, S_IDLE, S_FINISH };
    STATE state;
    void Start()
    {
        arriving = false;
    }
    void Update()
    {
        if (unitinfo.ifOnAvariceLevel)
        {

            startPosition = transform.position;
            startRotation = transform.rotation;

            switch (state)
            {

                case STATE.S_IDLE:
                    if (unitinfo.selected)
                    {

                    }
                    else
                    {
                        state = STATE.S_AVARICE1;
                    }
                    break;

                case STATE.S_AVARICE1:
                    IsProduce = false;
                    RemindText.text = "Working".ToString();
                    if (!arriving && !unitinfo.selected)
                    {
                        unitinfo.UnitAgent.enabled = true;
                        transform.rotation = startRotation;
                        unitinfo.UnitAgent.updateRotation = false;
                        if (!AITransformPoint[0].gameObject.GetComponent<AvariceCoin>().isUsed)
                        {
                            Vector3 GotoDestination0 = new Vector3(AITransformPoint[0].transform.position.x+5f, unitinfo.transform.position.y, AITransformPoint[0].transform.position.z-1f);
                            unitinfo.targetDestination = GotoDestination0;
                            unitinfo.UnitAgent.SetDestination(unitinfo.targetDestination);

                        }


                    }
                    if (WorkingTimeCount > 100)
                    {
                        state = STATE.S_FINISH;
                    }
                    if (unitinfo.selected)
                    {
                        state = STATE.S_IDLE;
                    }
                    break;
                case STATE.S_AVARICE2:
                    RemindText.text = "Working".ToString();
                    IsProduce = false;
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

                        if (!AITransformPoint[selectedDestination].gameObject.GetComponent<AvariceCoin>().isUsed)
                        {
                            Vector3 GotoDestination0 = new Vector3(AITransformPoint[selectedDestination].transform.position.x, unitinfo.transform.position.y, AITransformPoint[selectedDestination].transform.position.z);
                            unitinfo.targetDestination = GotoDestination0;
                            unitinfo.UnitAgent.SetDestination(unitinfo.targetDestination);
                        }

                    }
                    if (WorkingTimeCount > 50)
                    {
                        state = STATE.S_FINISH;
                    }
                    if (unitinfo.selected)
                    {
                        state = STATE.S_IDLE;
                    }
                    break;
                case STATE.S_FINISH:
                    arriving = true;
                    unitinfo.UnitAgent.enabled = false;

                    unitinfo.FatigueNumber = 1;
                    if (!IsProduce)
                    {
                        CoinScore.HellCoin += 1;
                    }
                    RemindText.text = "Need to Relax!".ToString();
                    break;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AvariceCoin" && !other.gameObject.GetComponent<AvariceCoin>().isUsed)
        {         
            if (IsChooseDes)
            {
                IsChooseDes = false;
            }

            if (!IsEffect)
            {
                Vector3 EffectPosition = new Vector3(transform.position.x, transform.position.y + 8f, transform.position.z);
                Destroy(Instantiate(AvariceEffect, transform.position, Quaternion.Euler(0f, 0f, 0f)), 2f);
                IsEffect = true;
            }
            other.gameObject.GetComponent<AvariceCoin>().isUsed = true;
            state = STATE.S_AVARICE1;
        }

        if(other.gameObject.tag == "AvariceCoinCenter" && !other.gameObject.GetComponent<AvariceCoin>().isUsed)
        {
            WorkingTimeCount += 1;
           
            if (!IsEffect)
            {
                Vector3 EffectPosition = new Vector3(transform.position.x, transform.position.y + 8f, transform.position.z);
                Destroy(Instantiate(AvariceEffect, transform.position, Quaternion.Euler(0f, 0f, 0f)), 2f);
                IsEffect = true;
            }
            other.gameObject.GetComponent<AvariceCoin>().isUsed = true;

            state = STATE.S_AVARICE2;
            
        }

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "AvariceCoin")
        {
            other.gameObject.GetComponent<AvariceCoin>().isUsed = false;
            arriving = false;
            IsEffect = false;

        }
        if (other.gameObject.tag == "AvariceCoinCenter")
        {
            other.gameObject.GetComponent<AvariceCoin>().isUsed = false;
            arriving = false;
            IsEffect = false;

        }
    }

}
