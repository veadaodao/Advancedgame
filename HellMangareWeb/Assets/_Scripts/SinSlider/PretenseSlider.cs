using UnityEngine;
using UnityEngine.UI;


public class PretenseSlider : MonoBehaviour
{
    public Slider slider;
    public int PretenseYear;
    public Gradient gradient;
    public UIManager uiManager;
    
    
    

    void Start()
    {
        GameObject UIObject = GameObject.Find("UIManager");
        uiManager = UIObject.GetComponent<UIManager>();

        PretenseYear = 0;
        slider.value = 0;

}

    
    public void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate {OnSliderWasChanged(); });
        OnSliderWasChanged();

    }
     public void OnSliderWasChanged()
     {
          PretenseYear = Mathf.RoundToInt(slider.value);
          uiManager.Pretense.text = "" + PretenseYear;

      }
    /*public void decrementSlider(float h) 
    {
        if (slideractive == false)
        {
            slider.value -= h;
            Debug.Log("newSlider=" + PretenseYear);

        }
    }*/

}



