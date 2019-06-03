using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    //Loads scene levelNum
    public void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene(levelNum);
    }

    //Closes the application
    public void CloseApp()
    {
        Application.Quit();
    }
}
