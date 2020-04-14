using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolicyManager : MonoBehaviour
{
    public static PolicyManager instance;

    public Policy[] policies;
    public PolicyButton[] policyButtons;
    public Policy activePolicy;

    SoulScoreManager soulScore;
    LightHouse lightHouse;
  



    public bool selected = false;

    public GameObject PolicyPanel;
    public GameObject PolicyMassage;
    public GameObject P1Button;
    public GameObject P2Button;
    public GameObject LeftPanel;
    public GameObject RightPanel;
    public GameObject PolicyTree1Panel;
    public GameObject PolicyTree2Panel;
    public GameObject SignButton;

    int policyItem;
    bool ifPolicy1 = false;
    bool ifPolicy2 = false;

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameObject ScoreObject = GameObject.Find("SoulScoreManager");
        soulScore = ScoreObject.GetComponent<SoulScoreManager>();
        GameObject LightObject = GameObject.Find("LightHouse");
        lightHouse = LightObject.GetComponent<LightHouse>();
    }


    public void ChoosePolicy()
    {
        FindObjectOfType<AudioManager>().Play("click");
        PolicyPanel.SetActive(true);

        if (!ifPolicy1 && !ifPolicy2)
        {
            P1Button.SetActive(true);
            P2Button.SetActive(true);
            switch (policyItem)
            {
                case 1:

                    Policy1();
                    break;
                case 2:
                    Policy2();
                    break;
            }
        }
        if (ifPolicy1 && !ifPolicy2)
        {
            Policy1();

        }

        if (ifPolicy2 &&!ifPolicy1)
        {
            Policy2();
        }


    }

    public void Policy1()
    {
        FindObjectOfType<AudioManager>().Play("click");
        ifPolicy1 = true;
        P1Button.SetActive(false);
        P2Button.SetActive(false);
        LeftPanel.SetActive(true);
        RightPanel.SetActive(true);
        PolicyTree1Panel.SetActive(true);
        SignButton.SetActive(true);



        for (int i = 0; i < policyButtons.Length; i++)
        {

            if(lightHouse.x == 0 && i == 0)
            {
                activePolicy = policyButtons[i].GetComponent<Policy>();
                activePolicy.GetComponent<Button>().interactable = true;


            }
            else if (lightHouse.x == 3 && i == 1)
            {
                activePolicy = policyButtons[i].GetComponent<Policy>();
                activePolicy.GetComponent<Button>().interactable = true;


            }
            else if (lightHouse.x == 6 && i == 2)
            {
                activePolicy = policyButtons[i].GetComponent<Policy>();
                activePolicy.GetComponent<Button>().interactable = true;

                    
            }
            else if (lightHouse.x == 9 && i == 3)
            {
                activePolicy = policyButtons[i].GetComponent<Policy>();
                activePolicy.GetComponent<Button>().interactable = true;
            }
        }
    }

    public void Policy2()
    {
        FindObjectOfType<AudioManager>().Play("click");
        ifPolicy2 = true;

        P1Button.SetActive(false);
        P2Button.SetActive(false);
        LeftPanel.SetActive(true);
        RightPanel.SetActive(true);
        PolicyTree2Panel.SetActive(true);
        SignButton.SetActive(true);



        for (int i = 0; i < policyButtons.Length; i++)
        {

            if (lightHouse.x == 0 && i == 4)
            {
                activePolicy = policyButtons[i].GetComponent<Policy>();
                activePolicy.GetComponent<Button>().interactable = true;
                activePolicy.PolicyId = 4;


            }
            else if (lightHouse.x == 3 && i == 5)
            {
                activePolicy = policyButtons[i].GetComponent<Policy>();
                activePolicy.GetComponent<Button>().interactable = true;

            }
            else if (lightHouse.x == 6 && i == 6)
            {

                activePolicy = policyButtons[i].GetComponent<Policy>();
                activePolicy.GetComponent<Button>().interactable = true;


            }
            else if (lightHouse.x == 9 && i == 7)
            {
                activePolicy = policyButtons[i].GetComponent<Policy>();
                activePolicy.GetComponent<Button>().interactable = true;

            }
        }

    }
    //Show SignButton


    public void PressSignButton()
    {
        FindObjectOfType<AudioManager>().Play("click");
        activePolicy.isSigned = true;
        SignPolicy();
        lightHouse.x += 3;
        SignButton.SetActive(false);
        Debug.Log("x =" + lightHouse.x);
        
    }

    //Policy Rules
    public void SignPolicy()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        for (int j = 0; j < units.Length; j++)
        {
            UnitInfo unit = units[j].GetComponent<UnitInfo>();

            //Sign FAITH POLICY
            if (unit.ifOnVisitingLevel && activePolicy.PolicyId == 0 && activePolicy.isSigned)
            {
                unit.MemoryNumber = 2 * unit.MemoryNumber;
                unit.SpiritNumber -= 1;
                Debug.Log("Faith loaded");
            }

            //Sign LANTERN POLICY
            if (activePolicy.PolicyId == 1 && activePolicy.isSigned)
            {
                unit.MemoryNumber -= 1;
                soulScore.HellCoal += 1;
                Debug.Log("Lantern loaded");
            }

            //sign SALVATION POLICY
            if (activePolicy.PolicyId == 2 && activePolicy.isSigned)
            {
                int x = (Random.Range(1, 8));
                Debug.Log(x);
                if (x >= 3)
                {
                    unit.SpiritNumber += 5;
                }
                Debug.Log("Salvation loaded");
            }

            //sign BLESSING POLICY
            if (unit.ifOnLivingLevel && activePolicy.PolicyId == 3 && activePolicy.isSigned)
            {
                soulScore.HellCoin += 1;

                Debug.Log("Blessing loaded+1");
                PolicyMassage.SetActive(true);
            }


            //Sign ENERGETIC POLICY
            if (activePolicy.PolicyId == 4 && activePolicy.isSigned)

            {
                unit.SpiritNumber -= 1;
                unit.FatigueNumber += 1;
                Debug.Log("Energetic loaded");
            }

            //sign LINGERING POLICY
            if (activePolicy.PolicyId == 5 && activePolicy.isSigned)
            {
                unit.SpiritNumber = 1;
                unit.FatigueNumber = 4;
                Debug.Log("Lingering loaded");
            }

            //sing WORKHOLIC POLICY
            if (unit.ifOnLivingLevel && activePolicy.PolicyId == 6 && activePolicy.isSigned)
            {
                soulScore.HellWater += 1;
                Debug.Log("Workholic loaded");
            }

            //sing PARDON POLICY
            if (unit.ifOnLivingLevel && activePolicy.PolicyId == 7 && activePolicy.isSigned)
            {
                soulScore.HellLight += 1;
                Debug.Log("Pardon loaded");
                PolicyMassage.SetActive(true);
            }
            
        }
        activePolicy.isSigned = false;
        activePolicy.GetComponent<Button>().interactable = false;
    }


}
