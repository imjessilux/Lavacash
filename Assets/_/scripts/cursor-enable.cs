using UnityEngine;

public class SourisLock : MonoBehaviour
{
   public bool locked = false;

   void Start()
   {
       SetLocked2(locked);
   }

   public void SetLocked2(bool value)
   {
       locked = value;
       Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
       Cursor.visible = !locked;
   }
}