using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensivity : MonoBehaviour
{
    public Slider[] mouseSlidersSensivity;
    [SerializeField] Transform playerBody;

    public float _minSensivity = 400f;
    public float _maxSensivity = 1600f;
    public float _defaultSensivity = 400f;

    private Vector3 _upVector = Vector3.up;

    public float sensivity;

    private float xRotation = 0f;

    private void Awake()
    {
        sensivity = _defaultSensivity;

        for (int i = 0; i < mouseSlidersSensivity.Length; i++) 
        {
            mouseSlidersSensivity[i].value = (sensivity - _minSensivity) / (_maxSensivity - _minSensivity);

            mouseSlidersSensivity[i].onValueChanged.AddListener(value =>
            {
                sensivity = Mathf.Max(_minSensivity, value * _maxSensivity);
            });
        }

    }

    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensivity * Time.smoothDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensivity * Time.smoothDeltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(_upVector * mouseX);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < mouseSlidersSensivity.Length; i++) 
        {
            mouseSlidersSensivity[i].onValueChanged.RemoveAllListeners();
        }

    }
}
