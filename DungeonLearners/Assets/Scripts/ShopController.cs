using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject items, avatar;

    public void CloseWindow()
    {
        gameObject.SetActive(false);
        //player.GetComponent<PlayerMovementController>().enableMove = true;
    }

    public void ShowItems()
    {
        items.SetActive(true);
        avatar.SetActive(false);
    }

    public void ShowAvatar()
    {
        items.SetActive(false);
        avatar.SetActive(true);
    }
}
