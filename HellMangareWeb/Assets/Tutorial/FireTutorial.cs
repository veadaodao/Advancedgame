using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireTutorial : MonoBehaviour
{
    public Transform FireFountain;
    public Transform SoulTutorial;
    public UnitInfoTutorial unitTutorial;
    public GameObject FireEffect;
    public FireFountainWork firefountainwork;
    public HellScoreTutorial ProduceHeat;
    public GameObject GotoHellPanel;
    public Transform PunishManager;
    public Image HeatingProgressBar;
    public Image HeatingProfile;
    private bool IsEffect = false;
    private bool IsHeating = false;
    private bool IsProduce = false;
    private float Timer;

    private void Update()
    {
        HeatingProgressBar.fillAmount = firefountainwork.HeatingTime * 17 / 100f;
    }
    public void GoinFireFountain()
    {
        FindObjectOfType<AudioManager>().Play("click");
        unitTutorial.targetDestination = FireFountain.transform.position;
        unitTutorial.UnitAgent.enabled = true;
        Vector3 destAtOurHeight = new Vector3(unitTutorial.targetDestination.x, SoulTutorial.transform.position.y, unitTutorial.targetDestination.z);
        unitTutorial.UnitAgent.SetDestination(destAtOurHeight);
        unitTutorial.UnitAgent.updateRotation = false;
        unitTutorial.UnitWholeInfo.gameObject.SetActive(false);
        GotoHellPanel.gameObject.SetActive(false);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fountain")
        {
            FindObjectOfType<AudioManager>().Play("fire");
            if (!IsEffect)
            {
                Vector3 EffectPosition = new Vector3(FireFountain.transform.position.x, transform.position.y +1f, FireFountain.transform.position.z);
                Destroy(Instantiate(FireEffect, EffectPosition, Quaternion.Euler(-90f, 0f, 0f)), 60);
                IsEffect = true;
            }
            unitTutorial.RemindText.text = "Working".ToString();
            HeatingProfile.gameObject.SetActive(true);
            StartCoroutine(RelaxFive());
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
                unitTutorial.FatigueNumber -= 1;
                IsHeating = true;
            }
            ProgressBarLoading();
            yield return new WaitForSeconds(10);
            yield return new WaitForSeconds(10);
            yield return new WaitForSeconds(10);
            yield return new WaitForSeconds(10);
            yield return new WaitForSeconds(10);
            yield return new WaitForSeconds(10);
            if (firefountainwork.HeatingTime >= 20)
            {
                if (!IsProduce)
                {
                    ProduceHeat.HellHeat += 3;
                    unitTutorial.FatigueNumber = 10;
                    IsProduce = true;
                    Debug.Log("ProduceHeat.HellHeat" + ProduceHeat.HellHeat);
                }
            }
            else
            {
                if (!IsProduce)
                {
                    unitTutorial.FatigueNumber = 10;
                    ProduceHeat.HellHeat += 2;
                    IsProduce = true;
                    Debug.Log("ProduceHeat.HellHeat" + ProduceHeat.HellHeat);
                }
            }
            yield return new WaitForSeconds(1f);
            firefountainwork.HeatingTime = 0;
            GooutofFountain();
            yield return null;
            
        }
    }
    public void ProgressBarLoading()
    {
        Timer += Time.deltaTime;
        if (Timer >= 10)
        {
            Timer = 0;
            firefountainwork.HeatingTime += 1;
            if (unitTutorial.FatigueNumber > 0)
            {
                unitTutorial.FatigueNumber -= 1;
            }
            if (firefountainwork.HeatingTime < 20)
            {
                firefountainwork.HeatingTime += 1;
            }
        }
    }
    private void GooutofFountain()
    {
        unitTutorial.RemindText.text = "Need to Relax!".ToString();
        unitTutorial.targetDestination = PunishManager.transform.position;
        unitTutorial.UnitAgent.enabled = true;
        Vector3 destAtOurHeight = new Vector3(unitTutorial.targetDestination.x, SoulTutorial.transform.position.y, unitTutorial.targetDestination.z);
        unitTutorial.UnitAgent.SetDestination(destAtOurHeight);
        unitTutorial.UnitAgent.updateRotation = false;
    }
}
