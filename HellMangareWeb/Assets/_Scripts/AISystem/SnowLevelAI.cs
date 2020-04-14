using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SnowLevelAI : MonoBehaviour
{
    private bool arriving;
    Vector3 startPosition;
    Quaternion startRotation;

    private int selectedDestination;
    public UnitInfo unitinfo;
    public SnowCoolProduce snowCoolProduce;
    public RealTime realTime;
    bool IsCooling = false;
    private float Timer;
    private float WaitToCooling;
    private bool IsProduce = false;
    public int FatigueReduce;
    private bool IsEffect;
    public bool IsGototheLiving = false;
    public Image CoolingProgressBar;
    public Image CoolingProfile;
    public GameObject SnowEffect;
    public TMP_Text RemindText;
    public Transform FinishCubeSnow;
    public SoulScoreManager ProduceCool;
    public List<GameObject> AITransformPoint;

    enum ENEMY_STATE { S_WALK, S_IDLE, S_WORK, S_RELAX };
    ENEMY_STATE state;

    void Start()
    {
        //state = ENEMY_STATE.S_IDLE;
        arriving = false;
    }
    void Update()
    {
        CoolingProgressBar.fillAmount = snowCoolProduce.CoolingTime * 17 / 100f;
        WaitToCooling = realTime.SecondsInAFullDay;

        if (unitinfo.ifOnFrozenLevel)
        {
            startPosition = transform.position;
            startRotation = transform.rotation;

            switch (state)
            {
                case ENEMY_STATE.S_IDLE:
                    CoolingProfile.gameObject.SetActive(false);
                    if (unitinfo.FatigueNumber > 1)
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
                        state = ENEMY_STATE.S_RELAX;
                    }

                    break;
                case ENEMY_STATE.S_RELAX:
                    if (unitinfo.FatigueNumber <= 1)
                    {
                        RemindText.text = "".ToString();
                        if (!IsGototheLiving)
                        {
                            unitinfo.UnitAgent.enabled = true;
                            unitinfo.targetDestination = FinishCubeSnow.transform.position;
                            unitinfo.UnitAgent.SetDestination(unitinfo.targetDestination);
                            IsGototheLiving = true;
                        }
                        //tired   // change it to walk // manully change the position of soul
                        if (unitinfo.selected && IsGototheLiving)
                        {
                            RemindText.text = "Need to relax!".ToString();
                        }
                    }

                    break;
                case ENEMY_STATE.S_WALK:
                    CoolingProfile.gameObject.SetActive(false);
                    IsProduce = false;
                    if (!arriving && !unitinfo.selected)
                    {
                        unitinfo.UnitAgent.enabled = true;
                        selectedDestination = Random.Range(0, AITransformPoint.Count);
                        transform.rotation = startRotation;
                        unitinfo.UnitAgent.updateRotation = false;
                        while (AITransformPoint[selectedDestination].GetComponent<SnowCoolProduce>().isUsed == true)
                        {
                            //Debug.Log("changeDestination");
                            selectedDestination = Random.Range(0, AITransformPoint.Count);

                        }
                        Vector3 GotoDestination = new Vector3(AITransformPoint[selectedDestination].transform.position.x, unitinfo.transform.position.y, AITransformPoint[selectedDestination].transform.position.z);
                        unitinfo.targetDestination = GotoDestination;
                        unitinfo.UnitAgent.SetDestination(unitinfo.targetDestination);

                    }

                    if (unitinfo.UnitAgent.enabled == true && arriving == true)
                    {
                        state = ENEMY_STATE.S_WORK;
                    }
                    if (unitinfo.selected)
                    {
                        state = ENEMY_STATE.S_IDLE;
                    }
                    break;

                case ENEMY_STATE.S_WORK:
                    arriving = true;
                    unitinfo.UnitAgent.enabled = false;
                    CoolingProfile.gameObject.SetActive(true);
                    FindObjectOfType<AudioManager>().Play("snow");
                    if (!IsEffect)
                    {
                        Vector3 EffectPosition = new Vector3(transform.position.x, transform.position.y +1, transform.position.z);
                        Destroy(Instantiate(SnowEffect, EffectPosition, Quaternion.Euler(0f, 0f, 0f)), WaitToCooling);
                        IsEffect = true;
                    }
                    RemindText.text = "Working".ToString();
                    StartCoroutine(WorkforCool());
                    break;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SnowCamp" && !other.gameObject.GetComponent<SnowCoolProduce>().isUsed && unitinfo.FatigueNumber >= 2)
        {
            state = ENEMY_STATE.S_WORK;
            other.gameObject.GetComponent<SnowCoolProduce>().isUsed = true;
        }
        else
        {
            state = ENEMY_STATE.S_IDLE;
        }

        if (other.gameObject.name == "FinishCubeSnow")
        {
            IsGototheLiving = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SnowCamp")
        {
            other.gameObject.GetComponent<SnowCoolProduce>().isUsed = false;
            arriving = false;
            state = ENEMY_STATE.S_IDLE;
        }
    }
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
                        Debug.Log(ProduceCool.HellCool);
                        ProduceCool.HellCool += 3;
                        unitinfo.FatigueNumber = 1;
                        IsProduce = true;
                    }
                }
                else
                {
                    if (!IsProduce)
                    {
                        Debug.Log(ProduceCool.HellCool);
                        unitinfo.FatigueNumber = 1;
                        ProduceCool.HellCool += 2;
                        IsProduce = true;
                    }
                }

                yield return new WaitForSeconds(1f);
                snowCoolProduce.CoolingTime = 0;
                state = ENEMY_STATE.S_RELAX;
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
            snowCoolProduce.CoolingTime += 1;
            if (unitinfo.FatigueNumber > 0)
            {
                unitinfo.FatigueNumber -= 1;
            }
            if (snowCoolProduce.CoolingTime < 20)
            {
                snowCoolProduce.CoolingTime += 1;
            }
        }
    }
}
