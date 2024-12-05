using UnityEngine;
using UnityEngine.UI;  // Necesario para acceder al Slider

public class PotentiometroVolumen : MonoBehaviour
{
    public Slider potentiometerSlider; 
    //public Text valueText; 

    void Start()
    {

        potentiometerSlider.value = 0.5f;


        potentiometerSlider.onValueChanged.AddListener(UpdatePotentiometerValue);
    }

    void UpdatePotentiometerValue(float value)
    {

        //valueText.text = "Valor: " + value.ToString("F2");
        AudioListener.volume = value;
    }
}
