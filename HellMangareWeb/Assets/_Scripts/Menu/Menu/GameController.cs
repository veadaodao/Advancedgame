using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject PauseButton;
    public GameObject ResumeButton;
    public GameObject MenuPanel;
    
    int choice;

    void Update()
    {
        MouseSelect();
    }

    private SoulData CreateSaveGameObject()
    {
        SoulData save = new SoulData();
        int i = 0;
        foreach (GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            UnitInfo unitInfo = unit.GetComponent<UnitInfo>();
            save.SoulName.Add(unitInfo.SoulName);
            save.SoulAge.Add(unitInfo.SoulAge);
            save.SoulReason.Add(unitInfo.SoulReason);
            save.SoulStory.Add(unitInfo.SoulStory);
            save.MemoryNumber.Add(unitInfo.MemoryNumber);
            save.SpiritNumber.Add(unitInfo.SpiritNumber);
            save.FatigueNumber.Add(unitInfo.FatigueNumber);

            save.ifOnLivingLevel.Add(unitInfo.ifOnLivingLevel);
            save.ifOnFireLevel.Add(unitInfo.ifOnFireLevel);
            save.ifOnFrozenLevel.Add(unitInfo.ifOnFrozenLevel);
            save.ifOnVisitingLevel.Add(unitInfo.ifOnVisitingLevel);
            save.ifOnSolitudeLevel.Add(unitInfo.ifOnSolitudeLevel);
            save.ifOnToilLevel.Add(unitInfo.ifOnToilLevel);
            save.ifOnAvariceLevel.Add(unitInfo.ifOnAvariceLevel);
            save.ifOnRebirthLevel.Add(unitInfo.ifOnRebirthLevel);
            save.ifOnDeathLevel.Add(unitInfo.ifOnDeathLevel);
            
            save.ChooseLevel.Add(unitInfo.ChooseLevel);
            save.TransLevel.Add(unitInfo.TransLevel);

            save.PositionX.Add(unitInfo.transform.position.x);
            save.PositionY.Add(unitInfo.transform.position.y);
            save.PositionZ.Add(unitInfo.transform.position.z);

            save.Convicted.Add(unitInfo.Convicted);
            save.PretenseYear.Add(unitInfo.PretenseYear);
            save.SlothYear.Add(unitInfo.SlothYear);
            save.StealYear.Add(unitInfo.StealYear);
            save.GreedYear.Add(unitInfo.GreedYear);
            save.MurderYear.Add(unitInfo.MurderYear);
            save.LustYear.Add(unitInfo.LustYear);
            save.AllYear.Add(unitInfo.AllYear);

            GameObject ScoreObject = GameObject.Find("SoulScoreManager");
            SoulScoreManager soulScore = ScoreObject.GetComponent<SoulScoreManager>();
            save.HellHeat = soulScore.HellHeat;
            save.HellCool = soulScore.HellCool;
            save.HellCoal = soulScore.HellCoal;
            //save.HellIce = soulScore.HellIce;
            save.HellWater = soulScore.HellWater;
            save.HellLight = soulScore.HellLight;
            save.HellCoin = soulScore.HellCoin;

            GameObject TimeObject = GameObject.Find("RealTimeManager");
            RealTime realTime = TimeObject.GetComponent<RealTime>();
            save.HellYear = realTime.currentDay;

            i++;
            
        }
        return save;
    }
    public void PressMenu()
    {
        FindObjectOfType<AudioManager>().Play("click");
        if (MenuPanel.activeInHierarchy == false)
        {
            MenuPanel.SetActive(true);
            switch (choice)
            {
                case 1:
                    Pause();
                break;

                case 2:
                    Resume();
                    break;

                case 3:
                    Save();
                    break;

                case 4:
                    Back();
                    break;
               
                case 5:
                    Quit();
                    break;
            }
        }
    }
    private void MouseSelect()
    {
        if (Input.GetMouseButtonDown(1) && MenuPanel.activeInHierarchy == true)
        {
            MenuPanel.SetActive(false);
        }
    }

    public void Pause()
    {
        FindObjectOfType<AudioManager>().Play("click");
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        for (int i = 0; i < units.Length; i++)
        {
            UnitInfo unit = units[i].GetComponent<UnitInfo>();
            PauseButton.SetActive(false);
            ResumeButton.SetActive(true);
            Time.timeScale = 0;

            unit.enabled = false;
        }
    }

    public void Resume()
    {
        FindObjectOfType<AudioManager>().Play("click");
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        for (int i = 0; i < units.Length; i++)
        {
            UnitInfo unit = units[i].GetComponent<UnitInfo>();
            PauseButton.SetActive(true);
            ResumeButton.SetActive(false);
            Time.timeScale = 1;
            unit.enabled = true;
        }
    }

    public void Save()
    {
        FindObjectOfType<AudioManager>().Play("click");
        Debug.Log("Saved1");
        SoulData save = CreateSaveGameObject();
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/soul.data";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, save);
        stream.Close();
        Debug.Log("Saved");
    }

    public void Back()
    {
        FindObjectOfType<AudioManager>().Play("click");
        Pause();
        MenuPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        FindObjectOfType<AudioManager>().Play("click");
        //UnityEditor.EditorApplication.isPlaying = false;

        //Application.Quit();
    }

}
