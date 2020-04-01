using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FrameManager : MonoBehaviour
{
    public float gameWidth { get; private set; }    // Width of the game view
    public float gameHeight { get; private set; }   // Height of the game view

    public float leftExtent;
    private float rightExtent;

    [Header("Frame Info")]
    public GameObject framePrefab;                  // Used to create a new frame of sprites

    public float speed;
    public float frameWidth;               // The width of a frame of sprites
    public float gap;
    public ParticleSystem successParticles;         // Particle system to run when a match is correct
    public ParticleSystem failParticles;            // Particle system to run when a match is wrong

    public static bool initialised;       // When initialised is true, the game can start
    public GameObject endFrame;

    private InputManager inputManager;
    private AudioManager audioSource;

    private GameObject hitFrame;

    private Camera mainCamera;

    public static bool gameEnd;

    public GameObject startCountDownCanvas;
    private float startTimeCount;
    public TextMeshProUGUI startTimeCountText;

    public List<GameObject> itemList;

    public float difficultMultiplier;

    private void Start()
    {
        initialised = false;

        mainCamera = Camera.main;

        gameHeight = mainCamera.orthographicSize * 2f;
        gameWidth = mainCamera.aspect * gameHeight;

        leftExtent = -gameWidth;
        rightExtent = gameWidth;

        int n = 0;
        float currX = leftExtent;
        frameWidth = framePrefab.GetComponent<BoxCollider2D>().size.x;
        gap = frameWidth * 0.05f;

        while (currX < rightExtent)
        {
            Vector3 currPos = new Vector3(currX, 0f, 0f);
            GameObject frame = displayFrame("Optimised Frame");
            frame.name = "Frame_" + n++;

            frame.transform.position = currPos;

            endFrame = frame;

            currX += frameWidth + gap;
        }

        startTimeCount = 3f;
        StartCoroutine(startCountDown());

        

        audioSource = GameObject.FindGameObjectWithTag("Audio Manager").GetComponent<AudioManager>();
        inputManager = GameObject.FindGameObjectWithTag("Input Manager").GetComponent<InputManager>();

        gameEnd = false;

        difficultMultiplier = 1f;
    }

    private IEnumerator startCountDown()
    {
        yield return null;

        startCountDownCanvas.SetActive(true);

        while (startTimeCount > 0)
        {
            startTimeCount -= Time.deltaTime;
            startTimeCountText.text = startTimeCount.ToString("F0");
            if (startTimeCount <= 0)
            {
                startTimeCountText.text = "GO!";
                yield return new WaitForSeconds(1f);
                startCountDownCanvas.SetActive(false);
                initialised = true;
            }
            yield return null;
        }
    }

    private void Update()
    {
        if (initialised && !gameEnd)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                hitFrame = inputManager.MobileInput();
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                hitFrame = inputManager.PCInput();
            }

            if (hitFrame && hitFrame.tag == "Optimised Frame")
            {
                if (hitFrame.GetComponent<FrameInfo>().mirror)
                {
                    GameObject newFrame = displayFrame("Optimised Frame");

                    newFrame.name = hitFrame.name;

                    RandomDropItem(hitFrame);

                    hitFrame.SetActive(false);

                    displayParticle("Success Particles");

                    audioSource.playSound(true);

                    ScorePanel.score++;

                    difficultMultiplier += 0.2f;
                }
                else
                {
                    displayParticle("Fail Particles");

                    audioSource.playSound(false);

                    PlayerHealth.health -= 1;
                }
            }
        }
    }

    public void RandomDropItem(GameObject hitFrame)
    {
        bool drop = Random.Range(1, 11) > 5 ? true : false;

        if (drop)
        {
            string itemName = itemList[Random.Range(0, itemList.Count)].name;
            
            displayItem(itemName, hitFrame);
        }
    }

    public GameObject displayFrame(string pooledObjectName)
    {
        GameObject obj = ObjectPooling.script.GetPooledObjects(pooledObjectName);

        if (obj != null)
        {
            obj.SetActive(true);

            float newX = endFrame.transform.position.x + frameWidth + gap;

            obj.transform.position = new Vector3(newX, 0f, 0f);

            endFrame = obj;
        }

        return obj;
    }

    public GameObject displayParticle(string pooledObjectName)
    {
        GameObject obj = ObjectPooling.script.GetPooledObjects(pooledObjectName);

        if (obj != null)
        {
            obj.SetActive(true);
            obj.GetComponent<ParticleSystem>().Play();
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            obj.transform.position = new Vector3(mousePos.x, mousePos.y, -1f);
        }

        return obj;
    }

    public GameObject displayItem(string pooledObjectName, GameObject hitFrame)
    {
        GameObject obj = ObjectPooling.script.GetPooledObjects(pooledObjectName);

        if (obj != null)
        {
            obj.SetActive(true);
            obj.transform.position = new Vector3(hitFrame.transform.position.x,hitFrame.transform.position.y,-1f);
        }

        return obj;
    }
}