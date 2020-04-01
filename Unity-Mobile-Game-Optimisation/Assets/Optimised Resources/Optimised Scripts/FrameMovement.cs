using System;
using UnityEngine;

public class FrameMovement : MonoBehaviour
{
    public float speed;
    private float currentSpeedMulti = 1f;

    private FrameManager frameManager;

    public Action<FrameSpeedChangedInfo> OnSpeedChangedStruct = delegate { };

    private Rigidbody2D rb;

    private bool speedChanger = false;

    private void Start()
    {
        frameManager = GameObject.Find("Frames Manager").GetComponent<FrameManager>();

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.x <= frameManager.leftExtent)
        {
            RespawnFrames();
        }
    }

    private void FixedUpdate()
    {
        if (FrameManager.initialised)
        {
            GetDistanceToNeighbour();

            OnSpeedChangedStruct = MoveFrames;
        }
    }

    private void MoveFrames(FrameSpeedChangedInfo info)
    {
        rb.velocity = new Vector2(speed * frameManager.difficultMultiplier * info.speedMultiplier, 0f);
    }

    private void GetDistanceToNeighbour()
    {
        Vector3 pos = transform.position;
        Vector3 raypos = new Vector3(pos.x - frameManager.frameWidth / 2f - frameManager.gap, 0f, pos.z);

        RaycastHit2D hit = Physics2D.Raycast(raypos, Vector3.left, frameManager.gap);

        if (hit)
        {
            if (hit.collider.tag == "Optimised Frame")
            {
                if (hit.distance >= frameManager.gap)
                {
                    speedChanger = true;
                    if (speedChanger)
                    {
                        SentEventWithStruct(10f);
                        speedChanger = false;
                    }
                }
                else
                {
                    speedChanger = true;
                    if (speedChanger)
                    {
                        SentEventWithStruct(1f);
                        speedChanger = false;
                    }
                }
            }
        }
        else
        {
            if (gameObject.transform.position.x > -frameManager.gameWidth / 2f)
            {
                speedChanger = true;
                if (speedChanger)
                {
                    SentEventWithStruct(10f);
                    speedChanger = false;
                }
            }
            else
            {
                speedChanger = true;
                if (speedChanger)
                {
                    SentEventWithStruct(1f);
                    speedChanger = false;
                }
            }
        }
    }

    private void SentEventWithStruct(float speedMultiplier)
    {
        OnSpeedChangedStruct(new FrameSpeedChangedInfo() { speedMultiplier = speedMultiplier });
    }

    private void RespawnFrames()
    {
        if (transform.position.x <= frameManager.leftExtent)
        {
            GameObject newFrame = frameManager.displayFrame("Optimised Frame");

            newFrame.name = gameObject.name;

            gameObject.SetActive(false);
        }
    }
}