using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SoulScoreManager : MonoBehaviour
{
    public int HellHeat = 3;
    public int HellCool = 3;
    public int HellSoul = 0;
    public int HellYear = 0;

    public int HellCoal = 0;
    public int HellCoin = 3;
    public int HellLight = 0;
    public int HellWater = 0;
    public int HellFlower = 0;

    public Canvas UnitWholeInfo;
    UIManager uiManager;
    RealTime realTime;

    LivingSoulManager livingsoul;
    VisitingSoulManager visitingsoul;
    FireSoulManager firesoul;
    FrozenSoulManager frozensoul;
    ToilSoulManager toilsoul;
    AvariceSoulManager avaricesoul;
    RebirthSoulManager rebirthsoul;
    DeathSoulManager deathsoul;

    public bool ProduceLight = false;

    public GameObject SaveButton;
    public GameObject ChoosePanel;
    public GameObject End;
    public GameObject Judge;
    public GameObject DeathPanel;
    public GameObject RebirthPanel;
    public GameObject ToilLevelButton;
    public GameObject AvariceLevelButton;

    //int LampOnIndex;
    //List<int> lights = new List<int>();
    //public GameObject[] Lights;

    public PretenseSlider pretenseSlider;
    public SlothSlider slothSlider;
    public StealSlider stealSlider;
    public GreedSlider greedSlider;
    public MurderSlider murderSlider;
    public BetrayelSlider betrayelSlider;
    public LustSlider lustSlider;

    int Policyitem;
    

    public Image HeatBar;
    public Image CoolBar;

    void Start()
    {
        GameObject Living = GameObject.Find("LivingSoulManager");
        livingsoul = Living.GetComponent<LivingSoulManager>();
        GameObject Visiting = GameObject.Find("VisitingSoulManager");
        visitingsoul = Visiting.GetComponent<VisitingSoulManager>();
        GameObject Fire = GameObject.Find("FireSoulManager");
        firesoul = Fire.GetComponent<FireSoulManager>();
        GameObject Frozen = GameObject.Find("FrozenSoulManager");
        frozensoul = Frozen.GetComponent<FrozenSoulManager>();
        GameObject Toil = GameObject.Find("ToilSoulManager");
        toilsoul = Toil.GetComponent<ToilSoulManager>();
        GameObject Avarice = GameObject.Find("AvariceSoulManager");
        avaricesoul = Avarice.GetComponent<AvariceSoulManager>();
        GameObject Rebirth = GameObject.Find("RebirthSoulManager");
        rebirthsoul = Rebirth.GetComponent<RebirthSoulManager>();
        GameObject Death = GameObject.Find("DeathSoulManager");
        deathsoul = Death.GetComponent<DeathSoulManager>();

        GameObject UIObject = GameObject.Find("UIManager");
        uiManager = UIObject.GetComponent<UIManager>();
        
        GameObject TimeObject = GameObject.Find("RealTimeManager");
        realTime = TimeObject.GetComponent<RealTime>();


        /*for (int i = 0; i < Lights.Length; ++i)
        {
            lights.Add(i);
        }*/

        End.SetActive(false);
        Judge.SetActive(false);

        HeatBar = HeatBar.GetComponent<Image>();
        CoolBar = CoolBar.GetComponent<Image>();

        HeatCoolBar();
        UIScore();
    }

    public void Update()
    {
        //Caculate Hellsoul
        HellSoul = livingsoul.LivingSoul + visitingsoul.VisitingSoul + /*solitudesoul.SolitudeSoul */+ firesoul.FireSoul + frozensoul.FrozenSoul + toilsoul.ToilSoul + avaricesoul.AvariceSoul + rebirthsoul.RebirthSoul + deathsoul.DeathSoul;
        uiManager.Hellsoul.text = "Hellsoul:" + HellSoul.ToString();
        UIScore();


        //Unlock levels
        if (HellSoul >= 5)
        {
            ToilLevelButton.gameObject.SetActive(true);
        }
        if (HellSoul >= 8)
        {
            AvariceLevelButton.gameObject.SetActive(true);
        }

        //Update Hellrescorse/
        if (realTime.IfYearUpdate ==true)
        {
            HellYear = realTime.currentDay;
            uiManager.HellYear.text = "HellYear: " + realTime.currentDay.ToString();
            Sin();
        }
        Lamp();
        ReBirthOrDeath();
        HeatCoolBar();
        MouseSelect();

        //Win
        if (rebirthsoul.RebirthSoul > deathsoul.DeathSoul && HellYear == 20)
        {
            End.SetActive(true);
            uiManager.EndMassage.text = "You Win!\r\nYou has run this hell for 20 years and helped more souls for reincarnation".ToString();
            Time.timeScale = 0;
        }
        //Lose
        if (rebirthsoul.RebirthSoul < deathsoul.DeathSoul && HellYear == 20)
        {
            Lose();
            uiManager.EndMassage.text = "Game Over!\r\nThe operation of this hell has led to more deaths than reincarnation.".ToString();
            SceneManager.LoadScene("Lose");
        }
        else if (Mathf.Abs(HellHeat - HellCool) > 10)
        {
            Lose();
            uiManager.EndMassage.text = "Game Over!\r\nAll souls can not be reincarnated due to the high temperature difference between heat and cool in this hell.".ToString();
            SceneManager.LoadScene("Lose");
        }
        else if (HellCoin < 0)
        {
            Lose();
            uiManager.EndMassage.text = "Game Over!\r\nAll HellCoin has been exhusted.".ToString();
            SceneManager.LoadScene("Lose");
        }
    }


    //Show sliders When Double click soul to open the profile panel
    public void Conviction()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        for (int i = 0; i < units.Length; i++)
        {
            UnitInfo unit = units[i].GetComponent<UnitInfo>();
            if (unit.ChooseLevel)
            {
                if (!unit.Convicted)
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
                    unit.AllYear = 0;

                    uiManager.Pretense.text = "" + pretenseSlider.PretenseYear;
                    uiManager.Sloth.text = "" + slothSlider.SlothYear;
                    uiManager.Steal.text = "" + stealSlider.StealYear;
                    uiManager.Greed.text = "" + greedSlider.GreedYear;
                    uiManager.Murder.text = "" + murderSlider.MurderYear;
                    uiManager.Betrayel.text = "" + betrayelSlider.BetrayelYear;
                    uiManager.Lust.text = "" + lustSlider.LustYear;
                    uiManager.AllSinYear.text = "" + unit.AllYear;

                    Judge.SetActive(true);
                    uiManager.JudgeMessage.text = "Please judge.".ToString();


                }
                else
                {
                    ChoosePanel.SetActive(false);
                    Judge.SetActive(false);
                    uiManager.Pretense.text = "" + unit.PretenseYear;
                    uiManager.Sloth.text = "" + unit.SlothYear;
                    uiManager.Steal.text = "" + unit.StealYear;
                    uiManager.Greed.text = "" + unit.GreedYear;
                    uiManager.Murder.text = "" + unit.MurderYear;
                    uiManager.Betrayel.text = "" + unit.BetrayelYear;
                    uiManager.Lust.text = "" + unit.LustYear;
                    uiManager.AllSinYear.text = "" + unit.AllYear;



                }
            }
        }

    }
    //Press save button to caculate allSinYears and save it. Check the rationality of judgement.
    public void SinYear()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        for (int i = 0; i < units.Length; i++)
        {
            UnitInfo unit = units[i].GetComponent<UnitInfo>();
            if (unit.ChooseLevel)
            {
                Debug.Log("unit.ChooseLevel" + unit.ChooseLevel);
                if (!unit.Convicted && ChoosePanel.activeInHierarchy)
                {
                    FindObjectOfType<AudioManager>().Play("click");
                    unit.PretenseYear = pretenseSlider.PretenseYear;
                    unit.SlothYear = slothSlider.SlothYear;
                    unit.StealYear = stealSlider.StealYear;
                    unit.GreedYear = greedSlider.GreedYear;
                    unit.MurderYear = murderSlider.MurderYear;
                    unit.BetrayelYear = betrayelSlider.BetrayelYear;
                    unit.LustYear = lustSlider.LustYear;
                    unit.AllYear = unit.PretenseYear + unit.SlothYear + unit.StealYear + unit.MurderYear + unit.BetrayelYear + unit.GreedYear + unit.LustYear;

                    uiManager.Pretense.text = "" + unit.PretenseYear;
                    uiManager.Sloth.text = "" + unit.SlothYear;
                    uiManager.Steal.text = "" + unit.StealYear;
                    uiManager.Greed.text = "" + unit.GreedYear;
                    uiManager.Murder.text = "" + unit.MurderYear;
                    uiManager.Betrayel.text = "" + unit.BetrayelYear;
                    uiManager.Lust.text = "" + unit.LustYear;
                    uiManager.AllSinYear.text = "" + unit.AllYear;

                    if (unit.AllYear <= 15 && unit.AllYear >= 3)
                    {
                        Judge.SetActive(true);
                        uiManager.JudgeMessage.text = "Judge completed.".ToString();
                        ChoosePanel.gameObject.SetActive(false);
                        unit.Convicted = true;
                    }
                    else if (unit.AllYear > 15 || unit.AllYear < 3)
                    {
                        Judge.SetActive(true);
                        uiManager.JudgeMessage.text = "Judge again.".ToString();
                        HellCoin -= 1;
                    }
                }
            }
        }

    }

    //Reduce Sinyears per year
    public void Sin()
    {

        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        for (int i = 0; i < units.Length; i++)
        {
            UnitInfo unit = units[i].GetComponent<UnitInfo>();

            if (unit.MemoryNumber >= 0 && unit.SpiritNumber > 0 && unit.FatigueNumber >= 0)
            {

                //FireLevel
                if (unit.ifOnFireLevel)
                {
                    //MurderYear and BetrayelYear -1/year;
                    if (unit.MurderYear >= 1)
                    {
                        unit.MurderYear -= 1;
                        uiManager.Lust.text = "" + unit.LustYear;

                    }
                    if (unit.BetrayelYear >= 1)
                    {
                        unit.BetrayelYear -= 1;
                        uiManager.Betrayel.text = "" + unit.BetrayelYear;
                    }

                }

                //FrozenLevel
                if (unit.ifOnFrozenLevel)
                {
                    //LustlYear -1/year;
                    if (unit.LustYear >= 1)
                    {
                        unit.LustYear -= 1;
                        uiManager.Lust.text = "" + unit.LustYear;

                    }
                }

                //ToilLevel
                if (unit.ifOnToilLevel)
                {
                    //PretenseYear and SlothYear -1/year;
                    if (unit.PretenseYear >= 1)
                    {
                        unit.PretenseYear -= 1;
                        uiManager.Pretense.text = "" + unit.PretenseYear;

                    }
                    if (unit.SlothYear >= 1)
                    {
                        unit.SlothYear -= 1;
                        uiManager.Sloth.text = "" + unit.SlothYear;
                    }

                }


                //AvariceLevel
                if (unit.ifOnAvariceLevel)
                {
                    //StealYear and GreedYear -1 /year;
                    if (unit.StealYear >= 1)
                    {
                        unit.StealYear -= 1;
                        uiManager.Steal.text = "" + unit.StealYear;

                    }
                    if (unit.GreedYear >= 1)
                    {
                        unit.GreedYear -= 1;
                        uiManager.Greed.text = "" + unit.GreedYear;
                    }

                }
            }
            //Update Sin Year
            unit.AllYear = unit.PretenseYear + unit.SlothYear + unit.StealYear + unit.MurderYear + unit.BetrayelYear + unit.GreedYear + unit.LustYear;
            uiManager.AllSinYear.text = "" + unit.AllYear;


            //Temperture difference between heat and cool is up to 2, all souls reduce 1 spirit/Year.
            if ((Mathf.Abs(HellHeat - HellCool) > 2)&& unit.SpiritNumber >= 1)
            {
                unit.SpiritNumber -= 1;
            }
          
            unit.UISoul();
           
        }

        //Produce Hell Resources per year
        //Consume Heat and Cool if there are souls in LivingLevel
        if (livingsoul.LivingSoul > 0 && HellHeat >= 1 && HellCool >=1)
        {
            HellHeat -= 1;
            HellCool -= 1;
        }

        //Produce Hell's Light per year. 1 HellHeat + 1 HellCoal = 1 Light
        if (HellHeat >=1 && HellCoal >= 1)
        {
            HellLight += 1;
            HellHeat -= 1;
            HellCoal -= 1;
            ProduceLight = true;
        }

        //Produce Hell's Water per year. 1 HellIce + 1 HellHeat = 1 HellWater
        if (HellCool >= 2 && HellHeat >= 1)
        {
            HellWater += 1;
            HellCool -= 2;
            HellHeat -= 1;

        }
    }

    public void Lose()
    {
        End.SetActive(true);
        Time.timeScale = 0;
    }

    //Light on lamp
    public void Lamp()
    {/*

        if (ProduceLight)
        {
            int randomIndex = Random.Range(0, lights.Count);
            int light = lights[randomIndex];
            lights.RemoveAt(randomIndex);

            Lights[light].SetActive(true);
            LampOnIndex++;
            Debug.Log("IndexLight" + light);
            ProduceLight = false;
        }

     */
    }
   

    
    //if soul is in Death level, reduce 1heat/1cool randomly. if soul is in Rebirth Level, go reborn
    public void ReBirthOrDeath()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        for (int i = 0; i < units.Length; i++)
        {
            UnitInfo unit = units[i].GetComponent<UnitInfo>();

            //Rebirth
            if (unit.ifOnRebirthLevel == true && HellWater >= 1)
            {
                if (realTime.IfYearUpdate == true)
                {
                    HellWater -= 1;
                }

                RebirthPanel.SetActive(true);
                Debug.Log("RebirthPanelActive");

                if (unit.SpiritNumber == 1 || unit.SpiritNumber == 2)
                {
                    uiManager.RebirthLifeName.text = "A PLANT".ToString();
                }
                else if (unit.SpiritNumber == 3 || unit.SpiritNumber == 4)
                {
                    uiManager.RebirthLifeName.text = "A ANIMAL".ToString();
                }
                else if (unit.SpiritNumber == 5 || unit.SpiritNumber == 6)
                {
                    uiManager.RebirthLifeName.text = "A PAUPER".ToString();
                }
                else if (unit.SpiritNumber == 7 || unit.SpiritNumber == 8)
                {
                    uiManager.RebirthLifeName.text = "A ORDINARY PEOPLE".ToString();
                }
                else if (unit.SpiritNumber == 9 || unit.SpiritNumber == 10)
                {
                    uiManager.RebirthLifeName.text = "A MEGNATE".ToString();
                }
                unit.RebornButton.gameObject.SetActive(false);
            }
            

            //Death

            if (unit.ifOnDeathLevel == true && (HellHeat >=1 || HellCool >=1))
            {
                if(realTime.IfYearUpdate == true)
                {
                    int x = (Random.Range(1, 2));
                    Debug.Log(x);
                    if (x == 1)
                    {
                        HellHeat -= 1;
                    }
                    else if(x==2)
                        HellCool -= 1;
                }
                unit.DeathButton.gameObject.SetActive(false);

            }
            
            
        }
    }

  
    private void MouseSelect()
    {
        if (Input.GetMouseButtonDown(1) && RebirthPanel.activeInHierarchy == true)
        {
            RebirthPanel.SetActive(false);
        }
    }

    public void UIScore()
    {
        uiManager.HellYear.text = "HellYear:" + HellYear.ToString();
        uiManager.Hellheat.text = "Heat:" + HellHeat.ToString();
        uiManager.Hellcool.text = "Cool:" + HellCool.ToString();
        uiManager.Hellcoal.text = "Coal:" + HellCoal.ToString();
        uiManager.Helllight.text = "Light:" + HellLight.ToString();
        uiManager.Hellwater.text = "Water:" + HellWater.ToString();
        uiManager.Hellcoin.text = "Coin:" + HellCoin.ToString();
    }
    public void HeatCoolBar()
    {
        HeatBar.fillAmount = HellHeat * 10 / 100f;
        CoolBar.fillAmount = HellCool * 10 / 100f;
    }
}