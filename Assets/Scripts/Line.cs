using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public List<Transform> points;
    //public List<Vector3> points;
    public LineController lineController;

    private void Start()
    {
        lineController.SetUpLine(points);
    }
    public void DelPoint()
    {
        points.Remove(points[1]);
    }
}
