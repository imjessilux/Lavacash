using UnityEngine;
using TMPro;

public class ChangeTextColorToRed : MonoBehaviour
{
    [SerializeField] private TMP_Text textToChange;

    public void MakeTextRed()
    {
        if (textToChange != null)
            textToChange.color = Color.red;
    }
}