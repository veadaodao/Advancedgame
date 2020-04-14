using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRoadLamp : MonoBehaviour
{
    public bool selected = false;
    public bool hover = false;
    public SoulScoreManager Lighting;
    public GameObject Light;
    LightHouse lightHouse;

    public void Start()
    {
        GameObject LightObject = GameObject.Find("LightHouse");
        lightHouse = LightObject.GetComponent<LightHouse>();
    }


    void OnMouseOver()
    {
        hover = true;

    }
    void OnMouseExit()
    {
        hover = false;

        selected = false;
    }
    public void OnMouseDown()
    {
        FindObjectOfType<AudioManager>().Play("pop");
        selected = true;
        if (Input.GetMouseButtonDown(0))
        {
            if (Lighting.HellLight > 0)
            {
                Light.gameObject.SetActive(true);
                Lighting.HellLight -= 1;
                lightHouse.LampOnIndex++;
            }
        }
    }
}
