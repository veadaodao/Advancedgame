using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
using UnityEngine.EventSystems;

public class UnitInfoTutorial : MonoBehaviour
{
    Renderer renderer;
    SoulTutorialMoveManager soulTutorialMoveManager;
    public SpriteRenderer spriteRenderer;
    public Vector3 startPosition;
    
    HellScoreTutorial hellScore;
    UIManager uiManager;

    public BaseTutorialSoulClasses BaseSoul;
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

    public Canvas UnitQuickCanvas;
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
    public bool TutorialLevel = true;
    public RaycastHit hit;
    public NavMeshAgent UnitAgent;
    public LayerMask layerMask;

    float DoubleClickTime = 0;
    public bool TransLevel = false;
    public bool ChooseLevel = false;
    public bool FinishFire = false;

    public GameObject RebornButton;
    public GameObject DeathButton;
    public GameObject PunishButton;
    public GameObject RelaxButton;
    public GameObject Visiting;

    Rigidbody rb;
    public bool ifOnToilLevel = false;
    public bool ifOnAvariceLevel = false;
    public LivingSoulManager livingSoulManager;
    public TMP_Text RemindText;
    public Transform PunishPanelManager;

    public GameObject SaveButton;
    public GameObject ChoosePanel;
    public GameObject Judge;

    public PretenseSlider pretenseSlider;
    public SlothSlider slothSlider;
    public StealSlider stealSlider;
    public GreedSlider greedSlider;
    public MurderSlider murderSlider;
    public BetrayelSlider betrayelSlider;
    public LustSlider lustSlider;

    void Start()
    {
        Spirit = Spirit.GetComponent<Image>();
        Memory = Memory.GetComponent<Image>();
        Name = Name.GetComponent<TextMeshProUGUI>();
        Name.color = Color.white;

        renderer = gameObject.GetComponentInChildren<Renderer>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        GameObject soulManagerObj = GameObject.Find("SoulTutorialMoveManager");
        soulTutorialMoveManager = soulManagerObj.GetComponent<SoulTutorialMoveManager>();
        UnitAgent.enabled = false;
        UnitInfoCanvasFade();
        DoubleClickTime = Time.time;

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;

        RebornButton.gameObject.SetActive(true);
        DeathButton.gameObject.SetActive(false);
        PunishButton.gameObject.SetActive(true);
        RelaxButton.gameObject.SetActive(true);
        Visiting.gameObject.SetActive(true);


       GameObject UIObject = GameObject.Find("UIManager");
        uiManager = UIObject.GetComponent<UIManager>();
        GameObject HellObject = GameObject.Find("HellScoreManager");
        hellScore = HellObject.GetComponent<HellScoreTutorial>();

        CheckStatus();

        SoulName = BaseSoul.SoulName;
        SoulAge = BaseSoul.SoulAge;
        SoulReason = BaseSoul.SoulReason;
        SoulStory = BaseSoul.SoulStory;
        MemoryNumber = BaseSoul.MemoryNumber;
        SpiritNumber = BaseSoul.SpiritNumber;
        FatigueNumber = BaseSoul.FatigueNumber;

        startPosition = transform.position;

    }
    void Update()
    {

        UnitMove();
        CheckStatus();
    }
    void OnMouseOver()
    {
        hover = true;
        UnitInfoCanvasFade();
        CheckConvicted();
    }
    void OnMouseExit()
    {
        hover = false;
        UnitInfoCanvasFade();
        RemindText.text = "".ToString();
    }
    public void OnMouseDown()
    {
        FindObjectOfType<AudioManager>().Play("click");
        selected = true;
        justSelected = true;
        UnitInfoCanvasFade();
        if (Time.time - DoubleClickTime <= 0.3f)
        {
            ChooseLevel = true;
            UISoul();
     //Show sliders When Double click soul to open the profile panel
            if (!Convicted)
            {
                ChoosePanel.SetActive(true);
                pretenseSlider.PretenseYear = 0;
                pretenseSlider.slider.value = 0;
                slothSlider.SlothYear = 0;
                slothSlider.slider.value = 0;
                stealSlider.StealYear = 0;
                stealSlider.slider.value = 0;
                greedSlider.GreedYear = 0;
                greedSlider.slider.value = 0;
                murderSlider.MurderYear = 0;
                murderSlider.slider.value = 0;
                betrayelSlider.BetrayelYear = 0;
                betrayelSlider.slider.value = 0;
                lustSlider.LustYear = 0;
                lustSlider.slider.value = 0;
                AllYear = 0;

                uiManager.Pretense.text = "" + pretenseSlider.PretenseYear;
                uiManager.Sloth.text = "" + slothSlider.SlothYear;
                uiManager.Steal.text = "" + stealSlider.StealYear;
                uiManager.Greed.text = "" + greedSlider.GreedYear;
                uiManager.Murder.text = "" + murderSlider.MurderYear;
                uiManager.Betrayel.text = "" + betrayelSlider.BetrayelYear;
                uiManager.Lust.text = "" + lustSlider.LustYear;
                uiManager.AllSinYear.text = "" + AllYear;
            }
            else
            {
                ChoosePanel.SetActive(false);
                uiManager.Pretense.text = "" + PretenseYear;
                uiManager.Sloth.text = "" + SlothYear;
                uiManager.Steal.text = "" + StealYear;
                uiManager.Greed.text = "" + GreedYear;
                uiManager.Murder.text = "" + MurderYear;
                uiManager.Betrayel.text = "" + BetrayelYear;
                uiManager.Lust.text = "" + LustYear;
                uiManager.AllSinYear.text = "" + AllYear;
            }

            UnitWholeInfo.gameObject.SetActive(true);
            if (Convicted && (MemoryNumber == 0 && AllYear == 0 && SpiritNumber > 0))
            {
                RebornButton.gameObject.SetActive(true);
            }
            else if (Convicted && (MemoryNumber == 0 && AllYear == 0 && SpiritNumber > 0))
            {
                RebornButton.gameObject.SetActive(true);
            }
            if (Convicted && (SpiritNumber == 0))
            {
                DeathButton.gameObject.SetActive(true);
            }
        }
        DoubleClickTime = Time.time;
    }

