using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;


public enum FruitType
{
    apple , banana , cherry , kiwi
}
public class Fruit_Item : MonoBehaviour
{
    private Animator anim;
    public FruitType myFruitType;

    [SerializeField] private TMP_Text textPoint;
    [SerializeField] private TMP_Text besScore;

    [SerializeField] private SpriteRenderer sr;
                     public Sprite[] myFruitSprite;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
  
        //Đưa weight của tất cả layer về 0 
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }
        
        //Đặt Weight của Layer được chọn thành 1 để chạy animation và hình ảnh
        anim.SetLayerWeight((int)myFruitType, 1);
    }

    private void OnValidate()
    {
        sr.sprite = myFruitSprite[(int)myFruitType];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlaySFX(3);
            PlayerManager.instance.fruits++;
            textPoint.text = "x" + PlayerManager.instance.fruits;
            /*PlayerPrefs.SetFloat("fruitPoints", collision.GetComponent<PlayerManager>().fruits);*/
            Destroy(gameObject);
            /*besScore.text = "+" + PlayerPrefs.GetFloat("fruitPoints"); */     
        }
    }
}
