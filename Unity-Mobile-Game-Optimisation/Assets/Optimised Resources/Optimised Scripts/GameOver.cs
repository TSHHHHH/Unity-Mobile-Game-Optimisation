using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreValueText;

    private void OnEnable()
    {
        scoreValueText.text = ScorePanel.score.ToString();
    }
}
