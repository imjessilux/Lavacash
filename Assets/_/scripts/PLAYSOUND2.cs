using UnityEngine;

using UnityEngine;

public class PlaySoundGrabObject : MonoBehaviour
{
    [Header("Sound settings")]
    [SerializeField] private AudioSource audioSource; // Assign the AudioSource that will play the sound.



    // Call this from a Button's On Click event or another script.
    public void PlaySoundWhenGrab()
    {
        if (audioSource != null)
            audioSource.Play();

      
    }
}