using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScHud : MonoBehaviour
{
    [SerializeField] GameObject MenuPausa;
    [SerializeField] ScEntity _entity;
    [SerializeField] private Slider HpBar;

    public void TogglePause()
    {
        if (MenuPausa.activeSelf)
        {
            MenuPausa.SetActive(false);
            Time.timeScale = 1.0f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            MenuPausa.SetActive(true);
            Time.timeScale = 0.0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level_MilitaryBase");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void CountHP()
    {
        HpBar.value = _entity.health / _entity.Stats.maxHealth;
    }
}
