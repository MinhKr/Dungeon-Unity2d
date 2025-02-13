using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelButton;
    [SerializeField] private Transform contentParentButton;

    private void Start()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string nameScene = "Level " + i;
            Debug.Log(nameScene);

            //Create button level 
            GameObject newButton = Instantiate(levelButton , contentParentButton);
            newButton.AddComponent<Button>().onClick.AddListener(() => LoadScene(nameScene));

            //name Button
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = nameScene;
            
        }
    }

    //Load Scene
    public void LoadScene(string nameScene) => SceneManager.LoadScene(nameScene);
}
