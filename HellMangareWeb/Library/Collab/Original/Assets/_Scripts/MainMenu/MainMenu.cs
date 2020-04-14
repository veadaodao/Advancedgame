using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public GameObject MainMenuPanel;
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;


    //RealTime realTime;

    public void Start()
    {
        //GameObject TimeObject = GameObject.Find("RealTimeManager");
        //realTime = TimeObject.GetComponent<RealTime>();
        Time.timeScale = 0;
    }
    public void Intro()
    {

    }
    public void PlayGame()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        //MainMenuPanel.SetActive(false);
        //SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        FindObjectOfType<AudioManager>().Play("click");
        //realTime.currentDay = 0;
        //realTime.currentTime = 0;
        Time.timeScale = 1;

    }

    public void GameLoad()
    {

        MainMenuPanel.SetActive(false);
        //realTime.currentDay = 0;
        //realTime.currentTime = 0;
        Time.timeScale = 1;

        FindObjectOfType<AudioManager>().Play("click");
        /*StartCoroutine(LoadAsynchronously());

        IEnumerator LoadAsynchronously()
        {

            AsyncOperation operation = SceneManager.LoadSceneAsync("SampleScene");

            loadingScreen.SetActive(true);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                slider.value = progress;
                progressText.text = progress * 100f + "%";

                yield return null;
            }
        }
        */


        string path = Application.persistentDataPath + "/soul.data";
        FileStream stream = new FileStream(path, FileMode.Open);

        if (File.Exists(path) && stream.Length > 0)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            SoulData save = formatter.Deserialize(stream) as SoulData;
            stream.Close();

            GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
            for (int i = 0; i < units.Length; i++)
            {
                UnitInfo unit = units[i].GetComponent<UnitInfo>();
                unit.SoulAge = save.SoulAge[i];
                unit.FatigueNumber = save.FatigueNumber[i];
                unit.SpiritNumber = save.SpiritNumber[i];
                unit.MemoryNumber = save.MemoryNumber[i];
                unit.SoulReason = save.SoulReason[i];
                unit.SoulStory = save.SoulStory[i];
                unit.SoulName = save.SoulName[i];

                unit.ChooseLevel = save.ChooseLevel[i];
                unit.TransLevel = save.TransLevel[i];

                unit.transform.position = new Vector3(
                            save.PositionX[i], save.PositionY[i], save.PositionZ[i] + 0.1f);
                unit.ifOnLivingLevel = save.ifOnLivingLevel[i];
                if (unit.ifOnLivingLevel == true)
                {
                    unit.UnitAgent.enabled = false;
                }
                unit.ifOnFireLevel = save.ifOnFireLevel[i];
                if (unit.ifOnFireLevel == true)
                {
                    unit.UnitAgent.enabled = false;
                }
                unit.ifOnFrozenLevel = save.ifOnFrozenLevel[i];
                if (unit.ifOnFrozenLevel == true)
                {
                    unit.UnitAgent.enabled = false;
                }
                unit.ifOnVisitingLevel = save.ifOnVisitingLevel[i];
                if (unit.ifOnFrozenLevel == true)
                {
                    unit.UnitAgent.enabled = false;
                }
                unit.ifOnSolitudeLevel = save.ifOnSolitudeLevel[i];
                if (unit.ifOnSolitudeLevel == true)
                {
                    unit.UnitAgent.enabled = false;
                }
                unit.ifOnToilLevel = save.ifOnToilLevel[i];
                if (unit.ifOnToilLevel == true)
                {
                    unit.UnitAgent.enabled = false;
                }
                unit.ifOnAvariceLevel = save.ifOnAvariceLevel[i];
                if (unit.ifOnAvariceLevel == true)
                {
                    unit.UnitAgent.enabled = false;
                }

                unit.ifOnRebirthLevel = save.ifOnRebirthLevel[i];
                if (unit.ifOnRebirthLevel == true)
                {
                    unit.UnitAgent.enabled = false;
                }
                unit.ifOnDeathLevel = save.ifOnDeathLevel[i];
                if (unit.ifOnDeathLevel == true)
                {
                    unit.UnitAgent.enabled = false;
                }

                unit.Convicted = save.Convicted[i];
                unit.PretenseYear = save.PretenseYear[i];
                unit.SlothYear = save.SlothYear[i];
                unit.StealYear = save.StealYear[i];
                unit.GreedYear = save.GreedYear[i];
                unit.MurderYear = save.MurderYear[i];
                unit.LustYear = save.LustYear[i];
                unit.AllYear = save.AllYear[i];

            }

            GameObject ScoreObject = GameObject.Find("SoulScoreManager");
            SoulScoreManager soulScore = ScoreObject.GetComponent<SoulScoreManager>();
            soulScore.HellHeat = save.HellHeat;
            soulScore.HellCool = save.HellCool;
            soulScore.HellCoal = save.HellCoal;
            //soulScore.HellIce = save.HellIce;
            soulScore.HellWater = save.HellWater;
            soulScore.HellLight = save.HellLight;
            soulScore.HellCoin = save.HellCoin;

            GameObject TimeObject = GameObject.Find("RealTimeManager");
            RealTime realTime = TimeObject.GetComponent<RealTime>();
            realTime.currentDay = save.HellYear;
            soulScore.UIScore();

        }
        else
        {
            Debug.LogError("Save file not found in " + path);

        }
        Debug.Log("Load");

    }


        public void Quit()
        {
           // Time.timeScale = 1;
           // UnityEditor.EditorApplication.isPlaying = false;

           //Application.Quit();
        }

    public void IntroTutorial()
    {
        FindObjectOfType<AudioManager>().Play("click");
        //SceneManager.LoadScene(1, LoadSceneMode.Single);
        //MainMenuPanel.SetActive(false);
        Destroy(MainMenuPanel);
        //SceneManager.UnloadSceneAsync(0);
    }

}
