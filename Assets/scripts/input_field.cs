using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class input_field : MonoBehaviour {

    int level = 0;

    private void OnEnable()
    {
        level = 0;
        gameObject.GetComponent<Text>().text = level.ToString();
    }

    public int GetLevel()
    {
        return level;
    }

    public void LevelDown()
    {
        this.level = Mathf.Max(0, this.level - 1);
        this.gameObject.GetComponent<Text>().text = this.level.ToString();
    }

    public void LevelUp()
    {
        this.level = (int)Mathf.Min(GameObject.Find("statistics").GetComponent<stat>().GetLevel(), this.level + 1);
        this.gameObject.GetComponent<Text>().text = this.level.ToString();
    }

    public void VerifyLevel(GameObject play_button)
    {
        if (gameObject.GetComponent<Text>().text.Length > 0) {
            int lvl = 0;
            int.TryParse(gameObject.GetComponent<Text>().text, out lvl);
            if (lvl > GameObject.Find("statistics").GetComponent<stat>().GetLevel() || lvl < 0)
            {
                play_button.SetActive(false);
            }
            else
            {
                play_button.SetActive(true);
            }
        }
        else
        {
            play_button.SetActive(false);
        }
    }
}