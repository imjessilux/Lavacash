using UnityEngine;


using UnityEngine.SceneManagement;

public class gameover : MonoBehaviour
{
    public string gameOver2nd = "game over TEMPS"; // Nom exact ??

    public void ChangeLaScene()
    {
        SceneManager.LoadScene(gameOver2nd);
    }
}
