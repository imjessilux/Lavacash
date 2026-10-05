using UnityEngine;
using UnityEngine.SceneManagement;

public class test : MonoBehaviour
{

  public string niveau = "Niveau 1";

  public void ChangeLaScene()
  {
       SceneManager.LoadScene(niveau);
  }

}
