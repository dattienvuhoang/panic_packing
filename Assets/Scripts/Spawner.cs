using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spawner : MonoBehaviour
{
    public int indexLevel;
    public SpawnCar spawnCarData;
    public GameObject carPrefab, carFakePrefab, linePrefab;
    public GameObject emptyObject;
    public Sprite spCarLot;
    private void Awake()
    {
        
    }
    private void Start()
    {
        indexLevel = GameManager.instance.indexLevel;
        Debug.Log(indexLevel);
        SpawnerObject(indexLevel);
    }
    private void Update()
    {
       
    }
    public void SpawnerObject(int indexLevel)
    {
        for (int i = 0; i < spawnCarData.levels[indexLevel].carInfo.Count; i++)
        {
            spawnCarData.levels[indexLevel].carInfo[i].car = carPrefab; 
            spawnCarData.levels[indexLevel].carInfo[i].carFake = carFakePrefab;
            spawnCarData.levels[indexLevel].carInfo[i].line = linePrefab;
            

            // Instantiate cac object
            GameObject car = Instantiate(spawnCarData.levels[indexLevel].carInfo[i].car, spawnCarData.levels[indexLevel].carInfo[i].carPos, Quaternion.identity);
            GameObject carFake = Instantiate(spawnCarData.levels[indexLevel].carInfo[i].carFake, spawnCarData.levels[indexLevel].carInfo[i].carPos, Quaternion.identity);
            GameObject line = Instantiate(spawnCarData.levels[indexLevel].carInfo[i].line, spawnCarData.levels[indexLevel].carInfo[i].carPos, Quaternion.identity);
            //  
            car.name = spawnCarData.levels[indexLevel].carInfo[i].carName;
            carFake.name = spawnCarData.levels[indexLevel].carInfo[i].carName + "Fake";
            // 
            CarController carController = car.GetComponent<CarController>();
            carController.carFake = carFake;
            //carController.listPos = spawnCar[indexLevel].carInfo[i].points;

            Line lineCar = carFake.GetComponent<Line>();
            lineCar.lineController = line.GetComponent<LineController>();
            lineCar.points.Add(carFake.transform);

            carController.line = lineCar;
            carController.listDirection = spawnCarData.levels[indexLevel].carInfo[i].diection;


            car.transform.eulerAngles = spawnCarData.levels[indexLevel].carInfo[i].rotation;
            for (int j = 0; j < spawnCarData.levels[indexLevel].carInfo[i].points.Count; j++)
            {
                GameObject pos = Instantiate(emptyObject, spawnCarData.levels[indexLevel].carInfo[i].points[j], Quaternion.identity);
                if (j == spawnCarData.levels[indexLevel].carInfo[i].points.Count-1)
                {
                    pos.AddComponent<SpriteRenderer>();
                    SpriteRenderer sp = pos.GetComponent<SpriteRenderer>();
                    sp.sprite = spCarLot;
                    sp.sortingOrder = 3;
                    pos.transform.eulerAngles = spawnCarData.levels[indexLevel].carInfo[i].rotationCarLot;
                }
                pos.name = "Pos";
                lineCar.points.Add(pos.transform);
                carController.listPos.Add(pos.transform);
            }

        }

    }
}
