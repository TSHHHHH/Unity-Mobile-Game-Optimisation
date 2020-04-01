using System.Collections.Generic;
using UnityEngine;

//This class respones for controling oscillating of the animal background.
public class AnimalBG : MonoBehaviour
{
    //Declare public variable for the oscillating range can make other developer and designer's life easier.
    //They don't need to go-through lines of code agian just because they want to change the speed.
    //
    public float oscillatingRange;

    private Vector2 leftExtent; //The left-most position the animal background can go
    private Vector2 rightExtent; //The right-most position the animal background can go

    private List<GameObject> childGOList; //All the animal background sprite game obejct atteched to the game object

    private bool showBackgroundCharacters = true;

    public float backgroundTime; //How long the animal background will display before disapear
    private float timeCounter;

    private void Start()
    {
        //Create and calculate instances of leftExtent and rightExtent 
        //
        leftExtent = new Vector2(transform.position.x - oscillatingRange, transform.position.y);
        rightExtent = new Vector2(transform.position.x + oscillatingRange, transform.position.y);

        childGOList = new List<GameObject>();

        //Add all child game object to the childGOList as we want to make the animal sprite game objects inactive instead of the whole group which will cause the script be inactive as well
        //
        detectChildGO();
    }

    private void detectChildGO()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            childGOList.Add(transform.GetChild(i).gameObject);
        }
    }

    private void Update()
    {
        AnimalBackgroundOscillat();
    }

    private void AnimalBackgroundOscillat()
    {
        timeCounter += Time.deltaTime;

        if (timeCounter >= backgroundTime)
        {
            showBackgroundCharacters = !showBackgroundCharacters;

            //Loop through all the child game obejct and set them to active or inactive
            //
            for (int i = 0; i < childGOList.Count; i++)
            {
                childGOList[i].gameObject.SetActive(showBackgroundCharacters);
            }

            //Reset timer to 0 to avoid the animal background sprite game obejcts keep switching between active and inactive
            timeCounter = 0;
        }
        else
        {
            //PingPongs the time, so that it is never larger than length and never smaller than 0.
            //We cant see the animal background move when they are inactive, so there is no need to move them when they are inactive
            //
            float time = Mathf.PingPong(Time.time * 1f, 1);
            transform.position = Vector3.Lerp(leftExtent, rightExtent, time);
        }
    }
}