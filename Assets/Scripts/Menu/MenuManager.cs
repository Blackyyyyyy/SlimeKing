using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager current;

    public GameObject activeMenu
    {
        get { return activeMenuStorage; }
        set
        {
            gotoMenu(value);
        }
    }
    private GameObject activeMenuStorage;

    private GameObject[] allMenus;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        allMenus = new GameObject[transform.childCount];
        Settings.allAudioSources.Add(GetComponent<AudioSource>());

        for (int i = 0; i < transform.childCount; i++)
        {
            allMenus[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject menu in allMenus) menu.SetActive(false);

        gotoMenu(0);
    }

    public void gotoMenu(GameObject menu)
    {
        activeMenu.SetActive(false);
        activeMenuStorage = menu;
        activeMenu.SetActive(true);
    }

    public void gotoMenu(int index)
    {
        if (activeMenu != null) activeMenu.SetActive(false);
        activeMenuStorage = allMenus[index];
        if (activeMenu != null) activeMenu.SetActive(true);
    }

    public void playButtonSound()
    {
        GetComponent<AudioSource>().Play();
    }
}
