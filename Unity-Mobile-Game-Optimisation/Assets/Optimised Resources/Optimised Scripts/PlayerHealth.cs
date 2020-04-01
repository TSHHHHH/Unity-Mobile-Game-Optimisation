using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static int health;
    public int currentHealth;
    private int currentIndex;

    private GameObject[] healthGOArr;
    private CanvasController canvasController;
        
    void Start()
    {
        health = 3;

        currentHealth = health;

        currentIndex = health;

        healthGOArr = new GameObject[health];

        for(int i = 0; i < transform.childCount; i++)
        {
            healthGOArr[i] = transform.GetChild(i).gameObject;
        }

        canvasController = GameObject.FindGameObjectWithTag("Master Canvas").GetComponent<CanvasController>();
    }
        
    void Update()
    {
        if(health < currentHealth)
        {
            currentHealth = health;
                        
            if(currentIndex > 0)
            {
                healthGOArr[currentIndex - 1].GetComponent<Animator>().SetTrigger("Lose Life");

                currentIndex -= 1;
            }
        }else if(health > currentHealth)
        {
            currentHealth = health;

            if(currentIndex < 3)
            {
                healthGOArr[currentIndex].GetComponent<Animator>().SetTrigger("Gain Life");

                currentIndex += 1;
            }
        }
    }
}
