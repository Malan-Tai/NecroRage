using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string mainScene;
    [SerializeField]
    private string menuScene;

    [SerializeField]
    private GameObject options;

    [SerializeField]
    private GameObject main;

    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject firstSlider;


    public void Tuto()
    {
        SoundManager.PlaySound(SoundManager.Sound.Validation_menu);
        SceneManager.LoadScene("Tuto");
    }

    public void Play()
    {
        SoundManager.PlaySound(SoundManager.Sound.Validation_menu);
        SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
        SoundManager.PlaySound(SoundManager.Sound.Back_menu);
        Application.Quit();
    }

    public void Options()
    {
        SoundManager.PlaySound(SoundManager.Sound.Validation_menu);
        EventSystem.current.SetSelectedGameObject(firstSlider);
        main.SetActive(false);
        options.SetActive(true);
    }

    public void Back()
    {
        SoundManager.PlaySound(SoundManager.Sound.Back_menu);
        EventSystem.current.SetSelectedGameObject(playButton);
        options.SetActive(false);
        main.SetActive(true);
    }

    public void Menu()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void ChangeAnythingSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.Movement_menu,0.5f);
    }
}
