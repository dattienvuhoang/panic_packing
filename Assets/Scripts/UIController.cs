using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button btnNextLevel, btnBackLevel , btnReplay, btnNewGame;
    public TMP_Text txtIndexLevel;

    private void Start()
    {
        btnNewGame.onClick.AddListener(() =>
        {
            NewGame();
        });
        btnReplay.onClick.AddListener(()=> Replay());
        btnNextLevel.onClick.AddListener(() => GameManager.instance.NextLevel());
        btnBackLevel.onClick.AddListener(()=> GameManager.instance.BackLevel());
        int level = GameManager.instance.indexLevel + 1;
        txtIndexLevel.text = txtIndexLevel.text + level;
    }
    public void Replay()
    {
        SceneManager.LoadScene(0);
    }
    public void NewGame()
    {
        GameManager.instance.indexLevel = 0;
        PlayerPrefs.SetInt("indexLevel", 0);
        Replay();
    }
}
