using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AvariceTutorial : MonoBehaviour
{
    private bool arriving;
    Vector3 startPosition;
    Quaternion startRotation;

    public int AvariceTimeCount = 0;
    public UnitInfoTutorial unitTutorial;
    public RealTime realTime;
    private bool IsProduce = false;
    private bool IsEffect;
    public GameObject AvariceEffect;
    public HellScoreTutorial IncreaseCoin;
    public TMP_Text RemindText;
    public List<GameObject> AITransformPoint;

    enum ENEMY_STATE { S_AVARICE1, S_AVARICE2, S_FINISH };
    ENEMY_STATE state;
    void Start()
    {
        arriving = false;
        //state = ENEMY_STATE.S_TOIL1;
    }
    void Update()
    {

        if (unitTutorial.ifOnAvariceLevel)
        {

            startPosition = transform.position;
            startRotation = transform.rotation;

            switch (state)
            {

                case ENEMY_STATE.S_AVARICE1:
                    IsProduce = false;
                    RemindText.text = "Working".ToString();
                    if (!arriving && !unitTutorial.selected)
                    {
                        unitTutorial.UnitAgent.enabled = true;
                        transform.rotation = startRotation;
                        unitTutorial.UnitAgent.updateRotation = false;
                        if (!AITransformPoint[0].gameObject.GetComponent<AvariceCoin>().isUsed)
                        {
                            Vector3 GotoDestination0 = new Vector3(AITransformPoint[0].transform.position.x+5f, unitTutorial.transform.position.y, AITransformPoint[0].transform.position.z-1f);
                            unitTutorial.targetDestination = GotoDestination0;
                            unitTutorial.UnitAgent.SetDestination(unitTutorial.targetDestination);
                            //ToilTimeCount += 1;

                        }
                        else
                        {
                            state = ENEMY_STATE.S_AVARICE2;
                        }

                    }
                    if (AvariceTimeCount > 9)
                    {
                        state = ENEMY_STATE.S_FINISH;
                    }

                    break;
                case ENEMY_STATE.S_AVARICE2:
                    RemindText.text = "Working".ToString();
                    IsProduce = false;
                    if (!arriving && !unitTutorial.selected)
                    {
                        unitTutorial.UnitAgent.enabled = true;
                        transform.rotation = startRotation;
                        unitTutorial.UnitAgent.updateRotation = false;
                        if (!AITransformPoint[1].gameObject.GetComponent<AvariceCoin>().isUsed)
                        {
                            Vector3 GotoDestination0 = new Vector3(AITransformPoint[1].transform.position.x-1f, unitTutorial.transform.position.y, AITransformPoint[1].transform.position.z);
                            unitTutorial.targetDestination = GotoDestination0;
                            unitTutorial.UnitAgent.SetDestination(unitTutorial.targetDestination);
                            //ToilTimeCount += 1;

                        }
                        else
                        {
                            state = ENEMY_STATE.S_AVARICE1;
                        }
                    }
                    if (AvariceTimeCount > 9)
                    {
                        state = ENEMY_STATE.S_FINISH;
                    }
                    break;
                case ENEMY_STATE.S_FINISH:
                    arriving = true;
                    unitTutorial.UnitAgent.enabled = false;
                    if (!IsProduce)
                    {
                        IncreaseCoin.HellCoin += 1;
                        IsProduce = true;
                    }
                    unitTutorial.FatigueNumber = 10;
                    RemindText.text = "Need to Relax!".ToString();
                    unitTutorial.ifOnAvariceLevel = false;
                    break;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AvariceCoin" && !other.gameObject.GetComponent<AvariceCoin>().isUsed && unitTutorial.FatigueNumber >= 2)
        {
            AvariceTimeCount += 1;
            Debug.Log(AvariceTimeCount);
            if (state == ENEMY_STATE.S_AVARICE1)
            {
                state = ENEMY_STATE.S_AVARICE2;
            }
            if (state == ENEMY_STATE.S_AVARICE2)
            {
                state = ENEMY_STATE.S_AVARICE1;
            }
            if (!IsEffect)
            {
                Vector3 EffectPosition = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
                Destroy(Instantiate(AvariceEffect, EffectPosition, Quaternion.Euler(0f, 0f, 0f)), 2f);
                IsEffect = true;
            }
            other.gameObject.GetComponent<AvariceCoin>().isUsed = true;
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
    }
}
