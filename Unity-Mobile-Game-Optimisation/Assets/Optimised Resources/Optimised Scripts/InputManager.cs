using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    private Camera mainCamera;

    public static bool inputEnabled = true;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public GameObject MobileInput()
    {
        //https://answers.unity.com/questions/959887/how-to-raycast-on-touch.html
        //Max. touch count at same time is 2 in default
        //
        Vector2[] touches = new Vector2[2];

        GameObject hitGO = null;

        if (FrameManager.initialised)
        {
            if (Input.touchCount > 0)
            {
                foreach (Touch t in Input.touches)
                {
                    touches[t.fingerId] = mainCamera.ScreenToWorldPoint(Input.GetTouch(t.fingerId).position);

                    if (Input.GetTouch(t.fingerId).phase == TouchPhase.Began)
                    {
                        RaycastHit2D hit = new RaycastHit2D();
                        hit = Physics2D.Raycast(touches[t.fingerId], Vector3.forward, 10f);

                        if (hit.collider.tag == "Optimised Frame" && !EventSystem.current.IsPointerOverGameObject(t.fingerId))
                        {
                            hitGO = hit.collider.gameObject;
                        }
                        else if (hit.transform.tag == "Pause Button")
                        {
                            hitGO = hit.collider.gameObject;
                        }else if(hit.transform.tag == "Add Player Health" || hit.transform.tag == "Add Time")
                        {
                            hitGO = hit.collider.gameObject;
                        }
                    }
                }
            }
        }
        return hitGO;
    }

    public GameObject PCInput()
    {
        // Check if the user has selected a frame (mousedown or touch). This is done by
        // casting a ray into the game view from the input position (after converting
        // from screen to world coordinates).
        //
        // Returns the frame that was selected, if any.
        //
        GameObject hitGO = null;

        if (FrameManager.initialised)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 raypos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D[] hit = new RaycastHit2D[1];

                Physics2D.RaycastNonAlloc(raypos, Vector3.forward, hit);

                if (hit[0])
                {
                    if (hit[0].collider.tag == "Optimised Frame" && !EventSystem.current.IsPointerOverGameObject())
                    {
                        hitGO = hit[0].collider.gameObject;
                    }
                    else if (hit[0].transform.tag == "Pause Button")
                    {
                        hitGO = hit[0].collider.gameObject;
                    }else if (hit[0].transform.tag == "Add Player Health" || hit[0].transform.tag == "Add Time")
                    {
                        hitGO = hit[0].collider.gameObject;
                    }
                }
            }
        }
        return hitGO;
    }
}