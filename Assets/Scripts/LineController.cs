using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LineController : MonoBehaviour
{
    public List<Transform> points;
    //public List<Vector3> points;
    public LineRenderer lr;

    public void SetUpLine(List<Transform> points)
    {
        lr.positionCount = points.Count;
        this.points = points;
    }
    private void Update()
    {
        for (int i = 0; i < points.Count; i++)
        {   
            lr.SetPosition(i, points[i].position);
            //lr.SetPosition(i, points[i]);
        }
    }
    
}
