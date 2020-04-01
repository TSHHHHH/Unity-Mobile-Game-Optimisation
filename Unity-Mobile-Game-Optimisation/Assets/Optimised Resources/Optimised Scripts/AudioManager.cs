using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip correctSound;
    public AudioClip wrongSound;

    private AudioSource sound;

    private void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
    }

    public void playSound(bool correct)
    {
        //Play the audio clip only once depends on the boolean variable
        if (correct)
        {
            sound.PlayOneShot(correctSound);
        }
        else
        {
            sound.PlayOneShot(wrongSound);
        }
    }
}