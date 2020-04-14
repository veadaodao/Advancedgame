using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ToilTutorial : MonoBehaviour
{
    private bool arriving;
    Vector3 startPosition;
    Quaternion startRotation;

    public int ToilTimeCount = 0;
    public UnitInfoTutorial unitTutorial;
    public RealTime realTime;
    private bool IsProduce = false;
    private bool IsEffect;
    public GameObject ToilEffect;
    public HellScoreTutorial IncreaseCoal;
    public TMP_Text RemindText;
    public List<GameObject> AITransformPoint;

    enum ENEMY_STATE { S_TOIL1, S_TOIL2, S_FINISH };
    ENEMY_STATE state;
    void Start()
    {
        arriving = false;
        //state = ENEMY_STATE.S_TOIL1;
    }
    void Update()
    {

        if (unitTutorial.ifOnToilLevel)
        {

            startPosition = transform.position;
            startRotation = transform.rotation;

            switch (state)
            {

                case ENEMY_STATE.S_TOIL1:
                    IsProduce = false;
                    RemindText.text = "Working".ToString();
                    if (!arriving && !unitTutorial.selected)
                    {
                        unitTutorial.UnitAgent.enabled = true;
                        transform.rotation = startRotation;
                        unitTutorial.UnitAgent.updateRotation = false;
                        if (!AITransformPoint[0].gameObject.GetComponent<ToilCheckCube>().isUsed)
                        {
                            Vector3 GotoDestination0 = new Vector3(AITransformPoint[0].transform.position.x, unitTutorial.transform.position.y, AITransformPoint[0].transform.position.z);
                            unitTutorial.targetDestination = GotoDestination0;
                            unitTutorial.UnitAgent.SetDestination(unitTutorial.targetDestination);
                            //ToilTimeCount += 1;

                        }
                        else
                        {
                            state = ENEMY_STATE.S_TOIL2;
                        }

                    }
                    if (ToilTimeCount > 8)
                    {
                        state = ENEMY_STATE.S_FINISH;
                    }

                    break;
                case ENEMY_STATE.S_TOIL2:
                    RemindText.text = "Working".ToString();
                    IsProduce = false;
                    if (!arriving && !unitTutorial.selected)
                    {
                        unitTutorial.UnitAgent.enabled = true;
                        transform.rotation = startRotation;
                        unitTutorial.UnitAgent.updateRotation = false;
                        if (!AITransformPoint[1].gameObject.GetComponent<ToilCheckCube>().isUsed)
                        {
                            Vector3 GotoDestination0 = new Vector3(AITransformPoint[1].transform.position.x, unitTutorial.transform.position.y, AITransformPoint[1].transform.position.z);
                            unitTutorial.targetDestination = GotoDestination0;
                            unitTutorial.UnitAgent.SetDestination(unitTutorial.targetDestination);
                            //ToilTimeCount += 1;

                        }
                        else
                        {
                            state = ENEMY_STATE.S_TOIL1;
                        }
                    }
                    if (ToilTimeCount > 8)
                    {
                        state = ENEMY_STATE.S_FINISH;
                    }
                    break;
                case ENEMY_STATE.S_FINISH:
                    arriving = true;
                    unitTutorial.UnitAgent.enabled = false;
                    if (!IsProduce)
                    {
                        IncreaseCoal.HellCoal += 1;
                        IsProduce = true;
                    }
                    unitTutorial.FatigueNumber = 10;
                    RemindText.text = "Need to Relax!".ToString();
                    unitTutorial.ifOnToilLevel = false;
                    break;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Toil" && !other.gameObject.GetComponent<ToilCheckCube>().isUsed && unitTutorial.FatigueNumber >= 2)
        {
            ToilTimeCount += 1;
            Debug.Log(ToilTimeCount);
            if (state == ENEMY_STATE.S_TOIL1)
            {
                state = ENEMY_STATE.S_TOIL2;
            }
            if (state == ENEMY_STATE.S_TOIL2)
            {
                state = ENEMY_STATE.S_TOIL1;
            }
            if (!IsEffect)
            {
                Vector3 EffectPosition = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
                Destroy(Instantiate(ToilEffect, EffectPosition, Quaternion.Euler(0f, 0f, 0f)), 2f);
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
