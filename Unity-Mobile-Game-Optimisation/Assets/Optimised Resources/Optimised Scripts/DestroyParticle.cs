using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    private ParticleSystem ps;

    private void Start()
    {
        ps = transform.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        //If the partical system finished playing, set it to inactive for object pooling later
        if (!ps.IsAlive())
        {
            transform.gameObject.SetActive(false);
        }
    }
}