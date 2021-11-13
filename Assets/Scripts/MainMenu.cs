using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string mainScene;

    [SerializeField]
    private GameObject options;

    [SerializeField]
    private GameObject main;

    public void Play()
    {
        SceneManager.LoadScene(mainScene);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Options()
    {
        main.SetActive(false);
        options.SetActive(true);
    }

    public void Back()
    {
        options.SetActive(false);
        main.SetActive(true);
    }
}
