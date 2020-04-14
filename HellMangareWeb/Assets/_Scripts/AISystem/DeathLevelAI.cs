using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DeathLevelAI : MonoBehaviour
{
    //private int MaxTime = 3;
    //private int MinTime = 1;

    private bool arriving;
    Vector3 startPosition;
    Quaternion startRotation;

    private int selectedDestination;
    public UnitInfo unitinfo;
    public CageDeath cageDeath;
    public RealTime realTime;
    //bool IsDeath = false;
    //private float Timer;
    private float WaitToCooling;
    private bool IsProduce = false;
    public int FatigueReduce;
    private bool IsEffect;
    public bool IsGototheLiving = false;
    public Image DeathProfile;
    public GameObject DeathEffect;
    public TMP_Text RemindText;
    //public Transform FinishCubeDeath;
    public SoulScoreManager DeathSoul;
    public List<GameObject> AITransformPoint;

    enum ENEMY_STATE { S_WALK, S_IDLE, S_DEATH };
    ENEMY_STATE state;

    void Start()
    {
        arriving = false;
        state = ENEMY_STATE.S_WALK;
    }

    void Update()
    {
        WaitToCooling = realTime.SecondsInAFullDay;

        if (unitinfo.ifOnDeathLevel)
        {
            startPosition = transform.position;
            startRotation = transform.rotation;

            switch (state)
            {
                case ENEMY_STATE.S_IDLE:
                    DeathProfile.gameObject.SetActive(false);
                    if (unitinfo.SpiritNumber <= 1)
                    {
                        if (unitinfo.selected)
                        {
                            unitinfo.UnitAgent.enabled = true;
                            Vector3 GotoDestination = new Vector3(AITransformPoint[selectedDestination].transform.position.x, unitinfo.transform.position.y, AITransformPoint[selectedDestination].transform.position.z);
                            unitinfo.targetDestination = GotoDestination;
                            unitinfo.UnitAgent.SetDestination(unitinfo.targetDestination);
                        }
                        else
                        {
                            state = ENEMY_STATE.S_WALK;
                        }
                    }
                    else
                    {
                        state = ENEMY_STATE.S_DEATH;
                    }
                    break;
                case ENEMY_STATE.S_WALK:
                    DeathProfile.gameObject.SetActive(false);
                    IsProduce = false;
                    if (!arriving && !unitinfo.selected)
                    {
                        unitinfo.UnitAgent.enabled = true;
                        selectedDestination = Random.Range(0, AITransformPoint.Count);
                        transform.rotation = startRotation;
                        unitinfo.UnitAgent.updateRotation = false;
                        while (AITransformPoint[selectedDestination].GetComponent<CageDeath>().isUsed == true)
                        {
                            selectedDestination = Random.Range(0, AITransformPoint.Count);
                        }
                        Vector3 GotoDestination = new Vector3(AITransformPoint[selectedDestination].transform.position.x, unitinfo.transform.position.y, AITransformPoint[selectedDestination].transform.position.z);
                        unitinfo.targetDestination = GotoDestination;
                        unitinfo.UnitAgent.SetDestination(unitinfo.targetDestination);

                    }

                    if (unitinfo.UnitAgent.enabled == true && arriving == true)
                    {
                        state = ENEMY_STATE.S_DEATH;
                    }
                    if (unitinfo.selected)
                    {
                        state = ENEMY_STATE.S_IDLE;
                    }
                    break;

                case ENEMY_STATE.S_DEATH:
                    arriving = true;
                    unitinfo.UnitAgent.enabled = false;
                    DeathProfile.gameObject.SetActive(true);

                    if (!IsEffect)
                    {
                        Vector3 EffectPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                        Destroy(Instantiate(DeathEffect, EffectPosition, Quaternion.Euler(0f, 0f, 0f)), WaitToCooling);
                        IsEffect = true;
                    }
                    //StartCoroutine(WorkforCool());
                    break;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cage" && !other.gameObject.GetComponent<CageDeath>().isUsed && unitinfo.SpiritNumber >= 2)
        {
            state = ENEMY_STATE.S_DEATH;
            other.gameObject.GetComponent<CageDeath>().isUsed = true;
        }
        else
        {
            state = ENEMY_STATE.S_IDLE;
        }

        if (other.gameObject.name == "FinishCubeDeath")
        {
            IsGototheLiving = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cage")
        {
            other.gameObject.GetComponent<CageDeath>().isUsed = false;
            arriving = false;
            state = ENEMY_STATE.S_IDLE;
        }
    }
    /*
    IEnumerator WorkforCool()
    {
        while (true)
        {
            if (!IsCooling)
            {
                snowCoolProduce.CoolingTime += 1;
                unitinfo.FatigueNumber -= 1;
                IsCooling = true;
            }
            ProgressBarLoading();
            yield return new WaitForSeconds(WaitToCooling / 6);
            yield return new WaitForSeconds(WaitToCooling / 6);
            yield return new WaitForSeconds(WaitToCooling / 6);
            yield return new WaitForSeconds(WaitToCooling / 6);
            yield return new WaitForSeconds(WaitToCooling / 6);
            yield return new WaitForSeconds(WaitToCooling / 6);
            if (unitinfo.FatigueNumber <= 4)
            {
                if (snowCoolProduce.CoolingTime >= 20)
                {
                    if (!IsProduce)
                    {
                        Debug.Log(DeathSoul.HellCool);
                        DeathSoul.HellCool += 2;
                        unitinfo.FatigueNumber = 1;
                        IsProduce = true;
                    }
                }
                else
                {
                    if (!IsProduce)
                    {
                        Debug.Log(DeathSoul.HellCool);
                        unitinfo.FatigueNumber = 1;
                        DeathSoul.HellCool += 1;
                        IsProduce = true;
                    }
                }

                yield return new WaitForSeconds(1f);
                snowCoolProduce.CoolingTime = 0;
                state = ENEMY_STATE.S_REBIRTH;
                yield return null;
            }
        }
    }
    public void ProgressBarLoading()
    {
        Timer += Time.deltaTime;
        if (Timer >= WaitToCooling / 6)
        {
            Timer = 0;
            cageDeath.CoolingTime += 1;
            if (unitinfo.FatigueNumber > 0)
            {
                unitinfo.FatigueNumber -= 1;
            }
            if (cageDeath.CoolingTime < 20)
            {
                cageDeath.CoolingTime += 1;
            }
        }
    }*/
}
