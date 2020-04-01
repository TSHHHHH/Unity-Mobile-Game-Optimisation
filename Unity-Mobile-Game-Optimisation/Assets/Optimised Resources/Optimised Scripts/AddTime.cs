using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTime : Bouncer
{
    private InputManager inputManager;
    private GameObject hitGo;

    private void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("Input Manager").GetComponent<InputManager>();
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            hitGo = inputManager.MobileInput();
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            hitGo = inputManager.PCInput();
        }

        if (hitGo && hitGo.tag == "Add Time")
        {
            TimePanel.timeLeft += 5f;

            gameObject.SetActive(false);
        }
    }
}
