using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class RebirthLevelAI : MonoBehaviour
{
    private bool arriving;
    Vector3 startPosition;
    Quaternion startRotation;

    private int selectedDestination;
    public UnitInfo unitinfo;
    public RealTime realTime;
    private float WaitToHeating;
    private bool IsProduce = false;
    private bool IsEffect;
    public GameObject RebirthEffect;
    public TMP_Text RemindText;
    public Image Water;
    public SoulScoreManager UsingWater;
    public List<GameObject> AITransformPoint;

    enum ENEMY_STATE { S_REBIRTHGOOD, S_REBIRTHMID, S_REBIRTHBAD, S_WORK };
    ENEMY_STATE state;
    void Start()
    {
        arriving = false;
    }
    void Update()
    {
        WaitToHeating = realTime.SecondsInAFullDay;

        if (unitinfo.ifOnSolitudeLevel)
        {
            
            startPosition = transform.position;
            startRotation = transform.rotation;

            switch (state)
            {
                case ENEMY_STATE.S_REBIRTHGOOD:
                    IsProduce = false;
                    if (!arriving && !unitinfo.selected)
                    {
                        unitinfo.UnitAgent.enabled = true;
                        selectedDestination = Random.Range(0, AITransformPoint.Count);
                        transform.rotation = startRotation;
                        unitinfo.UnitAgent.updateRotation = false;
                        while (AITransformPoint[selectedDestination].GetComponent<RebirthBuilding>().isUsed == true)
                        {
                            selectedDestination = Random.Range(0, 1);
                        }
                        Vector3 GotoDestination = new Vector3(AITransformPoint[selectedDestination].transform.position.x, unitinfo.transform.position.y, AITransformPoint[selectedDestination].transform.position.z);
                        unitinfo.targetDestination = GotoDestination;
                        unitinfo.UnitAgent.SetDestination(unitinfo.targetDestination);
                    }

                    if (unitinfo.UnitAgent.enabled == true && arriving == true)
                    {
                        state = ENEMY_STATE.S_WORK;
                    }

                    if (unitinfo.SpiritNumber > 2 && unitinfo.SpiritNumber <= 6)
                    {
                        state = ENEMY_STATE.S_REBIRTHMID;
                    }
                    else if (unitinfo.SpiritNumber <= 2)
                    {
                        state = ENEMY_STATE.S_REBIRTHBAD;
                    }
                    break;
                case ENEMY_STATE.S_REBIRTHMID:
                    IsProduce = false;
                    if (!arriving && !unitinfo.selected)
                    {
                        unitinfo.UnitAgent.enabled = true;
                        selectedDestination = Random.Range(0, AITransformPoint.Count);
                        transform.rotation = startRotation;
                        unitinfo.UnitAgent.updateRotation = false;
                        while (AITransformPoint[selectedDestination].GetComponent<RebirthBuilding>().isUsed == true)
                        {
                            selectedDestination = Random.Range(1, 3);
                        }
                        Vector3 GotoDestination = new Vector3(AITransformPoint[selectedDestination].transform.position.x, unitinfo.transform.position.y, AITransformPoint[selectedDestination].transform.position.z);
                        unitinfo.targetDestination = GotoDestination;
                        unitinfo.UnitAgent.SetDestination(unitinfo.targetDestination);
                    }
                    if (unitinfo.UnitAgent.enabled == true && arriving == true)
                    {
                        state = ENEMY_STATE.S_WORK;
                    }

                    if (unitinfo.SpiritNumber < 2)
                    {
                        state = ENEMY_STATE.S_REBIRTHMID;
                    }
                    else if (unitinfo.SpiritNumber > 6)
                    {
                        state = ENEMY_STATE.S_REBIRTHGOOD;
                    }
                    break;
                case ENEMY_STATE.S_REBIRTHBAD:
                    IsProduce = false;
                    if (!arriving && !unitinfo.selected)
                    {
                        unitinfo.UnitAgent.enabled = true;
                        selectedDestination = Random.Range(0, AITransformPoint.Count);
                        transform.rotation = startRotation;
                        unitinfo.UnitAgent.updateRotation = false;
                        while (AITransformPoint[selectedDestination].GetComponent<RebirthBuilding>().isUsed == true)
                        {
                            selectedDestination = Random.Range(3, 4);
                        }
                        Vector3 GotoDestination = new Vector3(AITransformPoint[selectedDestination].transform.position.x, unitinfo.transform.position.y, AITransformPoint[selectedDestination].transform.position.z);
                        unitinfo.targetDestination = GotoDestination;
                        unitinfo.UnitAgent.SetDestination(unitinfo.targetDestination);
                    }
                    if (unitinfo.UnitAgent.enabled == true && arriving == true)
                    {
                        state = ENEMY_STATE.S_WORK;
                    }

                    if (unitinfo.SpiritNumber > 2 && unitinfo.SpiritNumber<=6)
                    {
                        state = ENEMY_STATE.S_REBIRTHMID;
                    }else if (unitinfo.SpiritNumber > 6)
                    {
                        state = ENEMY_STATE.S_REBIRTHGOOD;
                    }
                    
                    break;
                case ENEMY_STATE.S_WORK:
                    arriving = true;
                    unitinfo.UnitAgent.enabled = false;
                    if (!IsEffect)
                    {
                        Vector3 EffectPosition = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
                        Destroy(Instantiate(RebirthEffect, EffectPosition, Quaternion.Euler(-90f, 0f, 0f)), WaitToHeating);
                        IsEffect = true;

                    }
                    if (unitinfo.selected)
                    {
                        Water.gameObject.SetActive(true);
                        StartCoroutine(RibirthProcess());
                        unitinfo.selected = false;
                        FindObjectOfType<AudioManager>().Play("water");
                    }

                    RemindText.text = "Reborn".ToString();
                    break;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rebirth" && !other.gameObject.GetComponent<RebirthBuilding>().isUsed && unitinfo.FatigueNumber >= 2)
        {
            state = ENEMY_STATE.S_WORK;
            other.gameObject.GetComponent<RebirthBuilding>().isUsed = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Rebirth")
        {
            other.gameObject.GetComponent<RebirthBuilding>().isUsed = false;
            arriving = false;
        }
    }
    IEnumerator RibirthProcess()
    {
        while (true)
        {
            if (!IsProduce)
            {
                transform.Translate(0, 1, 0);
                IsProduce = true;
                UsingWater.HellWater -= 1;
            }
            yield return new WaitForSeconds(WaitToHeating);
            Destroy(this.gameObject);
        }
    }
}
