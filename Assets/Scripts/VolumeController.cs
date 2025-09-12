using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public string[] volumeParameters;
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    public float _volumeValue;
    public float _multiplier = 20f;

    private void Awake()
    {
        volumeSlider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void HandleSliderValueChanged(float value) 
    {
        // диапазон слайдера изначально от 0 до 1, нам же надо изменять значение громкости от 0 до -80 децибел
        _volumeValue = Mathf.Log10(value) * _multiplier;

        for (int i = 0; i < volumeParameters.Length; i++) 
        {
            audioMixer.SetFloat(volumeParameters[i], _volumeValue);
        }
    }
}
