using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private Button mLoadButton;

    void Start()
    {
        PlayerPrefs.SetInt("Load", 0);
        string path = Application.persistentDataPath + "/gamedata.dat";

        if(File.Exists(path))
        {
            mLoadButton.interactable = true;
        }

    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        PlayerPrefs.SetInt("Load", 1);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
