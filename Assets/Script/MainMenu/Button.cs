using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Button : MonoBehaviour {
    public Transform MainMenu, OptMenu;
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OptionMenu(bool clicked)
    {
        if(clicked)
        {
            OptMenu.gameObject.SetActive(clicked);
            MainMenu.gameObject.SetActive(!clicked);
        }
        else
        {
            OptMenu.gameObject.SetActive(clicked);
            MainMenu.gameObject.SetActive(!clicked);
        }
    }

}
