using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadScene(int _number)
    {
        SceneManager.LoadScene(_number);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void DisableGameObject(GameObject _go)
    {
        _go.SetActive(false);
    }
}
