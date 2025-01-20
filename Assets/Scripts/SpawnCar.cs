using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnCar", order = 1)]


public class SpawnCar : ScriptableObject
{
    public List<Level> levels;
}

[Serializable]
public class Level
{
    public List<CarInfo> carInfo;
}

[Serializable]
public class CarInfo
{
    public string carName;
    public GameObject car, carFake, line;
    public Vector3 carPos;
    public Vector3 rotation, rotationCarLot;
    public List<Vector3> points;
    public List<Direction> diection;
}
