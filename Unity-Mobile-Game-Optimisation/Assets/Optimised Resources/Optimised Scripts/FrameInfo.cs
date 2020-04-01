using System.Collections.Generic;
using UnityEngine;

public class FrameInfo : MonoBehaviour
{
    private GameObject top;
    private List<GameObject> topChildList;

    private GameObject bottom;
    private List<GameObject> bottomChildList;

    public bool mirror;

    public Sprite[] animalSprites;

    public List<GameObject> itemList;

    //To prevent the game creates new instance everytime when a new frame spawned during run time, we loop through and add child game object to 2 lists in Awake()
    //
    private void Awake()
    {
        top = transform.GetChild(0).gameObject;
        topChildList = new List<GameObject>();
        for (int i = 0; i < top.transform.childCount; i++)
        {
            topChildList.Add(top.transform.GetChild(i).gameObject);
        }

        bottom = transform.GetChild(1).gameObject;
        bottomChildList = new List<GameObject>();
        for (int i = 0; i < bottom.transform.childCount; i++)
        {
            bottomChildList.Add(bottom.transform.GetChild(i).gameObject);
        }

        itemList = new List<GameObject>();
    }

    private void Update()
    {
        if (FrameManager.gameEnd)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (PlayerHealth.health > -1)
        {
            SpawnSprite();
        }
    }

    private void OnDisable()
    {
        if (PlayerHealth.health < 0)
        {
            displayParticle("Game Over Particle");
        }
    }

    private void SpawnSprite()
    {
        //Randomize thetop sprites first
        //
        for (int i = 0; i < topChildList.Count; i++)
        {
            topChildList[i].GetComponent<SpriteRenderer>().sprite = animalSprites[Random.Range(0, animalSprites.Length)];
        }

        mirror = Random.Range(1, 11) > 5 ? true : false;

        //If the result of Random.Range() returns true, it means the bottom sprites will be the exactly same as the top sprites, therefore we set every bottom sprites to the same sprite.
        //If the result of Random.Range() returns false, it means the bottom sprites will not be the exactly same as the top sprites, therefore we randomize agian and set every bottom sprotes to a random animal sprite.
        //
        if (mirror)
        {
            for (int i = 0; i < bottomChildList.Count; i++)
            {
                bottomChildList[i].GetComponent<SpriteRenderer>().sprite = topChildList[i].GetComponent<SpriteRenderer>().sprite;
            }
        }
        else
        {
            for (int i = 0; i < bottomChildList.Count; i++)
            {
                bottomChildList[i].GetComponent<SpriteRenderer>().sprite = animalSprites[Random.Range(0, animalSprites.Length)];
            }
        }
    }

    private GameObject displayParticle(string pooledObjectName)
    {
        GameObject obj = ObjectPooling.script.GetPooledObjects(pooledObjectName);

        if (obj != null)
        {
            obj.SetActive(true);
            obj.GetComponent<ParticleSystem>().Play();
            obj.transform.position = transform.position;
        }

        return obj;
    }
}