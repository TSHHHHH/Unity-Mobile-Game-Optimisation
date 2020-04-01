using UnityEngine;

//This class is attached to each sprite prefab individually instead of the parent game object (frame)
//Because we need to loop through every child in every frame in order to rotate each sprite which is inefficient
//
public class SpriteRotator : MonoBehaviour
{
    public float rotateSpeed;
    private Vector3 v3Rotation;

    private void Start()
    {
        //Caching local variables in the start to be more efficient
        v3Rotation = new Vector3(0f, 0f, rotateSpeed);
    }

    private void Update()
    {              
        if (FrameManager.initialised)
        {
            if (Time.timeScale > 0f)
            {
                transform.Rotate(v3Rotation);
            }
        }
    }
}