using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ToilLevelAI : MonoBehaviour
{
    private bool arriving;
    Vector3 startPosition;
    Quaternion startRotation;

    public int ToilTimeCount = 0;
    public UnitInfo unitinfo;
    public RealTime realTime;
    private bool IsProduce = false;
    private bool IsEffect;
    public GameObject ToilEffect;
    public SoulScoreManager IncreaseCoal;
    public TMP_Text RemindText;
    public List<GameObject> AITransformPoint;

    enum ENEMY_STATE { S_TOIL1, S_TOIL2, S_IDLE, S_FINISH };
    ENEMY_STATE state;
    void Start()
    {
        arriving = false;
        //state = ENEMY_STATE.S_TOIL1;
    }
    void Update()
    {

        if (unitinfo.ifOnToilLevel)
        {

            startPosition = transform.position;
            startRotation = transform.rotation;

            switch (state)
            {

                case ENEMY_STATE.S_IDLE:
                    if (unitinfo.selected)
                    {

                    }
                    else
                    {
                        state = ENEMY_STATE.S_TOIL1;
                    }
                    break;
                
                case ENEMY_STATE.S_TOIL1:
                    IsProduce = false;
                    RemindText.text = "Working".ToString();
                    if (!arriving && !unitinfo.selected)
                    {
                        unitinfo.UnitAgent.enabled = true;
                        transform.rotation = startRotation;
                        unitinfo.UnitAgent.updateRotation = false;
                        if (!AITransformPoint[0].gameObject.GetComponent<ToilCheckCube>().isUsed)
                        {
                            Vector3 GotoDestination0 = new Vector3(AITransformPoint[0].transform.position.x, unitinfo.transform.position.y, AITransformPoint[0].transform.position.z);
                            unitinfo.targetDestination = GotoDestination0;
                            unitinfo.UnitAgent.SetDestination(unitinfo.targetDestination);
                            //ToilTimeCount += 1;

                        }
                        else
                        {
                            state = ENEMY_STATE.S_TOIL2;
                        }

                    }
                    if (ToilTimeCount>100)
                    {
                        state = ENEMY_STATE.S_FINISH;
                    }
                    if (unitinfo.selected)
                    {
                        state = ENEMY_STATE.S_IDLE;
                    }
                    break;
                case ENEMY_STATE.S_TOIL2:
                    RemindText.text = "Working".ToString();
                    IsProduce = false;
                    if (!arriving && !unitinfo.selected)
                    {
                        unitinfo.UnitAgent.enabled = true;
                        transform.rotation = startRotation;
                        unitinfo.UnitAgent.updateRotation = false;
                        if (!AITransformPoint[1].gameObject.GetComponent<ToilCheckCube>().isUsed)
                        {
                            Vector3 GotoDestination0 = new Vector3(AITransformPoint[1].transform.position.x, unitinfo.transform.position.y, AITransformPoint[1].transform.position.z);
                            unitinfo.targetDestination = GotoDestination0;
                            unitinfo.UnitAgent.SetDestination(unitinfo.targetDestination);
                            //ToilTimeCount += 1;

                        }
                        else
                        {
                            state = ENEMY_STATE.S_TOIL1;
                        }
                    }
                    if (ToilTimeCount > 50)
                    {
                        state = ENEMY_STATE.S_FINISH;
                    }
                    if (unitinfo.selected)
                    {
                        state = ENEMY_STATE.S_IDLE;
                    }
                    break;
                case ENEMY_STATE.S_FINISH:
                    arriving = true;
                    unitinfo.UnitAgent.enabled = false;
                    if (!IsProduce)
                    {
                        IncreaseCoal.HellCoal += 1;
                        IsProduce = true;
                    }
                    unitinfo.FatigueNumber = 1;
                    RemindText.text = "Need to Relax!".ToString();
                    break;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Toil" && !other.gameObject.GetComponent<ToilCheckCube>().isUsed && unitinfo.FatigueNumber >= 2)
        {
            ToilTimeCount += 1;
            Debug.Log(ToilTimeCount);
            if (state == ENEMY_STATE.S_TOIL1)
            {
                state = ENEMY_STATE.S_TOIL2;
            }
            if(state == ENEMY_STATE.S_TOIL2)
            {
                state = ENEMY_STATE.S_TOIL1;
            }
            if (!IsEffect)
            {
                Vector3 EffectPosition = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
                Destroy(Instantiate(ToilEffect, transform.position, Quaternion.Euler(0f, 0f, 0f)), 2f);
                IsEffect = true;
            }
            other.gameObject.GetComponent<ToilCheckCube>().isUsed = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Toil")
        {
            other.gameObject.GetComponent<ToilCheckCube>().isUsed = false;
            arriving = false;
            IsEffect = false;
        }
    }

}
