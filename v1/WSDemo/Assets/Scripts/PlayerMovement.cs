using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    public float speed;
    public Vector3 fall;
    public float torque;
    public float rotSpeed = 10;

    Vector2 rotation = Vector2.zero;
    Rigidbody rb;
    bool isFalling = false;
    bool restoring = false;
    Transform fpc; //first person camera
    Vector3 forward, right;
    Vector3 move, strafe;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        fpc = transform.GetChild(0);
    }

    void Update()
    {
        //float xSpeed = Input.GetAxisRaw("Horizontal") * speed;
        //float zSpeed = Input.GetAxisRaw("Vertical") * speed;

        if (!isFalling)
        {
            forward = new Vector3(fpc.forward.x, 0, fpc.forward.z).normalized;
            right = new Vector3(fpc.right.x, fpc.right.y, 0).normalized;

            rotation.y += Input.GetAxis("Mouse X");
            rotation.x += -Input.GetAxis("Mouse Y");
            //transform.eulerAngles = (Vector2)rotation * rotSpeed;
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + rotation.y, 0);


            move = Input.GetAxis("Vertical") * forward * speed;
            strafe = Input.GetAxis("Horizontal") * right * speed;
            rb.velocity = move + strafe;
        }

        if (restoring && rb.velocity == Vector3.zero)
        {
            restoring = false;
            isFalling = false;
        }

        
        //Debug.Log(move);
        //rb.AddForce(move + strafe);

        //rb.velocity = new Vector3(xSpeed, rb.velocity.y, zSpeed);
        //xSpeed *= Time.deltaTime;
        //zSpeed *= Time.deltaTime;
        //transform.Translate(xSpeed, 0, zSpeed);
        if (Input.GetKeyDown("escape"))
        {
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
            rb.AddTorque(transform.forward * torque);
            Debug.Log("falled");
        }
        if (isFalling && collision.gameObject.tag == "Ground")
        {
            restoring = true;
            Debug.Log("grounded");
        }
    }
}
