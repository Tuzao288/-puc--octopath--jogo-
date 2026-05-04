using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    public int score;

    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void ScoreUp(int scorePoints)
    {
        score += scorePoints;
    }
    public void ScoreDown(int scorePoints)
    {
        score = score / 2;
    }
}
