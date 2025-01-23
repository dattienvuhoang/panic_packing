using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TypeLight
{
    RED,
    GREEN
};
public class LightController : MonoBehaviour
{
    public TypeLight type;
    public BoxCollider2D box;

    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (type == TypeLight.RED)
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(true);
            box.isTrigger = false;
        }
        else if (type == TypeLight.GREEN)
        {
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);
            box.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            TagGameObject tag = collision.gameObject.GetComponent<TagGameObject>();
            if (tag.tagValue == "Car")
            {
              
                GameManager.instance.ChangeLight();
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            TagGameObject tag = collision.gameObject.GetComponent<TagGameObject>();
            if (tag.tagValue == "Car")
            {
                if (type == TypeLight.GREEN)
                {
                    this.gameObject.SetActive(false);
                }
            }
        }
    }

    public void ChangeLight()
    {
        if (type == TypeLight.GREEN)
        {
            type = TypeLight.RED;
        }
        else if (type == TypeLight.RED)
        {
            type = TypeLight.GREEN;
        }
    }
}
