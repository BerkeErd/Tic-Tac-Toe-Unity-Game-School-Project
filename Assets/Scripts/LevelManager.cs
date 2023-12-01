using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    

    public void LoadLevel(string Name)
    { Debug.Log("Level Load requested : " + Name);
        SceneManager.LoadScene(Name);

    }
    public void QuitRequest()
    { Debug.Log("Quit requested");
        Application.Quit();
    }
    private void Start()
    {


    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(Application.loadedLevel);
    }

    

        

}
