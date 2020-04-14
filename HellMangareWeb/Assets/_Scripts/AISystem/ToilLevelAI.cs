using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ToilLevelAI : MonoBehaviour
{
    private bool arriving;
    Vector3 startPosition;
    Quaternion startRotation;
    private int selectedDestination;
    public int ToilTimeCount = 0;
    public UnitInfo unitinfo;
    public RealTime realTime;
    private bool IsProduce = false;
    private bool IsEffect;
    public GameObject ToilEffect;
    public SoulScoreManager IncreaseCoal;
    public TMP_Text RemindText;
    public List<GameObject> AITransformPoint;

    enum ENEMY_STATE { S_TOIL1, S_TOIL2, S_FINISH };
    ENEMY_STATE state;
    void Start()
    {
        arriving = false;
        state = ENEMY_STATE.S_TOIL1;
    }
    void Update()
    {

        if (unitinfo.ifOnToilLevel)
        {
            switch (state)
            {
                
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
                    if (ToilTimeCount>31)
                    {
                        state = ENEMY_STATE.S_FINISH;
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
                        selectedDestination = Random.Range(1, AITransformPoint.Count);
                        if (AITransformPoint[0].GetComponent<ToilCheckCube>().isUsed == true)
                        {

                            selectedDestination = Random.Range(1, AITransformPoint.Count);

                        }
                        Vector3 GotoDestination = new Vector3(AITransformPoint[selectedDestination].transform.position.x, unitinfo.transform.position.y, AITransformPoint[selectedDestination].transform.position.z);
                        unitinfo.targetDestination = GotoDestination;
                        unitinfo.UnitAgent.SetDestination(unitinfo.targetDestination);
                    }

                    if (ToilTimeCount > 30)
                    {
                        state = ENEMY_STATE.S_FINISH;
                    }
                    break;
                case ENEMY_STATE.S_FINISH:
                    arriving = true;
                    unitinfo.UnitAgent.enabled = true;
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
            FindObjectOfType<AudioManager>().Play("toil");
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
