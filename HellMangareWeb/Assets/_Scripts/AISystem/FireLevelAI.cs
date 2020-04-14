using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FireLevelAI : MonoBehaviour
{
    private bool arriving;
    Vector3 startPosition;
    Quaternion startRotation;

    private int selectedDestination;
    public UnitInfo unitinfo;
    public FireFountainWork firefountainwork;
    public RealTime realTime;
    bool IsHeating = false;
    private float Timer;
    private float WaitToHeating;
    private bool IsProduce = false;
    public int FatigueReduce;
    private bool IsEffect;
    public bool IsGototheLiving = false;
    public Image HeatingProgressBar;
    public Image HeatingProfile;
    public GameObject FireEffect;
    public TMP_Text RemindText;
    public Transform FinishCube;
    public SoulScoreManager ProduceHeat;
    public List<GameObject> AITransformPoint;

    enum ENEMY_STATE { S_WALK, S_IDLE, S_WORK, S_RELAX };
    ENEMY_STATE state;

    void Start()
    {
        arriving = false;
    }
    void Update()
    {
        HeatingProgressBar.fillAmount = firefountainwork.HeatingTime * 17 / 100f;
        WaitToHeating = realTime.SecondsInAFullDay;

        if (unitinfo.ifOnFireLevel)
        {
            startPosition = transform.position;
            startRotation = transform.rotation;

            switch (state)
            {
                case ENEMY_STATE.S_IDLE:
                    HeatingProfile.gameObject.SetActive(false);
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
                            unitinfo.targetDestination = FinishCube.transform.position;
                            unitinfo.UnitAgent.SetDestination(unitinfo.targetDestination);
                            IsGototheLiving = true;
                        }
                        //tired   // change it to walk // manully change the position of soul
                        if (unitinfo.selected && IsGototheLiving)
                        {
                            RemindText.text = "Need to relax!";
                        }
                    }

                    break;
                case ENEMY_STATE.S_WALK:
                    HeatingProfile.gameObject.SetActive(false);
                    IsProduce = false;
                    if (!arriving && !unitinfo.selected)
                    {
                        unitinfo.UnitAgent.enabled = true;
                        selectedDestination = Random.Range(0, AITransformPoint.Count);
                        transform.rotation = startRotation;
                        unitinfo.UnitAgent.updateRotation = false;
                        while (AITransformPoint[selectedDestination].GetComponent<FireFountainWork>().isUsed == true)
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
                    HeatingProfile.gameObject.SetActive(true);

                    if (!IsEffect)
                    {
                        Vector3 EffectPosition = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
                        Destroy(Instantiate(FireEffect, EffectPosition, Quaternion.Euler(-90f, 0f, 0f)), WaitToHeating);
                        IsEffect = true;
                    }
                    RemindText.text = "Working".ToString();
                    StartCoroutine(RelaxFive());
                    break;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fountain" && !other.gameObject.GetComponent<FireFountainWork>().isUsed&& unitinfo.FatigueNumber>=2)
        {
            state = ENEMY_STATE.S_WORK;
            other.gameObject.GetComponent<FireFountainWork>().isUsed = true;
            FindObjectOfType<AudioManager>().Play("fire");
        }
        else
        {
            state = ENEMY_STATE.S_IDLE;
        }

        if (other.gameObject.name == "FinishCube")
        {
            IsGototheLiving = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Fountain")
        {
            other.gameObject.GetComponent<FireFountainWork>().isUsed = false;
            arriving = false;
            state = ENEMY_STATE.S_IDLE;
        }
    }
    IEnumerator RelaxFive()
    {
        while (true)
        {
            //Debug.Log(firefountainwork.HeatingTime);
            if (!IsHeating)
            {
                firefountainwork.HeatingTime += 1;
                unitinfo.FatigueNumber -= 1;
                IsHeating = true;
            }
            ProgressBarLoading();
            yield return new WaitForSeconds(WaitToHeating / 6);
            yield return new WaitForSeconds(WaitToHeating / 6);
            yield return new WaitForSeconds(WaitToHeating / 6);
            yield return new WaitForSeconds(WaitToHeating / 6);
            yield return new WaitForSeconds(WaitToHeating / 6);
            yield return new WaitForSeconds(WaitToHeating / 6);
            if (unitinfo.FatigueNumber <= 4)
            {

                if (firefountainwork.HeatingTime >= 20)
                {
                    if (!IsProduce)
                    {
                        ProduceHeat.HellHeat += 3;
                        unitinfo.FatigueNumber = 1;
                        IsProduce = true;
                        Debug.Log("ProduceHeat.HellHeat"+ProduceHeat.HellHeat);
                    }
                }
                else
                {
                    if (!IsProduce)
                    {
                        unitinfo.FatigueNumber = 1;
                        ProduceHeat.HellHeat += 2;
                        IsProduce = true;
                        Debug.Log("ProduceHeat.HellHeat" + ProduceHeat.HellHeat);
                    }
                }

                yield return new WaitForSeconds(1f);
                firefountainwork.HeatingTime = 0;
                state = ENEMY_STATE.S_RELAX;
                yield return null;
            }
        }
    }
    public void ProgressBarLoading()
    {
        Timer += Time.deltaTime;
        if (Timer >= WaitToHeating/6)
        {
            Timer = 0;
            firefountainwork.HeatingTime += 1;
            if (unitinfo.FatigueNumber > 0)
            {
                unitinfo.FatigueNumber -= 1;
            }
            if (firefountainwork.HeatingTime < 20)
            {
                firefountainwork.HeatingTime += 1;
            }
        }
    }
}
