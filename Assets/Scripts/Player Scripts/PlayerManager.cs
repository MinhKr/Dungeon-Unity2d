using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] private GameObject playerPrefab;
                     public Transform respawnPoint;
                     public GameObject currentPlayer;
    public Vector3 posStart;

    [Header("Skin")]
    public int choosenSkinId;

    [Header("Fruit")]
    public int fruits;

    [Header("Healthbar System")]
    public float healthCurrent;
    /*[SerializeField] private Image totalHealthBar;*/
    [SerializeField] private Image currentHealthBar;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

    }

    public void PlayerRespawn()
    {
        if (currentPlayer == null)
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, transform.rotation);

        this.posStart = respawnPoint.position;
    }

    private void Update()
    {
        currentHealthBar.fillAmount = healthCurrent /10;
    }
}
