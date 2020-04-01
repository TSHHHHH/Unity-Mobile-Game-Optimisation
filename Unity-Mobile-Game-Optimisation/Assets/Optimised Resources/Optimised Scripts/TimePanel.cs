using TMPro;
using UnityEngine;

public class TimePanel : MonoBehaviour
{
    public static float timeLeft;

    [SerializeField]
    private TextMeshProUGUI timeText;
    private Animator textAnimator;

    private void Start()
    {
        timeLeft = 60f;
        

        //Cache public variable in start to increase performance
        //
        timeText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        textAnimator = timeText.GetComponent<Animator>();
        textAnimator.SetFloat("Time Left", timeLeft);
    }

    private void Update()
    {                
        if (FrameManager.initialised)
        {
            if (timeLeft >= 0)
            {
                UpdateTime();
            }
        }
    }

    //Update the time text in the UI
    //
    private void UpdateTime()
    {
        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString("F0");
        textAnimator.SetFloat("Time Left",timeLeft);
    }
}