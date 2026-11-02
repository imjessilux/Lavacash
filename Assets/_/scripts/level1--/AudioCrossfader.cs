using UnityEngine;

public class MouseLock : MonoBehaviour
{
   public bool locked = false;

   void Start()
   {
       SetLocked(locked);
   }

   public void SetLocked(bool value)
   {
       locked = value;
       Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
       Cursor.visible = !locked;
   }
}