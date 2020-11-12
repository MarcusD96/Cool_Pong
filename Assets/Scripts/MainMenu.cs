
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void SinglePlayer() {
        GameMode.isSingle = true;
        SceneManager.LoadScene("Singleplayer");
    }

    public void MultiPlayer() {
        GameMode.isSingle = false;
        SceneManager.LoadScene("Multiplayer");
    }
}
