using TMPro;
using UnityEngine;

public class ScorePanel : MonoBehaviour
{
    public static float score;
    private float currentScore; //local variable to store score value

    private TextMeshProUGUI scoreText;

    private void Start()
    {
        //Get the text mesh pro child game object and component in start to a void the UpdateTime() function keep create new instance in run-time.
        //
        scoreText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        //Instead of keep updating score by checking score variable during run-time, use local variable (currentScore in this case) and it only update when score is changed to increase performance.
        //
        if (score > currentScore)
        {
            UpdateScore();
        }
    }

    // Update the Score text in the UI
    //
    private void UpdateScore()
    {
        currentScore = score;
        scoreText.text = currentScore.ToString();
    }
}