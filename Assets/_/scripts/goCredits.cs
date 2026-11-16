using UnityEngine;
using UnityEngine.SceneManagement;

public class gocredits : MonoBehaviour
{

  public string credits = "credits";

  public void ChangeLaScene()
  {
       SceneManager.LoadScene(credits);
  }

}
