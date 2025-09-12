using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject[] _spawnObjects;
    [SerializeField] GameObject[] _points;

    [SerializeField] ScoreManager _scoreManager;

    // время спавна
    [SerializeField] float _timeBtwSpawn;
    [SerializeField] float _startTimeBtwSpawn;

    [SerializeField] float _decreaseTime;
    [SerializeField] float _minTime = 0.65f;

    public bool isCanSpawn;

    public void StartSpawnObjects()
    {
        if (_timeBtwSpawn <= 0)
        {
            int rand = Random.Range(0, _spawnObjects.Length);

            int randSpawn = Random.Range(0, _points.Length);

            Instantiate(_spawnObjects[rand], _points[randSpawn].transform.position, Quaternion.identity);
            _timeBtwSpawn = _startTimeBtwSpawn;

            _scoreManager.bestScore = (int)_scoreManager.score;
            _scoreManager.scoreText.text = "" + _scoreManager.bestScore.ToString();

            if (PlayerPrefs.GetInt("score") <= _scoreManager.bestScore) 
            {
                PlayerPrefs.SetInt("score", _scoreManager.bestScore);
            }

            if (_startTimeBtwSpawn > _minTime)
            {
                _startTimeBtwSpawn -= _decreaseTime;
            }

        }

        else
        {
            _timeBtwSpawn -= Time.deltaTime;
        }
    }


}
