using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn_QuitGame : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("quitting application");
        Application.Quit();
    }
}
