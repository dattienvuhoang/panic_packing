using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ToolCreateScene : MonoBehaviour
{
    public GameObject carPrefab,posPrefab, carFakePrefab, linePrefab, lightPrefab, barrierPrefab , car ;
    public List<Vector3> pos;
    public DataMaker data;
    private Camera cam;
    private bool isCreate;
    private void Start()
    {
        isCreate = false;
        cam = Camera.main;
    }

    private void Update()
    {
        Vector2 mousPos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Create Car");
            isCreate = true;
            pos = new List<Vector3>();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Stop Create Car");
            isCreate = false;
            
            data.cars.Add(car);
            CreateCarFake();
            car = null;
        }

        if (isCreate)
        {
           
            if (Input.GetKey(KeyCode.A) && Input.GetMouseButtonDown(0))
            {
                if (car == null)
                {
                    GameObject o = Instantiate(carPrefab, mousPos, Quaternion.identity);
                    car = o;
                    
                }
            }

            if (Input.GetKey(KeyCode.P) && Input.GetMouseButtonDown(0))
            {
                pos.Add(mousPos);   
                GameObject point = Instantiate(posPrefab, mousPos, Quaternion.identity);
                car.GetComponent<CarController>().listPos.Add(point.transform);
            }
            if (Input.GetKey(KeyCode.B) && Input.GetMouseButtonDown(0))
            {
                //pos.Add(mousPos);   
                GameObject o = Instantiate(barrierPrefab, mousPos, Quaternion.identity);
                //car.GetComponent<CarController>().listPos.Add(point.transform);
                CarController carController =  car.GetComponent<CarController>();
                carController.barriers = new List<Barrier>();
                Barrier barr = new Barrier();
                carController.barriers.Add(barr);
                carController.barriers[0].barrierPosition = o.transform.position;
                carController.barriers[0].barrierRotation = o.transform.eulerAngles;
            }
        }
    }

    public void CreateCarFake()
    {
        GameObject carFake = Instantiate(carFakePrefab, car.transform.position, Quaternion.identity);
        GameObject lineObject = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        Line line = carFake.GetComponent<Line>();
        line.lineController = lineObject.GetComponent<LineController>();
        line.points.Add(carFake.transform);
        CarController carController = car.GetComponent<CarController>();
        for (int i = 0; i < carController.listPos.Count; i++)
        {
            line.points.Add(carController.listPos[i]);
        }
    }
}
