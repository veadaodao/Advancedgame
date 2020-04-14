using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PolicyTree : MonoBehaviour
{

    //Renderer renderer1;
    public bool selected = false;
    bool justSelected = false;
    //public RaycastHit hit;
    //public LayerMask layerMask;

    public GameObject PolicyPanel;
    public GameObject HouseLight;
    public GameObject PolicyMassage;
    public GameObject PolicyTree1;
    public GameObject PolicyTree2;

    public GameObject P1Button;
    public GameObject P2Button;
    public GameObject SignFaithButton;
    public GameObject SignLanternButton;
    public GameObject SignEnergeticButton;
    public GameObject SignSalvationButton;
    public GameObject SignLingeringButton;
    public GameObject SignBlessingButton;

    SoulScoreManager soulScore;
    UIManager uIManager;

    float x;
    int policy;
    bool ifPolicy1 = false;
    bool ifPolicy2 = false;

    public bool ifLightOn = false;
    public bool ifFaith = false;
    public bool ifLantern = false;
    public bool ifEnergetic = false;
    public bool ifSalvation = false;
    public bool ifLingering = false;
    public bool ifBlessing = false;



    // Start is called before the first frame update
    void Start()
    {
        GameObject UIObject = GameObject.Find("UIManager");
        uIManager = UIObject.GetComponent<UIManager>();
        //renderer1 = gameObject.GetComponent<Renderer>();


        GameObject ScoreObject = GameObject.Find("SoulScoreManager");
        soulScore = ScoreObject.GetComponent<SoulScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MouseSelect();

        if (soulScore.HellLight - x == 3)
        {
            HouseLight.SetActive(true);
        }
        else
        {
            HouseLight.SetActive(false);
        }

    }

    public void OnMouseDown()
    {
        if (HouseLight.activeInHierarchy == true && (PolicyPanel.activeInHierarchy == false))
        {
            selected = true;
            justSelected = true;

            ChoosePolicy();
        }
        if (HouseLight.activeInHierarchy == false && PolicyPanel.activeInHierarchy == false && soulScore.HellLight >= 3)
        {
            PolicyPanel.SetActive(true);
        }

    }

    private void MouseSelect()
    {   
        if (Input.GetMouseButtonDown(1))
        {
            
            PolicyPanel.SetActive(false);
            if(HouseLight.activeInHierarchy == true)
            {
                HouseLight.SetActive(false);
            }

        }
    }

    public void ChoosePolicy()
    {
        PolicyPanel.SetActive(true);

        if(ifPolicy1 == false && ifPolicy2 == false)
        {
            P1Button.SetActive(true);
            P2Button.SetActive(true);
            switch (policy)
            {
                case 1:

                    Policy1();
                    break;
                case 2:
                    Policy2();
                    break;
            }
        }
        if (ifPolicy1 == true && ifPolicy2 == false)
        {
            Policy1();
        }

        if (ifPolicy2 == true && ifPolicy1 == false)
        {
            Policy2();
        }


    }

    public void Policy1()
    {
        Debug.Log("x2=" + x);
        ifPolicy1 = true;
        uIManager.PolicyMassage.text = "SIGN ONE POLICY".ToString();
        if (x ==0)
        {
            P1Button.SetActive(false);
            P2Button.SetActive(false);
            uIManager.PolicyTree1.text = "FAITH\r\n\r\n\r\nLANTERN    ENERGETIC\r\n\r\n\r\nSALVATION    LINGERING\r\n\r\n\r\nBLESSING".ToString();
            uIManager.PolicyTree2.text = "FAITH\r\n\r\nSOULS ACQUIRE MEMORY DUBLE IN THE VISITING LEVEL BUT WILL LOSS 1 SPIRIT / HELLYEAR".ToString();
            SignFaithButton.SetActive(true);
            x += 3;
        }

        
        if (x >= 15 && x < 30)
        {
            Debug.Log("x3=" + x);
            SignFaithButton.SetActive(false);
            uIManager.PolicyMassage.text = "POLICY I -> FAITH".ToString();
            uIManager.PolicyTree2.text = "\r\n\r\n<size=18>LANTERN\r\nALL SOULS CAN SACRIFICE THIRE 1 MEMORY TO ACQUIRE 5 LIGHT.\r\n\r\nENERGETIC\r\nALL SOULS HAVE MORE FAITH, THEY SACRIFICE THEIR 1 SPIRIT TO ACQUIRE 1 FATIGUE</size>".ToString();
            SignLanternButton.SetActive(true);
            SignEnergeticButton.SetActive(true);
            x += 3;
    

        }

        if (x >= 40 && x <= 60)
        {
       
            Debug.Log("x4=" + x);
            if (ifLantern == true && ifEnergetic == false)
            {
                uIManager.PolicyMassage.text = "POLICY I -> FAITH -> LANTERN".ToString();
                uIManager.PolicyTree2.text = "SALVATION: ALL THE SOULS WILL INCREASE THEIR CHANCES TO ACQUIRE DECENT REINCARNATION".ToString();
                SignSalvationButton.SetActive(true);

            }
            if (ifLantern == false && ifEnergetic == true)
            {
                uIManager.PolicyMassage.text = "POLICY I -> FAITH -> ENERGETIC".ToString();
                uIManager.PolicyTree2.text = "LINGERING: ALL THE SOULS KEEP THEIR SPIRIT = 1, FATIGUE = 4".ToString();
                SignLingeringButton.SetActive(true);

            }
            x += 3;

        }

        if (x>= 800 && x <= 100)
        {
            Debug.Log("x5=" + x);

            uIManager.PolicyMassage.text = "POLICY I -> FAITH -> LANTERN -> SAVATION/LINGERING".ToString();
            uIManager.PolicyTree2.text = "BLESSING: redUCING 5 BUILDING UP FEE FOR ALL THE HELL IN LEVEL 3".ToString();
            SignBlessingButton.SetActive(true);
            x += 3;

        }
   


    }

    public void Policy2()
    {
        ifPolicy2 = true;
        /*P1Button.SetActive(false);
        P2Button.SetActive(false);
        uIManager.PolicyTree1.text = "WORKAHOLIC\r\n\r\n\r\nYOUNG LABORS\r\n\r\n\r\nEQUAL\r\n\r\n\r\nWORKSHOP\r\n\r\n\r\nSLAVE".ToString();
        uIManager.PolicyTree2.text = "WORKAHOLIC: SOULS' WORKING CONSUMPTION redUCE 1".ToString();

        x = 2 * x;

        if (soulScore.PlayerSkillPower >= 100 && soulScore.PlayerSkillPower <= 200)
        {
            soulScore.LightInHouse();
            uIManager.PolicyMassage.text = "POLICY II -> WORKAHOLIC".ToString();
            uIManager.PolicyTree2.text = "YOUNG LABORS: ALL YOUND SOULS HAVE TO WORK NO MATTER THEIR AGE".ToString();
            SignYoung();

        }

        if (soulScore.PlayerSkillPower >= 200 && soulScore.PlayerSkillPower <= 300)
        {
            soulScore.LightInHouse();
            uIManager.PolicyMassage.text = "POLICY II -> WORKAHOLIC -> YOUNG LABORS".ToString();
            uIManager.PolicyTree2.text = "EQUAL: ALL WOULS HAVE TO WORK NO MATTER WHO HAVE SINS OR NOT".ToString();
            SignEqual();


        }

        if (soulScore.PlayerSkillPower >= 300 && soulScore.PlayerSkillPower <= 400)

        {
            soulScore.LightInHouse();
            uIManager.PolicyMassage.text = "POLICY II -> WORKAHOLIC -> YOUNG LABORS -> EQUAL".ToString();
            uIManager.PolicyTree2.text = "WORKSHOP: IF SOULS OCCUPY FULL OF ONE LEVEL OF HELL, SOULS IN THIS HELL PRODUCE DOUBLE RESOURCES".ToString();
            SignWorkshop();
        }

        if (soulScore.PlayerSkillPower >= 400 && soulScore.PlayerSkillPower <= 500)
        {
            soulScore.LightInHouse();
            uIManager.PolicyMassage.text = "POLICY II -> WORKAHOLIC -> YOUNG LABORS -> EQUAL -> WORKSHOP".ToString();
            uIManager.PolicyTree2.text = "SLAVE: IF SOULS DO WORKING UNTIL THEY CONSUME ALL THE SPIRIT AND FATUGUE, THEY WILL NOT DIE OR ACQUIRE REBIRTH".ToString();
            SignSlave();

        }*/

    }

    public void SignFaith()
    {
        ifFaith = true;
        SignFaithButton.SetActive(false);
        uIManager.PolicyTree1.text = "<color=red>FAITH</color>\r\n\r\n\r\nLANTERN    ENERGETIC\r\n\r\n\r\nSALVATION    LINGERING\r\n\r\n\r\nBLESSING".ToString();
        Debug.Log("ifFaith =" + ifFaith);

    }

    public void SignLantern()
    {
        
        ifLantern = true;
        SignLanternButton.SetActive(false);
        SignEnergeticButton.SetActive(false);
        uIManager.PolicyTree1.text = "<color=red>FAITH\r\n\r\n\r\nLANTERN</color>    ENERGETIC\r\n\r\n\r\nSALVATION    LINGERING\r\n\r\n\r\nBLESSING".ToString();
        uIManager.PolicyTree2.text = "\r\n\r\n<size=18>LANTERN\r\nALL SOULS CAN SACRIFICE THIRE 1 MEMORY TO ACQUIRE 5 LIGHT.\r\n\r\n</size>".ToString();

        
    }
    public void SignEnergetic()
    {
        
        ifEnergetic = true;
        SignLanternButton.SetActive(false);
        SignEnergeticButton.SetActive(false);
        uIManager.PolicyTree1.text = "<color=red>FAITH</color>\r\n\r\n\r\nLANTERN    <color=red>ENERGETIC</color>\r\n\r\n\r\nSALVATION    LINGERING\r\n\r\n\r\nBLESSING".ToString();
        uIManager.PolicyTree2.text = "\r\n\r\n<size=18>ENERGETIC\r\nALL SOULS HAVE MORE FAITH, THEY SACRIFICE THEIR 1 SPIRIT TO ACQUIRE 1 FATIGUE</size>".ToString();

        
    }
    public void SignSalvation()
    {

        
        ifSalvation = true;
        SignSalvationButton.SetActive(false);
        uIManager.PolicyTree1.text = "<color=red>FAITH\r\n\r\n\r\nLANTERN</color>    ENERGETIC\r\n\r\n\r\n<color=red>SALVATION</color>    LINGERING\r\n\r\n\r\nBLESSING".ToString();

        
    }
    public void SignLingering()
    {
        ifLingering = true;
        SignLingeringButton.SetActive(false);
        uIManager.PolicyTree1.text = "<color=red>FAITH</color>\r\n\r\n\r\nLANTERN    <color=red>ENERGETIC</color>\r\n\r\n\r\nSALVATION    <color=red>LINGERING</color>\r\n\r\n\r\nBLESSING".ToString();

        
    }
    public void SignBlessing()
    {

        
        ifBlessing = true;
        SignBlessingButton.SetActive(false);
        if (ifLingering == true && ifSalvation == false)
        {
            uIManager.PolicyTree1.text = "<color=red>FAITH</color>\r\n\r\n\r\nLANTERN    <color=red>ENERGETIC</color>\r\n\r\n\r\nSALVATION    <color=red>LINGERING</color>\r\n\r\n\r\n<color = red>BLESSING</color>".ToString();
        }
        if (ifLingering == false && ifSalvation == true)
        {
            uIManager.PolicyTree1.text = "<color=red>FAITH\r\n\r\n\r\nLANTERN</color>    ENERGETIC\r\n\r\n\r\n<color=red>SALVATION</color>    LINGERING\r\n\r\n\r\n<color=red>BLESSING</color>".ToString();
        }

        
    }


}

