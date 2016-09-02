using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneManagerInGame : MonoBehaviour {

    public Transform HUDCanvas, PauseMenu; 
    bool isLocked;
    bool isPaused;

	// Use this for initialization
	void Start () {
        SetCursorLock(true);
        SetPauseMenu(false);
        if(Time.timeScale ==0)
        {
            SetPause();
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            SetCursorLock(!isLocked);
            SetPause();
        }
	}

    void SetCursorLock(bool isLocked)
    {
        this.isLocked = isLocked;
        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
        Cursor.visible = !isLocked;
    }

    void SetPauseMenu(bool isPaused)
    {
        this.isPaused = isPaused;
        if(isPaused)
        {
            HUDCanvas.gameObject.SetActive(false);
            PauseMenu.gameObject.SetActive(true);
        }
        else
        {
            HUDCanvas.gameObject.SetActive(true);
            PauseMenu.gameObject.SetActive(false);
        }
    }

    void SetPause()
    {
        if (Time.timeScale == 1) //정지상태가 아니라면 
        {
            Time.timeScale = 0; //정지시킨다.
            SetPauseMenu(true);
        }
        else if(Time.timeScale == 0) // 정지중이라면
        {
            Time.timeScale = 1; //정지를 해제한다.
            SetPauseMenu(false);
        }
        // 1은 보통속도 0은 정지 1~0은 보통속도보다 느린속도.

    }

    public void ReturnGame()
    {
        Debug.Log("ReturnGame");
        SetCursorLock(!this.isLocked);
        SetPause();
        SetPauseMenu(false);
    }

    public void GoMainMenu()
    {
        Debug.Log("GotoMainMenu");
        SceneManager.LoadScene("scMainMenu");

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
