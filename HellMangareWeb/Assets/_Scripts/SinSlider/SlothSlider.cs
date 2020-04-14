using UnityEngine;
using UnityEngine.UI;



public class SlothSlider : MonoBehaviour
{
    public Slider slider;
    public int SlothYear;
    public Gradient gradient;
    public UIManager uiManager;
   // public bool slideractive = true;



    void Start()
    {
        GameObject UIObject = GameObject.Find("UIManager");
        uiManager = UIObject.GetComponent<UIManager>();

    }
    public void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { OnSliderWasChanged(); });
        OnSliderWasChanged();

    }


    public void OnSliderWasChanged()
    {
        SlothYear = Mathf.RoundToInt(slider.value);
        uiManager.Sloth.text = "" + SlothYear;

    }
    /*public void decrementSlider(float h)
    {
        if (slideractive == false)
        {
            slider.value -= h;
            Debug.Log("newSlider=" + SlothYear);

        }
    }*/

}