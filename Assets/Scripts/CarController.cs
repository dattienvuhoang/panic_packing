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
public class CarController : MonoBehaviour
{
    public GameObject carFake;
    public TypeAI type;
    public List<Transform> listPos;
    //public List<Vector3> listPos;
    public List<Direction> listDirection;
    public Line line;
    public bool isMove = false, isRotate = true, isStop = false, isLeft;
    public int index, count;
    public Vector3 direct;
    public Coroutine stop;
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
                Debug.Log("Time Car: " + timeCar);
                carCurrentTween = transform.DOMove(listPos[index].position, timeCar).SetEase(Ease.Linear);
                carFakeCurrentTwwen = carFake.transform.DOMove(listPos[index].position, timeCar-0.1f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    line.DelPoint();
                    if (line.points.Count == 1)
                    {
                        type = TypeAI.BOT;
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
            //if (Vector3.Distance(transform.position, listPos[index].transform.position) < 0.1f)
            if (Vector3.Distance(transform.position, listPos[index].position) < 0.1f)
            {
                if (index < listDirection.Count)
                {
                    //transform.position = listPos[index].transform.position;
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

    IEnumerator DeleyDelPoint()
    {
        yield return new WaitForSeconds(0.05f);
        line.DelPoint();
    }

    public void Move()
    {
        isMove = true;
    }
    public void Rotate()
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
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponent<TagGameObject>() != null)
        {
            TagGameObject tag = collision.gameObject.GetComponent<TagGameObject>();
            if (tag.tagValue == "Car")
            {
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
        }
        if (type == TypeAI.BOT)
        {
           
        }
        else if (type == TypeAI.PLAYER)
        {
            carCurrentTween.Kill();
            carFakeCurrentTwwen.Kill();
            rotateTween.Kill();
           /// transform.DOKill();
           // carFake.transform.DOKill();
            isRotate = false;
            GameManager.instance.setIsAccident(true);
        }

    }
    public void AddForce()
    {
        transform.DOMove(transform.position + direct * 0.2f, 0.15f);
    }

}
