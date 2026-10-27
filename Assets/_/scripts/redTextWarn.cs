using UnityEngine;

using UnityEngine;
using UnityEngine.UI; // Needed for Unity UI Text.

public class ChangeTextColorToRed : MonoBehaviour
{
    [SerializeField] private Text textToChange; // Assign a legacy UI Text component here.

    // Call this from a Button's On Click event or another script.
    public void MakeTextRed()
    {
        if (textToChange != null)
            textToChange.color = Color.red;
    }
}
