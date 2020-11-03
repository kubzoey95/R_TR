using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelHandle : MonoBehaviour {

    public GameObject stat;
    public GameObject pause;
    public GameObject restart;

	// Use this for initialization
	void Start () {

	}

    private string LevelName()
    {
        return "Level " + stat.GetComponent<stat>().GetLevel();
    }

    private string LevelName(int n)
    {
        return "Level " + n;
    }

    public static int GetLoadedLevelId()
    {
        int ret = -1;
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name.Contains("Level"))
            {
                if (SceneManager.GetSceneAt(i).isLoaded)
                {
                    int.TryParse(SceneManager.GetSceneAt(i).name.Substring(SceneManager.GetSceneAt(i).name.Length - 1), out ret);
                    return ret;
                }
            }
        }
        return ret;
    }

    public static string GetLoadedLevelName()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name.Contains("Level") || SceneManager.GetSceneAt(i).name == "Arcade")
            {
                if (SceneManager.GetSceneAt(i).isLoaded)
                {
                    return SceneManager.GetSceneAt(i).name;
                }
            }
        }
        return "";
    }

    public void LoadFromInputField(GameObject input_field)
    {
        //int n = 0;
        //int.TryParse(input_field.GetComponent<input_field>().level, out n);
        Activate(input_field.GetComponent<input_field>().GetLevel());
        //input_field.GetComponent<InputField>().text = "";
    }

    public void Activate(string levelName)
    {
        Physics.gravity = Vector3.up * (-9.8f);
        SceneManager.LoadSceneAsync(levelName);
        pause.SetActive(true);
        restart.SetActive(true);
    }

    public void ResetLevel()
    {
        Activate(GetLoadedLevelName());
    }

    public void NextLevel()
    {
        Activate(GetLoadedLevelId() + 1);
    }

    public void Activate()
    {
        Physics.gravity = Vector3.up * (-9.8f);
        SceneManager.LoadSceneAsync(LevelName());
        pause.SetActive(true);
        restart.SetActive(true);
    }

    public void ActivateArcade()
    {
        Physics.gravity = Vector3.up * (-9.8f);
        SceneManager.LoadSceneAsync("Arcade");
        pause.SetActive(true);
        restart.SetActive(true);
    }

    public void Activate(int n)
    {
        Physics.gravity = Vector3.up * (-9.8f);
        SceneManager.LoadSceneAsync(LevelName(n));
        pause.SetActive(true);
        restart.SetActive(true);
    }

    public void Deactivate()
    {
        SceneManager.LoadSceneAsync("null");
        pause.SetActive(false);
        restart.SetActive(false);
        Camera.main.GetComponent<Audio>().SetPlay(false);
        Camera.main.GetComponent<Audio>().controler = new GameObject();
    }

    public void TurnGameOff()
    {
        Application.Quit();
        
    }

    public void TimeScale(float scale)
    {
        Time.timeScale = scale;
    }

    public void Pause()
    {
        pause.SetActive(false);
        restart.SetActive(false);
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        pause.SetActive(true);
        restart.SetActive(true);
        Time.timeScale = 1;
    }

    public void Log()
    {
        Debug.LogError("debuggd");
    }
}
