using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> lights;
    public LayerMask layerCar;
    public GameObject car;
    public int indexLevel, coins;
    public bool isAccident;
    public float speedRun;
    private Camera cam;
    private GameObject[] _gameObjects;
    public int numberCar;
    private int currentCar;
    public bool isWin;
    public bool isSound, isMusic;
    private bool isUION;

    private UserData _userData;
     /*[Header("Audio")] public AudioSource AudioSource;
    public AudioClip soundCLip, musicClip;*/
    public static GameManager instance;
    private void Awake()
    {
        isAccident = false;
        instance = this;
        indexLevel = PlayerPrefs.GetInt("indexLevel");
        // Test sound 
        isMusic = true;
        isSound = true;
        isUION = false;
        LoadData();
        if (!PlayerPrefs.HasKey("Music"))
        {
            isMusic = true;
            PlayerPrefs.SetInt("Music", 1);
        }
        else
        {
            int music = PlayerPrefs.GetInt("Music");
            if (music == 1)
            {
                isMusic = true;
            }
            else
            {
                isMusic = false;
            }
                
        }
        if (!PlayerPrefs.HasKey("Sound"))
        {
            isSound = true;
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            int sound = PlayerPrefs.GetInt("Sound");
            if (sound == 1)
            {
                isSound = true;
            }
            else
            {
                isSound = false;
            }
                
        }

        if (!PlayerPrefs.HasKey("Coins"))
        {
            PlayerPrefs.SetInt("Coins", 0);
            coins = PlayerPrefs.GetInt("Coins");
        }
        else
        {
            coins = PlayerPrefs.GetInt("Coins");
        }
    }
    private void Start()
    {
        isWin = false;
        currentCar = 0;
        Application.targetFrameRate = 120;
        cam = Camera.main;
       _gameObjects = GameObject.FindGameObjectsWithTag("Light");
       foreach (var o in  _gameObjects)
       {
           lights.Add(o);
       }
       GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
       GameObject[] carRed = GameObject.FindGameObjectsWithTag("CarRed");
       GameObject[] carPurple = GameObject.FindGameObjectsWithTag("CarPurple");
       numberCar = cars.Length + carRed.Length + carPurple.Length;
       Debug.Log(numberCar);
    }
    private void Update()
    {
        Click();
        if (currentCar == numberCar && !isWin)
        {
            isWin = true;
            UIController.instance.ShowWin();    
        }
    }
    private void Click()
    {
        if (Input.GetMouseButtonDown(0) && !isAccident && !isUION)
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.forward, Mathf.Infinity, layerCar);
            if (hit.collider != null)
            {
                car = hit.collider.gameObject;
                TagGameObject tag = car.GetComponent<TagGameObject>();
                if (tag != null && tag.tagValue == "Car")
                {
                    // ----------- di chuyen 1 xe ------------
                    /*CarController carController = car.GetComponent<CarController>();
                    carController.isMove = true;
                    carController.type = TypeAI.PLAYER;*/
                    
                    // di chuyen nhieu xe
                    CarController carController = car.GetComponent<CarController>();
                    ColorCar color = carController.color;
                    switch (color)
                    {
                        case ColorCar.YELLOW:
                        {
                            carController.isMove = true;
                            carController.type = TypeAI.PLAYER;
                            break;
                        }
                        case ColorCar.RED:
                        {
                            Debug.Log("Car is red");
                            GameObject[] listCar = GameObject.FindGameObjectsWithTag("CarRed");
                            for (int i = 0; i < listCar.Length; i++)
                            {
                                Debug.Log("Run car" + listCar[i].name);
                                CarController controller = listCar[i].GetComponent<CarController>();
                                controller.type = TypeAI.PLAYER;
                                controller.isMove = true;
                            }
                            break;
                        }
                        case ColorCar.PURPLE:
                        {
                            Debug.Log("Car is purple");
                            GameObject[] listCar = GameObject.FindGameObjectsWithTag("CarPurple");
                            for (int i = 0; i < listCar.Length; i++)
                            {
                                CarController controller = listCar[i].GetComponent<CarController>();
                                controller.type = TypeAI.PLAYER;
                                controller.isMove = true;
                            }
                            break;
                        }

                    }
                }
            }
        }
    }
    public void NextLevel()
    {
        if (indexLevel < GlobalData.MAXLEVEL - 1)
        {
            GameManager.instance.SetUION(false);
            Debug.Log("Next Level");
            indexLevel++;
            PlayerPrefs.SetInt("indexLevel", indexLevel);
            SceneManager.LoadScene("GamePlay");
        }
        else
        {
            Debug.Log("Max Level");
        }

    }
    public void BackLevel()
    {
        if (indexLevel > 0)
        {
            Debug.Log("Back Level");
            indexLevel--;
            PlayerPrefs.SetInt("indexLevel", indexLevel);
            SceneManager.LoadScene("GamePlay");
        }
        else
        {
            Debug.Log("Level 0");
        }
    }

    public void setIsAccident(bool set)
    {
        isAccident = set;    
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = cam.transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            cam.transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null; 
        }

        cam.transform.localPosition = originalPosition;
    }

    public void ShakeCam(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    public void ChangeLight()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].GetComponent<LightController>().ChangeLight();    
        }
    }

    public void UpNumberCar()
    {
        currentCar++;
        Debug.Log("Current car "  + currentCar);
    }

    public void SetUION(bool set)
    {
        isUION = set;
    }

    public void ChangeCoin(int coin)
    {
        coins -= coin;
    }

    public UserData LoadData()
    {
        if (PlayerPrefs.HasKey("UserData"))
        {
            string json = PlayerPrefs.GetString("PlayerData");
            UserData data = JsonUtility.FromJson<UserData>(json);
            Debug.Log("Data loaded from PlayerPrefs: " + json);
            return data;
        }
        else
        {
           Debug.LogError("Ngu");
           _userData = new UserData();
           _userData.indexLevel = 0;
           _userData.coin = 0;
           _userData.indexBG = 0;
           _userData.indexSkin = 0;
           _userData.isMusic = true;
           _userData.isSound = true;
           for (int i = 0; i < _userData.background.Count; i++)
           {
               _userData.background[i] = 0;
           }
           for (int i = 0; i < _userData.skin.Count(); i++)
           {
               _userData.skin[i] = 0;
           }
           SaveData(_userData);
           return _userData;
        }
    }
    public void SaveData(UserData data)
    {
        string json = JsonUtility.ToJson(data, true);
        Debug.Log(json);
        PlayerPrefs.SetString("PlayerData", json);
        PlayerPrefs.Save();
    }
    
}
