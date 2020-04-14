using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrozenTutorial : MonoBehaviour
{
    public Transform SnowCamp;
    public Transform SoulTutorial;
    public UnitInfoTutorial unitTutorial;
    public GameObject FrozenEffect;
    public SnowCoolProduce snowCoolProduce;
    public HellScoreTutorial ProduceHeat;
    public GameObject GotoHellPanel;
    public Transform PunishManager;
    public Image CoolingProgressBar;
    public Image CoolingProfile;
    private bool IsEffect = false;
    private bool IsCooling = false;
    private bool IsProduce = false;
    private float Timer;

    private void Update()
    {
        CoolingProgressBar.fillAmount = snowCoolProduce.CoolingTime * 17 / 100f;
    }
    public void GoinSnowCamp()
    {
        FindObjectOfType<AudioManager>().Play("click");
        unitTutorial.targetDestination = SnowCamp.transform.position;
        unitTutorial.UnitAgent.enabled = true;
        Vector3 destAtOurHeight = new Vector3(unitTutorial.targetDestination.x, SoulTutorial.transform.position.y, unitTutorial.targetDestination.z);
        unitTutorial.UnitAgent.SetDestination(destAtOurHeight);
        unitTutorial.UnitAgent.updateRotation = false;
        unitTutorial.UnitWholeInfo.gameObject.SetActive(false);
        GotoHellPanel.gameObject.SetActive(false);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SnowCamp")
        {
            if (!IsEffect)
            {
                Vector3 EffectPosition = new Vector3(transform.position.x, transform.position.y+2, transform.position.z);
                Destroy(Instantiate(FrozenEffect, EffectPosition, Quaternion.Euler(-90f, 0f, 0f)), 60);
                IsEffect = true;
            }
            unitTutorial.RemindText.text = "Working".ToString();
            CoolingProfile.gameObject.SetActive(true);
            StartCoroutine(RelaxFive());
        }
    }
    IEnumerator RelaxFive()
    {
        while (true)
        {
            if (!IsCooling)
            {
                snowCoolProduce.CoolingTime += 1;
                unitTutorial.FatigueNumber -= 1;
                IsCooling = true;
            }
            ProgressBarLoading();
            yield return new WaitForSeconds(10);
            yield return new WaitForSeconds(10);
            yield return new WaitForSeconds(10);
            yield return new WaitForSeconds(10);
            yield return new WaitForSeconds(10);
            yield return new WaitForSeconds(10);
            if (snowCoolProduce.CoolingTime >= 20)
            {
                if (!IsProduce)
                {
                    ProduceHeat.HellCool += 3;
                    unitTutorial.FatigueNumber = 10;
                    IsProduce = true;
                    Debug.Log(ProduceHeat.HellCool);
                }
            }
            else
            {
                if (!IsProduce)
                {
                    unitTutorial.FatigueNumber = 10;
                    ProduceHeat.HellCool += 2;
                    IsProduce = true;
                    Debug.Log(ProduceHeat.HellCool);
                }
            }
            yield return new WaitForSeconds(1f);
            snowCoolProduce.CoolingTime = 0;
            GooutofSnowCamp();
            yield return null;

        }
    }
    public void ProgressBarLoading()
    {
        Timer += Time.deltaTime;
        if (Timer >= 10)
        {
            Timer = 0;
            snowCoolProduce.CoolingTime += 1;
            if (unitTutorial.FatigueNumber > 0)
            {
                unitTutorial.FatigueNumber -= 1;
            }
            if (snowCoolProduce.CoolingTime < 20)
            {
                snowCoolProduce.CoolingTime += 1;
            }
        }
    }
    private void GooutofSnowCamp()
    {
        unitTutorial.RemindText.text = "Need to Relax!".ToString();
        unitTutorial.targetDestination = PunishManager.transform.position;
        unitTutorial.UnitAgent.enabled = true;
        Vector3 destAtOurHeight = new Vector3(unitTutorial.targetDestination.x, SoulTutorial.transform.position.y, unitTutorial.targetDestination.z);
        unitTutorial.UnitAgent.SetDestination(destAtOurHeight);
        unitTutorial.UnitAgent.updateRotation = false;
    }
}
