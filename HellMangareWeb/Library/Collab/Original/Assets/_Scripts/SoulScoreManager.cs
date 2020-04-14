using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulScoreManager : MonoBehaviour
{

    public Text Memory;
    public Text Spirit;
    public Text Fatigue;

    public Text MurderYear;
    public Text BetrayalYear;
    public Text LustYear;
    public Text PretenseYear;
    public Text SlothYear;
    public Text StealYear;
    public Text GreedYear;

    public BaseSoulClasses soul;

    public LivingSoulManager LivingSoulManager;
    public VisitingSoulManager VisitingSoulManager;
    public SolitudeSoulManager SolitudeSoulManager;
    public FireSoulManager FireSoulManager;
    public FrozenSoulManager FrozenSoulManager;
    public ToilSoulManager ToilSoulManager;
    public AvariceSoulManager AvariceSoulManager;

    int ifInLivingLevel = -1;
    int ifInVisitingLevel = -1;
    int ifInFireLevel = -1;
    int ifInFrozenLevel = -1;
    int ifInSolitudeLevel = -1;
    int ifInToilLevel = -1;
    int ifInAvariceLevel = -1;

    void Start()
    {
        //soul = new BaseSoulClasses("John", "A soldier", Random.Range(1,10), Random.Range(1, 10), Random.Range(1, 10),4,0,2,0,0,0,0);
        soul = new BaseSoulClasses();
        soul.MemoryNumber = Random.Range(1, 10);
        soul.SpiritNumber = Random.Range(1, 10);
        soul.FatigueNumber = Random.Range(1, 10);

        soul.MurderYear = 4;
        soul.BetrayalYear = 0;
        soul.LustYear = 2;
        soul.PretenseYear = 0;
        soul.SlothYear = 0;
        soul.StealYear = 0;
        soul.GreedYear = 0;
    }
    public void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.tag == "LivingLevel" && ifInLivingLevel == -1)
        {
            LivingSoulManager.updateLivingSoulin(1);
        }

        if (collision.gameObject.tag == "LivingLevel" && ifInLivingLevel== 1)
        {
            LivingSoulManager.updateLivingSoulout(1);
        }
        if (collision.gameObject.tag == "VisitingLevel" && ifInVisitingLevel == -1)
        {
            VisitingSoulManager.updateVisitingSoulin(1);
        }

        if (collision.gameObject.tag == "VisitingLevel" && ifInVisitingLevel == 1)
        {
            VisitingSoulManager.updateVisitingSoulout(1);
        }

        if (collision.gameObject.tag == "SolitudeLevel" && ifInSolitudeLevel == -1)
        {
            SolitudeSoulManager.updateSolitudeSoulin(1);
        }

        if (collision.gameObject.tag == "SolitudeLevel" && ifInSolitudeLevel == 1)
        {
            SolitudeSoulManager.updateSolitudeSoulout(1);
        }

        if (collision.gameObject.tag == "FireLevel" && ifInFireLevel == -1)
        {
            FireSoulManager.updateFireSoulin(1);
        }

        if (collision.gameObject.tag == "FireLevel" && ifInFireLevel == 1)
        {
            FireSoulManager.updateFireSoulout(1);
        }

        if (collision.gameObject.tag == "FrozenLevel" && ifInFrozenLevel == -1)
        {
            FrozenSoulManager.updateFrozenSoulin(1);
        }

        if (collision.gameObject.tag == "FrozenLevel" && ifInFrozenLevel == 1)
        {
            FrozenSoulManager.updateFrozenSoulout(1);
        }

        if (collision.gameObject.tag == "ToilLevel" && ifInToilLevel == -1)
        {
            ToilSoulManager.updateToilSoulin(1);
        }

        if (collision.gameObject.tag == "ToilLevel" && ifInToilLevel == 1)
        {
            ToilSoulManager.updateToilSoulout(1);
        }

        if (collision.gameObject.tag == "AvariceLevel" && ifInAvariceLevel == -1)
        {
            AvariceSoulManager.updateAvariceSoulin(1);
        }

        if (collision.gameObject.tag == "AvariceLevel" && ifInAvariceLevel == 1)
        {
            AvariceSoulManager.updateAvariceSoulout(1);
        }

    }
    public void OnTriggerExit(Collider collision)
    {
        ifInLivingLevel = -ifInLivingLevel;
    }

   public void SoulScore()
    {
        if (ifInLivingLevel == 1 && soul.MemoryNumber > 0)
        {
            soul.MemoryNumber -= 1;
            soul.FatigueNumber += 1;
        }

        if (ifInSolitudeLevel == 1)
        {
            soul.SpiritNumber += 1;
        }

        if (ifInVisitingLevel == 1)
        {
            int x = (Random.Range(1, 7));
            if (x == 7)
            {
                soul.MemoryNumber += 2;
            }
        }

        if ((ifInFireLevel == 1 || ifInFrozenLevel == 1 || ifInAvariceLevel == 1 || ifInToilLevel == 1) && soul.FatigueNumber > 0)
        {
            soul.FatigueNumber -= 1;
        }
        if (ifInFireLevel == 1 && (soul.MurderYear >0 || soul.BetrayalYear >0))
        {
            soul.MemoryNumber -= 1;
            soul.MurderYear -= 1;
            soul.BetrayalYear -= 1;
        }
        if (ifInFrozenLevel == 1 && soul.LustYear >0)
        {
            soul.MemoryNumber -= 1;
            soul.LustYear -= 1;
        }
        if (ifInToilLevel == 1 && (soul.PretenseYear >0 || soul.SlothYear >0))
        {
            soul.MemoryNumber -= 1;
            soul.PretenseYear -= 1;
            soul.SlothYear -= 1;
        }
        Debug.Log("Memory=" + soul.MemoryNumber);
        Memory.text = "Memory:" + soul.MemoryNumber.ToString();
        Spirit.text = "Spirit:" + soul.SpiritNumber.ToString();
        Fatigue.text = "Fatigue:" + soul.FatigueNumber.ToString();

        MurderYear.text = "Murder:" + soul.MurderYear.ToString();
        BetrayalYear.text = "Betrayal:" + soul.BetrayalYear.ToString();
        LustYear.text = "Lust:" + soul.LustYear.ToString();
        PretenseYear.text = "Pretense:" + soul.PretenseYear.ToString();
        SlothYear.text = "Sloth:" + soul.SlothYear.ToString();
        StealYear.text = "Steal:" + soul.StealYear.ToString();
        GreedYear.text = "Greed:" + soul.GreedYear.ToString();

    }

}
