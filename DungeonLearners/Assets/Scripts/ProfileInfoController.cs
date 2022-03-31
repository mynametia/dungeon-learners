using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the player profile information
/// </summary>
public class ProfileInfoController : MonoBehaviour
{
    public GameObject items, avatar, worlds, account;

    public void CloseWindow()
    {
        gameObject.SetActive(false);
        //player.GetComponent<PlayerMovementController>().enableMove = true;
    }

    public void ShowItems()
    {
        items.SetActive(true);
        avatar.SetActive(false);
        worlds.SetActive(false);
        account.SetActive(false);
    }

    public void ShowAvatar()
    {
        items.SetActive(false);
        avatar.SetActive(true);
        worlds.SetActive(false);
        account.SetActive(false);
    }

    public void ShowWorlds()
    {
        items.SetActive(false);
        avatar.SetActive(false);
        worlds.SetActive(true);
        account.SetActive(false);
    }

    public void ShowAccount()
    {
        items.SetActive(false);
        avatar.SetActive(false);
        worlds.SetActive(false);
        account.SetActive(true);
    }
}
