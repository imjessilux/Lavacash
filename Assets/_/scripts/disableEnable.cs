using UnityEngine;

using UnityEngine;

public class GameObjectSwitcher : MonoBehaviour
{ // making of field to assign object + group fo object 
    [Header("Objects to deactivate")]
    [SerializeField] private GameObject[] objectsToDeactivate;

    [Header("Objects to activate")]
    [SerializeField] private GameObject[] objectsToActivate;
    

     [Header("Animators to stop")]
    [SerializeField] private Animator[] animatorsToStop;

    // function to be called to switch the enable/diusable of stuff
    public void SwitchObjects()
    {
        // set the group of object false to disable or true to activate
        SetActiveState(objectsToDeactivate, false);
        SetActiveState(objectsToActivate, true);
          StopAnimations();
    }

    // second list of object -> to turn on
    private void SetActiveState(GameObject[] objects, bool active)
    {
        if (objects == null) return;

        foreach (GameObject obj in objects)
        {
            if (obj != null)
                obj.SetActive(active);
        }
    }

    private void StopAnimations()
    {
        if (animatorsToStop == null) return;

        foreach (Animator animator in animatorsToStop)
        {
            if (animator != null)
                animator.StopPlayback();
        }
    }
}