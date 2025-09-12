using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text _timerText;
    [SerializeField] AudioSource _timerAudio;
    [SerializeField] Spawn spawnObjects;

    [SerializeField] int _startTime = 3;

    public Animator animator;

    private void Start()
    {
        _timerText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_timerText.text == "GO!" && !_timerText.IsActive()) 
        {
            if (spawnObjects.isCanSpawn == true)
                spawnObjects.StartSpawnObjects();

            else if (spawnObjects.isCanSpawn == false)
                Debug.Log("Спавн закончился");
        }

    }

    public void СoutDown()
    {
        StartCoroutine(CoutDownTime());
    }

    IEnumerator CoutDownTime() 
    {
        Debug.Log("Кнопка нажата");

        _timerText.gameObject.SetActive(true);

        while (_startTime > 0)
        {
            _timerText.text = _startTime.ToString();
            _timerAudio.Play();
            yield return new WaitForSeconds(1);
            _startTime--;
        }
        _timerAudio.Stop();

        _timerText.text = "GO!";
        yield return new WaitForSeconds(1);

        _timerText.gameObject.SetActive(false);

        spawnObjects.isCanSpawn = true;
        Debug.Log("Спавн начался");
    }
}
