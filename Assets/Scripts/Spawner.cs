using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spawner : MonoBehaviour
{
    public int indexLevel;
    public SpawnCar spawnCarData;
    public GameObject carPrefab, carFakePrefab, linePrefab, lightPrefab, barrierPrefab;
    public GameObject emptyObject;
    public Sprite spCarLot, spCarRed, spCarPurple;
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
            spawnCarData.levels[indexLevel].carInfo[i].barrier = barrierPrefab;

            // Instantiate cac object
            GameObject car = Instantiate(spawnCarData.levels[indexLevel].carInfo[i].car, spawnCarData.levels[indexLevel].carInfo[i].carPos, Quaternion.identity);
            GameObject carFake = Instantiate(spawnCarData.levels[indexLevel].carInfo[i].carFake, spawnCarData.levels[indexLevel].carInfo[i].carPos, Quaternion.identity);
            if (spawnCarData.levels[indexLevel].carInfo[i].barriers.Count != 0)
            {
                GameObject barrier = Instantiate(spawnCarData.levels[indexLevel].carInfo[i].barrier,spawnCarData.levels[indexLevel].carInfo[i].barriers[0].barrierPosition,Quaternion.identity);
                barrier.transform.eulerAngles = spawnCarData.levels[indexLevel].carInfo[i].barriers[0].barrierRotation;
            }
            GameObject line = Instantiate(spawnCarData.levels[indexLevel].carInfo[i].line, spawnCarData.levels[indexLevel].carInfo[i].carPos, Quaternion.identity);
            if (spawnCarData.levels[indexLevel].carInfo[i].lightPos.Count >0 )
            {
                GameObject light = Instantiate(lightPrefab, spawnCarData.levels[indexLevel].carInfo[i].lightPos[0], Quaternion.identity);
                LightController lightController = light.GetComponent<LightController>();
                lightController.type = spawnCarData.levels[indexLevel].carInfo[i].typeLight;
            }
            //  
            car.name = spawnCarData.levels[indexLevel].carInfo[i].carName;
            
            carFake.name = spawnCarData.levels[indexLevel].carInfo[i].carName + "Fake";
            // 
            CarController carController = car.GetComponent<CarController>();
            carController.carFake = carFake;
            carController.color = spawnCarData.levels[indexLevel].carInfo[i].color;
            switch (carController.color)
            {
                case ColorCar.RED:
                {
                    car.tag = "CarRed";
                    car.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sprite = spCarRed;
                    break;
                }
                case ColorCar.PURPLE:
                {
                    car.tag = "CarPurple";
                    car.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().sprite = spCarPurple;
                    break;
                }
            }
            //carController.listPos = spawnCar[indexLevel].carInfo[i].points;

            Line lineCar = carFake.GetComponent<Line>();
            lineCar.lineController = line.GetComponent<LineController>();
            lineCar.points.Add(carFake.transform);

            carController.line = lineCar;
            carController.listDirection = spawnCarData.levels[indexLevel].carInfo[i].diection;
            if (spawnCarData.levels[indexLevel].carInfo[i].lightPos.Count > 0)
            {
                carController.isLight = true;
            }

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
