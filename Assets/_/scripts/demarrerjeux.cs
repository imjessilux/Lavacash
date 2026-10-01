using UnityEngine;
using UnityEngine.SceneManagement;

public class demarrerjeux : MonoBehaviour
{

public string niveau1 = "niveau 1";

public void ChangeLaScene()
{
       SceneManager.LoadScene(niveau1);
}

}
