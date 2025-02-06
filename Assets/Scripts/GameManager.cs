using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> lights;
    
    public LayerMask layerCar;
    public GameObject car;
    public int indexLevel;
    public bool isAccident;
    public float speedRun;
    private Camera cam;
    private GameObject[] _gameObjects;
    public static GameManager instance;
    private void Awake()
    {
        isAccident = false;
        instance = this;
        indexLevel = PlayerPrefs.GetInt("indexLevel");
    }
    private void Start()
    {
        Application.targetFrameRate = 120;
        cam = Camera.main;
       _gameObjects = GameObject.FindGameObjectsWithTag("Light");
       foreach (var o in  _gameObjects)
       {
           lights.Add(o);
       }
    }
    private void Update()
    {
        Click();
    }
    private void Click()
    {
        if (Input.GetMouseButtonDown(0) && !isAccident)
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
}
