using UnityEngine;

using UnityEngine;

public class PlaySoundAndEnableObject : MonoBehaviour
{
    [Header("Sound settings")]
    [SerializeField] private AudioSource audioSource; // Assign the AudioSource that will play the sound.

    [Header("Object settings")]
    [SerializeField] private GameObject objectToEnable; // Assign the GameObject to turn on.

    // Call this from a Button's On Click event or another script.
    public void PlaySoundAndEnable()
    {
        if (audioSource != null)
            audioSource.Play();

        if (objectToEnable != null)
            objectToEnable.SetActive(true);
    }
}