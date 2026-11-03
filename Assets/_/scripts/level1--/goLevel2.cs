using UnityEngine;
using UnityEngine.SceneManagement;

public class goLevel2 : MonoBehaviour
{

  public string niveau2 = "Niveau 2";

  public void ChangeLaScene()
  {
       SceneManager.LoadScene(niveau2);
  }

}
