using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveLine : MonoBehaviour
{
    private bool isMoving;  
    public Line line;
    public List<Transform> points;
    public int index = 0;

    private void Start()
    {
        isMoving = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving && index < points.Count)
        {
            MoveToPoint(index);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(0);
        }
    }
    
    private void MoveToPoint(int targetIndex)
    {
        isMoving = true;
        transform.DOMove(points[targetIndex].position, 1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                line.DelPoint(); 
                index++;  
                isMoving = false;
                if (index < points.Count)
                {
                    MoveToPoint(index);
                }
            });
    }
}
