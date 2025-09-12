using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public TMP_Text bestScoreText;
    [SerializeField] public TMP_Text scoreText;

    public int score;

    public int bestScore;

    private void Start()
    {
        score = 0;
    }
}
