using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeController : MonoBehaviour
{
    public RectTransform rectNameGame;
    public Button btnPlay;
    public Slider slider;
    public TMP_Text txtSlider;
    public GameObject sliderObject;
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
        //SceneManager.LoadScene("GamePlay");
        LoadingScene(); 
    }

    public void LoadingScene()
    {
        btnPlay.gameObject.SetActive(false);
        sliderObject.SetActive(true);
        slider.DOValue(.99f, 1).OnUpdate(()=>
        {
            txtSlider.text = Math.Round((slider.value*100)).ToString() + "%";
        }). OnComplete(() =>
        {
            SceneManager.LoadScene("GamePlay");
        });
    }

    
}
