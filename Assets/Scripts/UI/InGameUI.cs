using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject InGame_UI;
    [SerializeField] private GameObject Pause_UI;
    [SerializeField] private GameObject EndLevel_UI;

    [SerializeField] private TMP_Text fruitPoint;

    private bool gamePaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CheckIfGamePaused();
    }

    public void ReloadCurrentScene()
    {
        PlayerManager.instance.fruits = 0;
        AudioManager.instance.PlayBGM(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SwitchToMainMenu() => SceneManager.LoadScene("MainMenu");

    private bool CheckIfGamePaused()
    {
        if(!gamePaused)
        {
            gamePaused = true;
            SwitchTo(Pause_UI);
            Time.timeScale = 0;
            return false;
            
        }
        else
        {
            gamePaused = false;
            SwitchTo(InGame_UI);
            Time.timeScale = 1;
            return true;
        }
    }

    public void EndLevel() {
        fruitPoint.text = "+ " + PlayerManager.instance.fruits;
        AudioManager.instance.PlaySFX(0);
        SwitchTo(EndLevel_UI); 
    }
    public void SwitchTo(GameObject ui)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        ui.SetActive(true);
    }
}
