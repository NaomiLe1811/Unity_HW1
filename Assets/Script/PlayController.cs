using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetPoint
{
    topLeft,
    topRight,
    bottomRight,
    bottomLeft,
}

public class PlayerController1 : MonoBehaviour
{
    [SerializeField]
    public int mySpeed = 10;

    //gan trang thai dau tien
    private TargetPoint nextPoint = TargetPoint.topLeft;

    //khai bao vi tri cua 4 diem
    public Transform topLeftTransform;
    public Transform topRightTransform;
    public Transform bottomRightTransform;
    public Transform bottomLeftTransform;

    //khai bao vi tri hien tai
    private Transform currentPoint;

    // Start is called before the first frame update
    void Start()
    {
        currentPoint = topLeftTransform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPoint = currentPoint.position;
        Vector3 moveDirection = targetPoint - transform.position;

        float distance = moveDirection.magnitude;

        if (distance > 0.1f)
        {
            //chua toi thi di chuyen tiep
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, mySpeed * Time.deltaTime);
        }
        else
        {
            //chuyen sang vi tri moi
            SetNextTarget(nextPoint);

        }

        //thay doi goc quay theo huong target
        //cong thuc toan vector huong
        Vector3 direction = currentPoint.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = targetRotation;
    }

    void SetNextTarget(TargetPoint target)
    {
        switch (target)
        {
            case TargetPoint.topLeft:
                currentPoint = topLeftTransform;
                nextPoint = TargetPoint.topRight;
                break;
            case TargetPoint.topRight:
                currentPoint = topRightTransform;
                nextPoint = TargetPoint.bottomRight;
                break;
            case TargetPoint.bottomRight:
                currentPoint = bottomRightTransform;
                nextPoint = TargetPoint.bottomLeft;
                break;
            case TargetPoint.bottomLeft:
                currentPoint = bottomLeftTransform;
                nextPoint = TargetPoint.topLeft;
                break;
        }
    }
}