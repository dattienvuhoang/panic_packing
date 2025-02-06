using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeController : MonoBehaviour
{
    public RectTransform rectNameGame;
    public Button btnPlay;
    private Tween tween;
    private void Start()
    {
        ScaleNameGame();
        btnPlay.onClick.AddListener(()=> PlayGame());
    }

    public void ScaleNameGame()
    {
        tween =  rectNameGame.DOScale(1.1f, 0.5f).OnComplete(() =>
        {
            rectNameGame.DOScale(1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        });
    }

    public void PlayGame()
    {
        rectNameGame.transform.DOKill();
        SceneManager.LoadScene("GamePlay");
    }
    
}
