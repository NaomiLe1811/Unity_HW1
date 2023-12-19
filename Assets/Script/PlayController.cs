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

//manual hay automatic
public enum DriveMode
{
    Manual,
    Auto,
}

public class PlayerController1 : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int mySpeed = 10;
    [SerializeField] private int rotationSpeed = 10;
    [SerializeField] private int damage = 10;
    [SerializeField] private int fuel;
    [SerializeField] private int capacity = 10;
    [SerializeField] private int laps = 0;

    //gan trang thai dau tien
    private TargetPoint nextPoint = TargetPoint.topLeft;

    //khai bao vi tri (toa do x,y,z) cua 4 diem
    public Transform topLeftTransform;
    public Transform topRightTransform;
    public Transform bottomRightTransform;
    public Transform bottomLeftTransform;

    private DriveMode mode = DriveMode.Manual; //trang thai hien tai
    //khai bao vi tri hien tai
    private Transform currentPoint;

    // Start is called before the first frame update
    void Start()
    {
        currentPoint = topLeftTransform;
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if(mode == DriveMode.Auto)
        {
            AutoMode();
        }
        else if (mode == DriveMode.Manual)
        {
            ManualMode();
        }
    }

    void AutoMode()
    {
        Vector3 targetPoint = currentPoint.position;
        Vector3 moveDirection = currentPoint.position - transform.position;

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
        //cong thuc toan vector huong quay
        Vector3 direction = currentPoint.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = targetRotation;
    }
    void ManualMode()
    {
        //Input.GetAxis
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");

        //tinh toan vector di chuyen dua ten input
        //Vector3 movement = new Vector3(horizontalInput,0, verticalInput) * mySpeed * Time.deltaTime;

        //ap vi tri
        //transform.Translate(movement);

        // Lấy giá trị trục ngang và trục dọc từ bàn phím
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Tính toán di chuyển và quay
        float translation = verticalInput * mySpeed * Time.deltaTime;
        float rotation = horizontalInput * rotationSpeed * Time.deltaTime;

        // Di chuyển chiếc xe theo trục dọc (forward)
        transform.Translate(0f, 0f, translation);

        // Quay chiếc xe theo trục ngang (turn)
        transform.Rotate(0f, rotation, 0f);

        Debug.Log(horizontalInput + ", " + verticalInput);    
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ThungXang")
        {
            other.gameObject.SetActive(false);
            capacity = capacity + 5;
        } else if(other.gameObject.tag == "SpawnPoint")
        {
            laps++;
            Debug.Log("Laps++");
        } 
    }
    private void OnCollisionEnter(Collision collision)
    {
         if (collision.gameObject.tag == "HangRao")
        {
            damage -= 5;
            if(damage == 0)
            {
                Time.timeScale = 0;
            }
        }
    }
}
