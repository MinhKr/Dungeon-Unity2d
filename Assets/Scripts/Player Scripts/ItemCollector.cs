using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemCollector : MonoBehaviour
{
    private int point = 0;
    [SerializeField] private Text TextPoint;
    [SerializeField] private AudioSource CollectionSound;

    private void Start()
    {
        TextPoint = GetComponent<Text>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruit"))
        {
            CollectionSound.Play();
            Destroy(collision.gameObject);
            point++;
            /*TextPoint.text = "Cherries: " + point.ToString();*/
            Debug.Log(point);
        }
    }
}
