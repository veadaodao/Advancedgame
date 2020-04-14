using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HellScoreTutorial : MonoBehaviour
{
    public int HellHeat = 3;
    public int HellCool = 3;
    public int HellSoul = 0;
    public int HellCoal = 0;
    public int HellCoin = 3;
    public int HellLight = 0;
    public int HellWater = 0;

    public Canvas UnitWholeInfo;
    UITutorialManager uIManager;
    LightHouse lightHouse;

    public bool ProduceLight = false;

    int LampOnIndex;
    List<int> lights = new List<int>();
    public GameObject[] Lights;

    // Start is called before the first frame update
    void Start()
    {
        GameObject UIObject = GameObject.Find("UITutorialManager");
        uIManager = UIObject.GetComponent<UITutorialManager>();

        GameObject PoObject = GameObject.Find("LightHouse");
        lightHouse = PoObject.GetComponent<LightHouse>();

        for (int i = 0; i < Lights.Length; ++i)
        {
            lights.Add(i);
        }
        UIScore();
    }

    // Update is called once per frame
    void Update()
    {
        SoulScore();
        Lamp();
        UIScore();
    }

    public void SoulScore()
    {

        //Produce Hell's Light. 1 HellHeat + 1 HellCoal = 1 Light

        if (HellCoal >=1 && HellHeat >= 1)
        {
            HellLight += 1;
            HellHeat -= 1;
            HellCoal -= 1;
            ProduceLight = true;
        }
       
    }
    public void Lamp()
    {
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


    }
    
    public void UIScore()
        {
            uIManager.Hellheat.text = "Heat:" + HellHeat.ToString();
            uIManager.Hellcool.text = "Cool:" + HellCool.ToString();
            uIManager.Hellcoal.text = "Coal:" + HellCoal.ToString();
            uIManager.Helllight.text = "Light:" + HellLight.ToString();
            uIManager.Hellwater.text = "Water:" + HellWater.ToString();
            uIManager.Hellcoin.text = "Coin:" + HellCoin.ToString();
        }
}
