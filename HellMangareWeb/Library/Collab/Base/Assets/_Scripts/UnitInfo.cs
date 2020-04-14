using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class UnitInfo : MonoBehaviour
{
    Renderer renderer;
    SoulMoveManager soulMoveManager;
    SpriteRenderer spriteRenderer;
    SoulScoreManager soulScoreManager;

    UIManager uiManager;

    public BaseSoulClasses BaseSoul;
    public string SoulName;
    public string SoulReason;
    public int SoulAge;
    public string SoulStory;

    public float SpiritNumber;
    public float MemoryNumber;
    public int FatigueNumber;

    public bool Convicted = false;
    public int PretenseYear;
    public int SlothYear;
    public int StealYear;
    public int MurderYear;
    public int BetrayelYear;
    public int GreedYear;
    public int LustYear;
    public int AllYear;

    public Canvas UnitInfoCanvas;
    public Canvas UnitWholeInfo;
    public TextMeshProUGUI Name;
    
    public Image Spirit;
    public Image Memory;
    public float TextfadeTime;

    public Color unselectedColor;
    public Color selectedColor;
    public Color hoverColor;
    public Vector3 targetDestination;
    public Vector3 destAtOurHeight;
    public bool selected = false;
    public bool hover = false;
    public bool justSelected = false;
    public RaycastHit hit;
    public NavMeshAgent UnitAgent;
    public LayerMask layerMask;
    public int Level;

    float DoubleClickTime = 0;
    public bool TransLevel = false;
    public bool ChooseLevel = false;

    
    public GameObject RelaxPanel;
    public GameObject PunishPanel;

    public GameObject RebornButton;
    public GameObject DeathButton;

    Rigidbody rb;
    public GameObject LivingLevelManager;
    public GameObject VisitingLevelManager;
    public GameObject SolitudeLevelManager;
    public GameObject FireLevelManager;
    public GameObject FrozenLevelManager;
    public GameObject ToilLevelManager;
    public GameObject AvariceLevelManager;
    public GameObject RebirthManager;
    public GameObject DeathManager;
    
    public LivingSoulManager livingSoulManager;
    public VisitingSoulManager visitingSoulManager;
    public SolitudeSoulManager solitudeSoulManager;
    public FireSoulManager fireSoulManager;
    public FrozenSoulManager frozenSoulManager;
    public ToilSoulManager toilSoulManager;
    public AvariceSoulManager avariceSoulManager;
    public RebirthSoulManager rebirthSoulManager;
    public DeathSoulManager deathSoulManager;

    public bool ifOnLivingLevel= false;
    public bool ifOnFireLevel = false;
    public bool ifOnFrozenLevel = false;
    public bool ifOnVisitingLevel = false;
    public bool ifOnSolitudeLevel = false;
    public bool ifOnToilLevel = false;
    public bool ifOnAvariceLevel = false;
    public bool ifOnRebirthLevel = false;
    public bool ifOnDeathLevel = false;

    void Start()
    {
        Spirit = Spirit.GetComponent<Image>();
        Memory = Memory.GetComponent<Image>();
        Name = Name.GetComponent<TextMeshProUGUI>();
        Name.color = Color.white; 

        renderer = gameObject.GetComponentInChildren<Renderer>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        GameObject soulManagerObj = GameObject.Find("SoulMoveManager");
        soulMoveManager = soulManagerObj.GetComponent<SoulMoveManager>();
        UnitAgent.enabled = false;
        UnitInfoCanvasFade();
        DoubleClickTime = Time.time;

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;

        RebornButton.gameObject.SetActive(false);
        DeathButton.gameObject.SetActive(false);

        GameObject UIObject = GameObject.Find("UIManager");
        uiManager = UIObject.GetComponent<UIManager>();

        GameObject ScoreObject = GameObject.Find("SoulScoreManager");
        soulScoreManager = ScoreObject.GetComponent<SoulScoreManager>();
        CheckStatus();

        SoulName = BaseSoul.SoulName;
        SoulAge = BaseSoul.SoulAge;
        SoulReason = BaseSoul.SoulReason;
        SoulStory = BaseSoul.SoulStory;
        MemoryNumber = BaseSoul.MemoryNumber;
        SpiritNumber = BaseSoul.SpiritNumber;
        FatigueNumber = BaseSoul.FatigueNumber;
    }

    /*internal void SoulName(SoulName soulName)
    {
        throw new NotImplementedException();
    }*/

    // Update is called once per frame
    void Update()
    {

        UnitMove();
        CheckStatus();
    }
    void OnMouseOver()
    {
        hover = true;
        UnitInfoCanvasFade();
    }
    void OnMouseExit()
    {
        hover = false;
        UnitInfoCanvasFade();
    }
    public void OnMouseDown()
    {
        selected = true;
        justSelected = true;
        UnitInfoCanvasFade();
        if (Time.time - DoubleClickTime <= 0.3f)
        {
            Debug.Log("double click");
            ChooseLevel = true;
            UISoul();
            soulScoreManager.Conviction();
           
            UnitWholeInfo.gameObject.SetActive(true);
        }
        DoubleClickTime = Time.time;
    }
    
    public void UnitInfoCanvasFade()
    {
        if (selected == true)
        {
            renderer.material.color = selectedColor;
        }
        else
        {
            if (hover == true)
            {
                UnitInfoCanvas.gameObject.SetActive(true);
                CheckStatus();
                renderer.material.color = hoverColor;
            }
            else
            {
                UnitInfoCanvas.gameObject.SetActive(false);
                renderer.material.color = unselectedColor;
            }
        }
    }
    public void CheckStatus()
    {
        Name.text = BaseSoul.SoulName;
        Spirit.fillAmount = BaseSoul.SpiritNumber*10 / 100f;
        Memory.fillAmount = BaseSoul.MemoryNumber*10 / 100f;
        if (FatigueNumber < 2)
        {
            spriteRenderer.color = new Color(255, 255, 255, 40);
        }
        else
        {
            float newAlpha = FatigueNumber * 25.5f / 255f;
            spriteRenderer.color = new Color(255, 255, 255, newAlpha);
        }
    }
    public void UnitMove()
    {
        if (selected && !justSelected)
        {           
            UnitAgent.enabled = true;
          
            if (Input.GetMouseButtonDown(0))
            {

                if (EventSystem.current.IsPointerOverGameObject() == false)                    
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, layerMask))
                    {
                        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("MoveUnit"))   
                        {
                            targetDestination = hit.point;
                        }
                    }
                    else
                    {                        
                        soulMoveManager.selectUnit(null);
                    }
                }
            }            
        }
        justSelected = false; 
        //movement
        float distanceToTarget = Vector3.Distance(transform.position, targetDestination);
        if (distanceToTarget > 1)
        {
            UnitAgent.enabled = true;
            Vector3 destAtOurHeight = new Vector3(targetDestination.x, targetDestination.y + 1f, targetDestination.z);
            UnitAgent.SetDestination(destAtOurHeight);
            UnitAgent.updateRotation = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            selected = false;
            UnitInfoCanvasFade();
            ChooseLevel = false;
            UnitWholeInfo.gameObject.SetActive(false);
        }
    }
    //initial SoulNumber and AllMemory/AllSpirit 
    public void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "LivingLevel")
        {
            livingSoulManager.updateLivingSoulin(1);
            Debug.Log("+Living");
            ifOnLivingLevel = true;
            rb.isKinematic = true;

            soulScoreManager.AllBaseMemory += BaseSoul.MemoryNumber;
            uiManager.AllMemory.text = "AllSoulMemory: " + soulScoreManager.AllBaseMemory.ToString();
            Debug.Log(soulScoreManager.AllBaseMemory);
            soulScoreManager.AllBaseSpirit += BaseSoul.SpiritNumber;
            uiManager.AllSpirit.text = "AllSoulSpirit: " + soulScoreManager.AllBaseSpirit.ToString();
            soulScoreManager.SkillPower();
            soulScoreManager.LightInHouse();


        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "door" && TransLevel== true )
        {
            
            switch (Level)
            {
                case 1:
                    UnitAgent.enabled = false;
                    transform.position = LivingLevelManager.transform.position;
                    RelaxPanel.gameObject.SetActive(false);
                    TransLevel = false;
                    Debug.Log("Let's go to the living level");
                    //LivingSoulNum +1
                    livingSoulManager.updateLivingSoulin(1);
                    Debug.Log("checkedinLiving");
                    ifOnLivingLevel = true;
                    break;

                case 2:
                    UnitAgent.enabled = false;
                    transform.position = FireLevelManager.transform.position;
                    Debug.Log("GoFire =" + transform.position);
                    PunishPanel.gameObject.SetActive(false);
                    TransLevel = false;
                    Debug.Log("Let's go to the fire level");
                    //FireSoulNum +1
                    fireSoulManager.updateFireSoulin(1);
                    ifOnFireLevel = true;
                    Debug.Log("checkedinFire");
                    break;

                case 3:
                    UnitAgent.enabled = false;
                    transform.position = FrozenLevelManager.transform.position;
                    PunishPanel.gameObject.SetActive(false);
                    TransLevel = false;
                    Debug.Log("Let's go to the frozen level");
                    //FrozenSoulNum +1
                    frozenSoulManager.updateFrozenSoulin(1);
                    ifOnFrozenLevel = true;
                    Debug.Log("checkedinFrozen");
                    break;

                case 4:
                    UnitAgent.enabled = false;
                    transform.position = ToilLevelManager.transform.position;
                    PunishPanel.gameObject.SetActive(false);
                    TransLevel = false;
                    Debug.Log("Let's go to the Toil level");
                    //ToilSoulNum +1
                    toilSoulManager.updateToilSoulin(1);
                    ifOnToilLevel = true;
                    Debug.Log("checkedinToil");
                    break;

                case 5:
                    UnitAgent.enabled = false;
                    transform.position = AvariceLevelManager.transform.position;
                    PunishPanel.gameObject.SetActive(false);
                    TransLevel = false;
                    Debug.Log("Let's go to the Avarice level");
                    //AvariceSoulNum +1
                    avariceSoulManager.updateAvariceSoulin(1);
                    ifOnAvariceLevel = true;
                    Debug.Log("checkedinAvarice");
                    break;

                case 6:
                    UnitAgent.enabled = false;
                    transform.position = SolitudeLevelManager.transform.position;
                    PunishPanel.gameObject.SetActive(false);
                    TransLevel = false;
                    Debug.Log("Let's go to the Solitude level");
                    //SolitudeSoulNum +1
                    solitudeSoulManager.updateSolitudeSoulin(1);
                    ifOnSolitudeLevel = true;
                    Debug.Log("checkedinSolitude");
                    break;

                case 7:
                    UnitAgent.enabled = false;
                    transform.position = VisitingLevelManager.transform.position;
                    RelaxPanel.gameObject.SetActive(false);
                    TransLevel = false;
                    Debug.Log("Let's go to the Visiting level");
                    //VisitingSoulNum +1
                    visitingSoulManager.updateVisitingSoulin(1);
                    ifOnVisitingLevel = true;
                    Debug.Log("checkedinVisiting");
                    break;

                case 8:
                    UnitAgent.enabled = false;
                    transform.position = RebirthManager.transform.position;
                    RebornButton.gameObject.SetActive(false);
                    TransLevel = false;
                    Debug.Log("Let's go to the Rebirth level");
                    //RebirthSoulNum +1
                    rebirthSoulManager.updateRebirthSoulin(1);
                    ifOnRebirthLevel = true;
                    Debug.Log("checkedinRebirth");
                    break;

                case 9:
                    UnitAgent.enabled = false;
                    transform.position = DeathManager.transform.position;
                    DeathButton.gameObject.SetActive(false);
                    TransLevel = false;
                    Debug.Log("Let's go to the Death level"); 
                    //DeathSoulNum +1
                    deathSoulManager.updateDeathSoulin(1);
                    ifOnDeathLevel = true;
                    Debug.Log("checkedinDeath" );
                    break;
            }

        }

    }
    
    public void UISoul()
    {
        uiManager.ProfileName.text = "Name:" + SoulName;
        uiManager.ProfileAge.text = "Age:" + SoulAge;
        uiManager.Reason.text = "Reason of Death:" + SoulReason;
        uiManager.LifeStory.text = "LifeStory: " + SoulStory;
        uiManager.ProfileMemory.text = "Memory:" + MemoryNumber;
        uiManager.ProfileSpirit.text = "Spirit:" + SpiritNumber;
        uiManager.ProfileFatigue.text = "Fatigue:" + FatigueNumber;
    }

}