using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuGO;
    [SerializeField] private string _reloadLevel;
    [SerializeField] private string _mainMenu;


    public void PauseGame() {
        Toggle();
    }

    public void ResumeGame() {
        Toggle();
    }
    public void ReloadGame() {
        Toggle();
        SceneManager.LoadScene(_reloadLevel);
    }

    public void MainMenu() {
        Toggle();
        SceneManager.LoadScene(_mainMenu);
    }

    public void Quit() {
        Application.Quit();
    }

    private void Toggle() {
        _pauseMenuGO.SetActive(!_pauseMenuGO.activeSelf);

        if (_pauseMenuGO.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else if(!_pauseMenuGO.activeSelf)
        {
            Time.timeScale = 1f;
        }

        Debug.Log(Time.timeScale);
    }
}