    //Press save button to caculate allSinYears and save it. Check the rationality of judgement.
    public void SinYear()
    {
        if (ChooseLevel)
        {

            if (!Convicted && ChoosePanel.activeInHierarchy)
            {
                FindObjectOfType<AudioManager>().Play("click");
                PretenseYear = pretenseSlider.PretenseYear;
                SlothYear = slothSlider.SlothYear;
                StealYear = stealSlider.StealYear;
                GreedYear = greedSlider.GreedYear;
                MurderYear = murderSlider.MurderYear;
                BetrayelYear = betrayelSlider.BetrayelYear;
                LustYear = lustSlider.LustYear;
                AllYear = PretenseYear + SlothYear + StealYear + MurderYear + BetrayelYear + GreedYear + LustYear;

                uiManager.Pretense.text = "" + PretenseYear;
                uiManager.Sloth.text = "" + SlothYear;
                uiManager.Steal.text = "" + StealYear;
                uiManager.Greed.text = "" + GreedYear;
                uiManager.Murder.text = "" + MurderYear;
                uiManager.Betrayel.text = "" + BetrayelYear;
                uiManager.Lust.text = "" + LustYear;
                uiManager.AllSinYear.text = "" + AllYear;

                if (AllYear <= 15 && AllYear >= 3)
                {
                    Judge.SetActive(false);
                    ChoosePanel.gameObject.SetActive(false);
                    Convicted = true;

                }
                else if (AllYear > 15)
                {
                    Judge.SetActive(true);
                    uiManager.JudgeMessage.text = "Your judgement might be too heavy!".ToString();
                    hellScore.HellCoin -= 1;
                }
                else if (AllYear < 3)
                {
                    Judge.SetActive(true);
                    uiManager.JudgeMessage.text = "Your judgement might be too light!".ToString();
                    hellScore.HellCoin -= 1;
                }
            }
        }
    }

    public void CheckConvicted()
    {
        if (!Convicted)
        {
            RemindText.text = "Need to Judge".ToString();
        }
        else
        {
            RemindText.text = "".ToString();
        }

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
                UnitQuickCanvas.gameObject.SetActive(true);
                CheckStatus();
                renderer.material.color = hoverColor;
            }
            else
            {
                UnitQuickCanvas.gameObject.SetActive(false);
                renderer.material.color = unselectedColor;
            }
        }
    }
    public void CheckStatus()
    {
        Name.text = BaseSoul.SoulName;
        Spirit.fillAmount = BaseSoul.SpiritNumber * 10 / 100f;
        Memory.fillAmount = BaseSoul.MemoryNumber * 10 / 100f;
        if (FatigueNumber <= 1)
        {
            spriteRenderer.color = new Color(255, 255, 255, 40);
        }
        else
        {
            float newAlpha = FatigueNumber * 25.5f / 255f;
            spriteRenderer.color = new Color(255, 255, 255, newAlpha);
        }
        if (MemoryNumber > 10)
        {
            MemoryNumber = 10;
        }
    }
    public void UnitMove()
    {
        if (selected && !justSelected)
        {
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
                        soulTutorialMoveManager.selectUnit(null);
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
    //initial SoulNumber
    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "LivingLevel")
        {
            livingSoulManager.updateLivingSoulin(1);
            rb.isKinematic = true;
        }
    }

    
    public void UISoul()
    {
        uiManager.ProfileName.text = "Name:" + SoulName;
        uiManager.ProfileAge.text = "Age:" + SoulAge;
        uiManager.Reason.text = "Reason of Death:\r\n" + SoulReason;
        uiManager.LifeStory.text = "LifeStory:\r\n\r\n" + SoulStory;
        uiManager.ProfileMemory.text = "Memory:" + MemoryNumber;
        uiManager.ProfileSpirit.text = "Spirit:" + SpiritNumber;
        uiManager.ProfileFatigue.text = "Fatigue:" + FatigueNumber;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "FirstLevelManager")
        {
            UnitAgent.enabled = false;
            transform.position = PunishPanelManager.transform.position;
        }
    }

    public void GoToil()
    {
        if (!ifOnToilLevel)
        {
            FindObjectOfType<AudioManager>().Play("click");
            ifOnToilLevel = true;
        }
    }
    public void GoAvarice()
    {
        if (!ifOnAvariceLevel)
        {
            FindObjectOfType<AudioManager>().Play("click");
            ifOnAvariceLevel = true;
        }
    }

}
