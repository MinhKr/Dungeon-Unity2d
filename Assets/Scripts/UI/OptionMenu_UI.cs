using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu_UI : MonoBehaviour
{
    [SerializeField] private int skin_id;
    [SerializeField] private Animator anim;
    
    public void Next()
    {
        skin_id++;
        if(skin_id > 3)
            skin_id = 0;

        anim.SetInteger("SkinId", skin_id);
    }

    public void Previous()
    {
        skin_id--;
        if (skin_id < 0)
            skin_id = 3;

        anim.SetInteger("SkinId", skin_id);
    }

    public void Equip()
    {
        PlayerManager.instance.choosenSkinId = skin_id;
    }
}
