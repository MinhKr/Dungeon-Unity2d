using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void SwitchTo(GameObject ui)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        ui.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
