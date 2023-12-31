using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VFX;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingUI;
    [SerializeField] private GameObject welcomeUI;
    [SerializeField] private GameObject levelSelectionUI;

    [SerializeField] private Button playButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button welcomeButton;
    [SerializeField] private Button closeSettingButton;
    [SerializeField] private Button closeWelcomeButton;

    public static MainMenuUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        playButton.onClick.AddListener(PlayGame);
        settingButton.onClick.AddListener(SwitchSettingUI);
        welcomeButton.onClick.AddListener(SwitchWelcomeUI);
        closeSettingButton.onClick.AddListener(SettingClose);
        closeWelcomeButton.onClick.AddListener(SettingClose);
    }


    private void PlayGame()
    {
        mainMenu.SetActive(false);
        levelSelectionUI.SetActive(true);
    }

    private void SwitchSettingUI()
    {
        mainMenu.SetActive(false);
        settingUI.SetActive(true);
        
    }

    private void SwitchWelcomeUI()
    {
        mainMenu.SetActive(false);
        welcomeUI.SetActive(true);
    }

    private void SettingClose()
    {
        mainMenu.SetActive(true);
        settingUI.SetActive(false);
        welcomeUI.SetActive(false);
    }

    public void CloseLevelMenu()
    {
        levelSelectionUI.SetActive(false);
    }

    public void OpenLevelMenu()
    {
        levelSelectionUI.SetActive(true);
    }

    
}
