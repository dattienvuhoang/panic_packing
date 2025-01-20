using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitScene : MonoBehaviour
{
    Camera cam;
    private void Awake()
    {
        cam = Camera.main;
    }
    private void Start()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        float aspectRatio = (float)screenHeight / screenWidth;
        float defaultSize = 16f / 9f;
        float size = (5 / defaultSize) * aspectRatio;
        if (aspectRatio > 1.5f)
        {
            cam.orthographicSize = size;
        }
        else
        {
            cam.orthographicSize = 4.55f;
        }
    }
}
