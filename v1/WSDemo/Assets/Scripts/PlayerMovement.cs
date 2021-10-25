using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    public float speed;
    public Vector3 fall;
    int torque;
    public GameObject thirdCam;
    float rotSpeed = 1f;
    public Transform fpc; //first person camera
    public float waitTime; //wait before switching cameras

    //Vector2 rotation = Vector2.zero;
    Rigidbody rb;
    bool isFalling = false;
    bool restoring = false;
    bool resetRotation = false;
    float t;
    
    Vector3 forward, right;
    Vector3 move, strafe;

    Vector3[] faces;
    Vector3 ForwardFace;
    int closest;
    Quaternion lookToward;
    int r;

    void Start()
    {
        //隐藏鼠标指针
        //Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        //fpc = transform.GetChild(0);
        faces = new Vector3[6];
        for(int i = 0; i < 6; i++)
        {
            faces[i] = (transform.GetChild(i).position - transform.position).normalized;
        }
        ForwardFace = faces[4];
        t = 0;
        r = 3;
        thirdCam.SetActive(false);
        fpc.gameObject.SetActive(true);
    }

    void Update()
    {
        //float xSpeed = Input.GetAxisRaw("Horizontal") * speed;
        //float zSpeed = Input.GetAxisRaw("Vertical") * speed;

        if (!isFalling)
        {
            //if (resetRotation)
            //{
            //    t += Time.deltaTime;
            //    if (t >= 1)
            //    {
            //        transform.eulerAngles = Vector3.zero;
            //        resetRotation = false;
            //    }
            //}
            forward = new Vector3(fpc.forward.x, 0, fpc.forward.z).normalized;
            right = new Vector3(fpc.right.x, fpc.right.y, 0).normalized;
            transform.eulerAngles = Vector3.zero;

            #region 用不到的code
            //rotation.y += Input.GetAxis("Mouse X");
            //rotation.x += -Input.GetAxis("Mouse Y");
            //transform.eulerAngles = (Vector2)rotation * rotSpeed;
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + rotation.y, 0);
            //lookToward = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(ForwardFace, fpc.forward), rotSpeed * Time.deltaTime);
            //lookToward = Quaternion.Slerp(Quaternion.Euler(ForwardFace), Quaternion.Euler(fpc.forward), rotSpeed * Time.deltaTime);
            //lookToward = Quaternion.FromToRotation(ForwardFace, fpc.forward);
            //Debug.Log(lookToward.eulerAngles);
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, lookToward.eulerAngles.y, transform.eulerAngles.z);

            //transform.Rotate(0, lookToward.eulerAngles.y, 0);
            #endregion

            move = Input.GetAxis("Vertical") * forward * speed;
            strafe = Input.GetAxis("Horizontal") * right * speed;
            rb.velocity = move + strafe;
        }

        if (restoring && rb.velocity == Vector3.zero)
        {
            t += Time.deltaTime;
            if (t >= waitTime)
            {
                thirdCam.SetActive(false);
                fpc.gameObject.SetActive(true);
                restoring = false;
                isFalling = false;
                resetRotation = true;
                t = 0;
            }
            

            #region 其他用不到的code
            //float[] dist = new float[6];
            //int minIndex = -1;
            //float min = Mathf.Infinity;

            //for (int i = 0; i < 6; i++)
            //{
            //    faces[i] = (transform.GetChild(i).position - transform.position).normalized;
            //    //faces[i] = transform.GetChild(1 + i);
            //    //Debug.Log(faces[i].position);
            //    //dist[i] = Vector3.Dot(faces[i], fpc.forward);
            //    if (faces[i].y == 1||faces[i].y==-1)
            //    {
            //        dist[i] = 400;
            //    }
            //    else
            //    {
            //        dist[i] = Vector3.Angle(faces[i], fpc.forward);
            //        //Debug.Log(i+" y is 0");
            //    }
            //    //dist[i] = Vector3.Angle(faces[i], fpc.forward);

            //    //Debug.Log(i + " angle difference is: " + dist[i]);
            //    if (dist[i] < min)
            //    {
            //        min = dist[i];
            //        minIndex = i;
            //    }
            //    //Debug.Log(i+" y value is "+faces[i].y);
            //    //Debug.Log(i + " face: " + faces[i]);
            //}
            //closest = minIndex;
            //Debug.Log("closest: " + closest);
            //ForwardFace = faces[closest];
            //Debug.Log("grounded");
            #endregion

            
        }

        
        //Debug.Log(move);
        //rb.AddForce(move + strafe);

        //rb.velocity = new Vector3(xSpeed, rb.velocity.y, zSpeed);
        //xSpeed *= Time.deltaTime;
        //zSpeed *= Time.deltaTime;
        //transform.Translate(xSpeed, 0, zSpeed);
        if (Input.GetKeyDown("escape"))
        {
            //显示鼠标指针
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Fall" && !isFalling)
        {
            isFalling = true;
            Vector3 force = new Vector3(fall.x*forward.x, fall.y, fall.x*forward.z);
            rb.AddForce(force);
            torque = Random.Range(10, 30);
            rb.AddTorque(transform.forward * torque);
            thirdCam.SetActive(true);
            Debug.Log(torque);
            //fpc.gameObject.SetActive(false);
            Debug.Log("falled");
        }
        if (isFalling && collision.gameObject.tag == "Ground")
        {
            restoring = true;
            //float[] dist = new float[6];
            //for (int i = 0; i < 6; i++)
            //{
            //    //faces[i] = transform.GetChild(1 + i);
            //    //Debug.Log(faces[i].position);
            //    dist[i] = Vector3.Dot(faces[i].position, fpc.forward);
            //    if (i > 0 && dist[i] < dist[i - 1])
            //    {
            //        closest = i;
            //    }
            //    else if (i > 0 && dist[i] > dist[i - 1])
            //    {
            //        closest = i - 1;
            //    }
            //    else
            //    {
            //        closest = 0;
            //    }
            //    //Debug.Log(faces[i].name + ": " + dist[i]);
            //}
            
            //Debug.Log("closest: "+faces[closest].name);
            //ForwardFace = faces[closest].position;
            //Debug.Log("grounded");
        }
    }
}
