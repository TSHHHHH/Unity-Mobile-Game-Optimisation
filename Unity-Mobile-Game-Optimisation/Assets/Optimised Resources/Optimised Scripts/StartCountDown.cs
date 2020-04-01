using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartCountDown : MonoBehaviour
{
    public static float startTimeCount;

    private TextMeshProUGUI startTimeCountText;

    // Start is called before the first frame update
    void Start()
    {
        startTimeCount = 3f;

        startTimeCountText = transform.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(startTimeCount > 0)
        {
            UpdateTime();
        }
        else
        {
            startTimeCountText.text = "Go!";
        }
    }

    //Update the time text in the UI
    //
    private void UpdateTime()
    {
        startTimeCount -= Time.deltaTime;
        startTimeCountText.text = startTimeCount.ToString("F0");
    }
}
