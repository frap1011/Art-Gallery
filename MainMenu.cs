using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    private string FirstLevel;

	public void gameTime()
    {
        SceneManager.LoadScene(FirstLevel);
    }
}
