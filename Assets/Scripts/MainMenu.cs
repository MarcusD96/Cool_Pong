
using UnityEngine.SceneManagement;
using Mirror;

public class MainMenu : NetworkBehaviour {

    public void SinglePlayer() {
        GameMode.isSingle = true;
        SceneManager.LoadScene("Singleplayer");
    }

    public void MultiPlayer() {
        GameMode.isSingle = false;
        SceneManager.LoadScene("Multiplayer");
    }
}
