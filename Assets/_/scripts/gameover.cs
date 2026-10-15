using UnityEngine;


using UnityEngine.SceneManagement;

public class gameoverL : MonoBehaviour
{
    public string gameOver2nd = "Game Over LAVE"; // Nom exact ??

    public void ChangeLaScene()
    {
        SceneManager.LoadScene(gameOver2nd);
    }
}
