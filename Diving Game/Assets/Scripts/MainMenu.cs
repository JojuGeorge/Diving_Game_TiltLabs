using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string _level01;

    public void NewGame() {
        SceneManager.LoadScene(_level01);
    }

    public void Quit() {
        Application.Quit();
    }
}
