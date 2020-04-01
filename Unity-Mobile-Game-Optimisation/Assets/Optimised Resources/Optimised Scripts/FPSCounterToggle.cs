using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounterToggle : MonoBehaviour
{
    public GameObject FPSPanel;

    public void ToggleFPSPanel(bool isToggeled)
    {
        FPSPanel.SetActive(isToggeled);
    }
}
