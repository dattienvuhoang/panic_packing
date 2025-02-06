using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataMaker : MonoBehaviour
{
    public SpawnCar data;
    public List<GameObject> cars;
    public int indexLevel;
    public void SaveData()
    {
        data.levels[indexLevel].carInfo.Clear();    
        /*for (int i = 0; i < cars.Count; i++)
        {
            CarInfo carInfo = new CarInfo();
            data.levels[indexLevel].carInfo.Add(carInfo);
            Debug.Log( data.levels[indexLevel].carInfo.Count);
            data.levels[indexLevel].carInfo[i].carName = "Car " + (i + 1);  
            data.levels[indexLevel].carInfo[i].carPos = cars[i].transform.GetChild(0).transform.position; 
            data.levels[indexLevel].carInfo[i].points = new List<Vector3>();
            for (int j = 1; j < cars[i].transform.childCount; j++)
            {
                data.levels[indexLevel].carInfo[i].points.Add(cars[i].transform.GetChild(j).gameObject.transform.position);
                
            }
        }*/
        for (int i = 0; i < cars.Count; i++)
        {
            CarInfo carInfo = new CarInfo();
            data.levels[indexLevel].carInfo.Add(carInfo);
            Debug.Log( data.levels[indexLevel].carInfo.Count);
            data.levels[indexLevel].carInfo[i].carName = "Car " + (i + 1);  
            data.levels[indexLevel].carInfo[i].carPos = cars[i].transform.position; 
            data.levels[indexLevel].carInfo[i].points = new List<Vector3>();
           
            //data.levels[indexLevel].carInfo[i].diection = new List<Direction>();
            CarController carController = cars[i].GetComponent<CarController>();
            for (int j = 0; j < carController.listPos.Count; j++)
            {
                data.levels[indexLevel].carInfo[i].points.Add(carController.listPos[j].transform.position);
            }

            if (carController.barriers.Count > 0)
            {
                data.levels[indexLevel].carInfo[i].barriers = new List<Barrier>();
                data.levels[indexLevel].carInfo[i].barriers.Add(new Barrier());
                data.levels[indexLevel].carInfo[i].barriers[0].barrierPosition = carController.barriers[0].barrierPosition;
                data.levels[indexLevel].carInfo[i].barriers[0].barrierRotation = carController.barriers[0].barrierRotation;
            }
            
            
        }
        
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(DataMaker)), CanEditMultipleObjects]
public class UpdateRootBone_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DataMaker myScript = (DataMaker)target;
        if (GUILayout.Button("Save Data !"))
        {
            myScript.SaveData();
        }
    }
}
#endif
