using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelChooseHandler : MonoBehaviour
{

    public input_field field;
    public void LevelUp()
    {
        field.GetComponent<input_field>().LevelUp();
    }

    public void LevelDown()
    {
        field.GetComponent<input_field>().LevelDown();
    }
}
