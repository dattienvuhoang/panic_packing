using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    /*public Button btnNextLevel, btnBackLevel , btnReplay, btnNewGame;
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
        SceneManager.LoadScene("GamePlay");
    }
    public void NewGame()
    {
        GameManager.instance.indexLevel = 0;
        PlayerPrefs.SetInt("indexLevel", 0);
        Replay();
    }*/
    public GameObject pnShop;
    [Header("UI Setting")] public Button btnSetting;
    public Button btnReplay, btnCloseSetting, btnShop, btnSound, btnMusic, btnCloseShop;
    public GameObject pnSetting;
    public RectTransform rectSetting;
    public Sprite spSoundOn, spSoundOff, spMusicOn, spMusicOff;
    [Header("UI Win")] 
    public GameObject pnWin;
    public Button btnNextLevel, btnOpenShop;
    public CanvasGroup cvWin, cvCoin;
    public RectTransform rectWin, rectCoin;
    
    [Header("UI Lose")]
    public CanvasGroup cvLose;
    public RectTransform rectLose;
    public Button btnReplayLose; 
    public static UIController instance;

    [Header("UI Shop")] public Button btnBackground;
    public Button btnSkins, btnNoAds;
    public GameObject pnBackground, pnSkin, pnNoAds;
    public TMP_Text txtCoin;
    public int coin;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        coin = GameManager.instance.coins;
        txtCoin.text = coin.ToString();
        bool music = GameManager.instance.isMusic;
        if (music)
        {
            btnMusic.GetComponent<Image>().sprite = spSoundOn;
        }
        else
        {
            btnMusic.GetComponent<Image>().sprite = spSoundOff;
        }
        bool sound = GameManager.instance.isSound;
        if (sound)
        {
            btnSound.GetComponent<Image>().sprite = spSoundOn;
        }
        else
        {
            btnSound.GetComponent<Image>().sprite = spSoundOff;
        }
        
        // Setting
        btnReplay.onClick.AddListener(() => Replay());
        btnSetting.onClick.AddListener(()=> OpenSetting());
        btnCloseSetting.onClick.AddListener(()=> CloseSetting());
        btnSound.onClick.AddListener(SettingSound);
        btnMusic.onClick.AddListener(SettingMusic);
        btnShop.onClick.AddListener(OpenShop);
        btnOpenShop.onClick.AddListener(() => OpenShop());
        btnCloseShop.onClick.AddListener(() => CloseShop());
        // Win 
        btnNextLevel.onClick.AddListener(()=> GameManager.instance.NextLevel());
        // Lose 
        btnReplayLose.onClick.AddListener(()=> Replay());
        
        MenuShop(); 
    }
    public void Replay()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void OpenSetting()
    {
        GameManager.instance.SetUION(true);
        pnSetting.SetActive(true);
        rectSetting.DOAnchorPosY(0 , 0.5f).SetEase(Ease.OutBack);
    }

    public void CloseSetting()
    {
        rectSetting.DOAnchorPosY(2000, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            pnSetting.SetActive(false);
        });
    }

    public void ShowWin()
    {
        coin += 20;
        PlayerPrefs.SetInt("Coins", coin);
        txtCoin.text = coin.ToString();
        GameManager.instance.SetUION(true);
        pnWin.SetActive(true);
        rectWin.DOScale(1,0.5f).SetEase(Ease.InBack);
        cvWin.DOFade(1, 0.5f).OnComplete(() =>
        {
            rectCoin.DOAnchorPosY(-250, 0.5f);
            cvCoin.DOFade(0, 0.5f).OnComplete(() =>
            {
                rectCoin.DOAnchorPosY(-320, 0.25f);
            });
        });
    }

    public void ShowLose()
    {
        GameManager.instance.SetUION(true);
        pnWin.SetActive(true);
        rectLose.DOScale(1,0.5f).SetEase(Ease.InBack);
        cvLose.DOFade(1, 0.5f);
    }

    public void SettingSound()
    {
        Debug.Log("111");
        bool isSound = GameManager.instance.isSound;
        if (isSound)
        {
            Debug.Log("222");   
            GameManager.instance.isSound = false;
            //isSound = false;
            btnSound.GetComponent<Image>().sprite = spSoundOff;
        }
        else
        {
            Debug.Log("333");
            GameManager.instance.isSound = true;

            btnSound.GetComponent<Image>().sprite = spSoundOn;
  
        }
    }

    public void SettingMusic()
    {
        bool isMusic = GameManager.instance.isMusic;
        if (isMusic)
        {
            Debug.Log("222");   
            GameManager.instance.isMusic = false;
            PlayerPrefs.SetInt("Music", 0);
            //isSound = false;
            btnMusic.GetComponent<Image>().sprite = spSoundOff;
            AudioManager.instance.StopMusic();
        }
        else
        {
            Debug.Log("333");
            GameManager.instance.isMusic = true;
            btnMusic.GetComponent<Image>().sprite = spSoundOn;
            PlayerPrefs.SetInt("Music", 1);
            AudioManager.instance.PlayMusic();

        }
    }

    public void OpenShop()
    {
        GameManager.instance.SetUION(true);
        pnShop.SetActive(true);
        pnShop.GetComponent<CanvasGroup>().DOFade(1, 0.25f);
        pnShop.GetComponent<RectTransform>().DOScale(1, 0.25f);
    }

    public void CloseShop()
    {
        GameManager.instance.SetUION(false);

        pnShop.GetComponent<RectTransform>().DOScale(0, 0.25f).OnComplete(() =>
        {
            pnShop.SetActive(false);
        });
    }

    public void OpenShopBackground()
    {
        pnBackground.SetActive(true);
        pnSkin.SetActive(false);
        pnNoAds.SetActive(false);   
    }

    public void OpenShopSkins()
    {
        pnBackground.SetActive(false);
        pnSkin.SetActive(true);
        pnNoAds.SetActive(false);   
    }

    public void OpenNoAds()
    {
        pnBackground.SetActive(false);
        pnSkin.SetActive(false);
        pnNoAds.SetActive(true);   
    }
    public void MenuShop()
    {
        btnBackground.onClick.AddListener(OpenShopBackground);
        btnSkins.onClick.AddListener(OpenShopSkins);    
        btnNoAds.onClick.AddListener(OpenNoAds);
    }
}
