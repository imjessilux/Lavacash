using UnityEngine;
using UnityEngine.SceneManagement;

public class retournerA : MonoBehaviour
{

  public string accueilpage = "Accueil";

  public void ChangeLaScene()
  {
       SceneManager.LoadScene(accueilpage);
  }

}
