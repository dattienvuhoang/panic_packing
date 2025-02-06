using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    LEFT, RIGHT
};
public enum TypeAI
{
    PLAYER, BOT
};

public enum ColorCar
{
    YELLOW,
    RED,
    PURPLE
};

public class CarController : MonoBehaviour
{
    public GameObject carFake;
    public TypeAI type;
    public ColorCar color;
    public List<Transform> listPos;
    //public List<Vector3> listPos;
    public List<Direction> listDirection;
    public Line line;
    public bool isMove = false, isRotate = true, isStop = false, isLeft;
    public int index, count;
    public Vector3 direct;
    public Coroutine stop;
    public bool isLight = false, isBarrier = false;
    GameObject barrier; 
    public List<Barrier> barriers;
    Tween carCurrentTween, carFakeCurrentTwwen, rotateTween ;
    private void Start()
    {
        count = listPos.Count;
        index = 0;
    }
    private void Update()
    {
        if (isMove)
        {
            if (isBarrier == true)
            {
                barrier.SetActive(false);
                isBarrier = false;
            }
            if (index < count)
            {
                isMove = false;
                /*carCurrentTween =  transform.DOMove(listPos[index].position, .7f).SetEase(Ease.Linear);
                carFakeCurrentTwwen =  carFake.transform.DOMove(listPos[index].position, 0.6f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    line.DelPoint();
                    if (line.points.Count == 1)
                    {
                        type = TypeAI.BOT;
                    }
                });*/
                 
                // Tinh thoi gian de van toc bang nhau 
                float timeCar = Vector3.Distance(transform.position, listPos[index].position) / GameManager.instance.speedRun;
                //Debug.Log("Time Car: " + timeCar);
                carCurrentTween = transform.DOMove(listPos[index].position, timeCar).SetEase(Ease.Linear);
                /*carFakeCurrentTwwen = carFake.transform.DOMove(listPos[index].position, timeCar-0.1f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    line.DelPoint();
                    if (line.points.Count == 1)
                    {
                        type = TypeAI.BOT;
                        if (!isLight)
                        {
                            GameManager.instance.ChangeLight();
                        }
                    }
                });*/ 
                carFakeCurrentTwwen = carFake.transform.DOMove(listPos[index].position, timeCar).SetEase(Ease.Linear).OnComplete(() =>
                {
                    line.DelPoint();
                    if (line.points.Count == 1)
                    {
                        type = TypeAI.BOT;
                        if (!isLight)
                        {
                            GameManager.instance.ChangeLight();
                        }
                    }
                });
            }
            else
            {
                return;
            }
        }
        if (listPos.Count != 0)
        {
            if (Vector3.Distance(transform.position, listPos[index].position) < 0.1f)
            {
                if (index < listDirection.Count)
                {
                    index++;
                    isMove = true;
                    {
                        if (isRotate)
                        {
                            if (listDirection[index - 1] == Direction.LEFT)
                            {
                                var rotation = transform.eulerAngles + new Vector3(0, 0, 90);
                                rotateTween = transform.DORotate(rotation, 0.1f, RotateMode.FastBeyond360);
                            }
                            else
                            {
                                var rotation = transform.eulerAngles + new Vector3(0, 0, -90);
                                rotateTween = transform.DORotate(rotation, 0.1f, RotateMode.FastBeyond360);
                            }
                        }
                    }
                }
                else
                {
                    isRotate = false;
                }

            }
        }
    }

    public void Move()
    {
        isMove = true;
        if (isBarrier == true)
        {
            barrier.SetActive(false);
            isBarrier = false;
        }
    }
    /*public void Rotate()
    {
        if (isRotate)
        {
            line.DelPoint();
            if (listDirection[index - 1] == Direction.LEFT)
            {
                isLeft = true;
                var rotation = transform.eulerAngles + new Vector3(0, 0, 90);
                transform.DORotate(rotation, 0.01f, RotateMode.FastBeyond360);
            }
            else
            {
                isLeft = false;
                var rotation = transform.eulerAngles + new Vector3(0, 0, -90);
                transform.DORotate(rotation, 0.01f, RotateMode.FastBeyond360);

            }
        }
    }*/
    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponent<TagGameObject>() != null)
        {
            Debug.Log("Hit");
            TagGameObject tag = collision.gameObject.GetComponent<TagGameObject>();
            if (tag.tagValue == "Car")
            {
                Debug.Log("Car Hit");
                direct = transform.position - collision.gameObject.transform.position;
                if (collision.transform.GetComponent<CarController>().isLeft)
                {
                    transform.DORotate(new Vector3(0, 0, 15), 0.1f);
                }
                else
                {
                    transform.DORotate(new Vector3(0, 0, -15), 0.1f);
                }
                AddForce();
            }

            if (tag.tagValue == "Light")
            {
                GameObject light = collision.gameObject;
                LightController lightController = light.GetComponent<LightController>();
                if (lightController.type == TypeLight.GREEN)
                {
                    Debug.Log("Light is Green");
                    
                }
                else if (lightController.type == TypeLight.RED)
                {
                    Debug.Log("Light is Red");
                    GameManager.instance.ShakeCam(0.2f,0.1f);
                }
            }

            if (tag.tagValue == "Barrier")
            {
                Debug.Log("Barrier Hit ");
                barrier = collision.gameObject;
                Debug.Log(barrier.name);
                carCurrentTween.Kill();
                carFakeCurrentTwwen.Kill();
                //rotateTween.Kill();
                isMove = false;
                isBarrier = true;
            }
        }
        if (type == TypeAI.BOT && !isBarrier)
        {
            GameManager.instance.ShakeCam(0.2f,0.1f);
            Debug.Log("Shake car");
        }
        else if (type == TypeAI.PLAYER && !isBarrier)
        {
            carFakeCurrentTwwen.Kill();
            carCurrentTween.Kill();
            rotateTween.Kill();
            isRotate = false;
            GameManager.instance.setIsAccident(true);
            GameManager.instance.ShakeCam(0.2f,0.1f);
        }

    }
    public void AddForce()
    {
        transform.DOMove(transform.position + direct * 0.2f, 0.15f);
    }

   
}
