using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadLamp : MonoBehaviour
{
    public bool selected = false;
    public bool hover = false;
    public HellScoreTutorial HellLight;
    public GameObject Light;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            if (HellLight.HellLight > 0)
            {
                Light.gameObject.SetActive(true);
                HellLight.HellLight -= 1;
            }
        }
    }
}
