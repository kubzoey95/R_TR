using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowFPS : MonoBehaviour
{
    float deltaTime = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.01f;
    }

    void OnGUI()
    {
        GetComponent<Text>().text = ((int)(1.0f / deltaTime)).ToString();
    }
}
