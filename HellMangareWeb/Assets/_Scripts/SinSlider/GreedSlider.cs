using UnityEngine;
using UnityEngine.UI;



public class GreedSlider : MonoBehaviour
{
    public Slider slider;
    public int GreedYear;
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
        GreedYear = Mathf.RoundToInt(slider.value);
        uiManager.Greed.text = "" + GreedYear;

    }

    /*public void decrementSlider(float h)
    {
        if (slideractive == false)
        {
            slider.value -= h;
            Debug.Log("newSlider=" + GreedYear);

        }
    }*/

}