using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebirthTutorial : MonoBehaviour
{
    public Transform RebirthBuilding;
    public UnitInfoTutorial unitTutorial;
    public GameObject SoulTutorial;
    public GameObject RebirthEffect;
    public bool IsFinishFirstTutorial = false;
    private bool IsEffect = false;

    public void GoRebirth()
    {
        if (!IsFinishFirstTutorial)
        {
            FindObjectOfType<AudioManager>().Play("click");
            unitTutorial.targetDestination = RebirthBuilding.transform.position;
            unitTutorial.UnitAgent.enabled = true;
            Vector3 destAtOurHeight = new Vector3(unitTutorial.targetDestination.x, SoulTutorial.transform.position.y, unitTutorial.targetDestination.z);
            unitTutorial.UnitAgent.SetDestination(destAtOurHeight);
            unitTutorial.UnitAgent.updateRotation = false;
            IsFinishFirstTutorial = true;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rebirth")
        {

            if (!IsEffect)
            {
                Vector3 EffectPosition = new Vector3(unitTutorial.targetDestination.x, unitTutorial.targetDestination.y-0.5f, unitTutorial.targetDestination.z);
                Destroy(Instantiate(RebirthEffect, EffectPosition, Quaternion.Euler(-90f, 0f, 0f)), 5f);
                IsEffect = true;
            }
            StartCoroutine(RibirthProcess());
            unitTutorial.RemindText.text = "Reborn".ToString();
        }
    }
    IEnumerator RibirthProcess()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            transform.position = unitTutorial.startPosition;
            gameObject.SetActive(false);
            IsFinishFirstTutorial = false;
        }
    }

}
