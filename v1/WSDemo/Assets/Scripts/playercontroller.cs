using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public int speed = 5;
    public int jumpForce = 400;
    private Vector2 rotation = Vector2.zero;
    float rotSpeed=5f; //mouse sensitivity
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        transform.eulerAngles = (Vector2)rotation * rotSpeed;


        //Vector3 move = Input.GetAxis("Vertical") * transform.forward * speed;
        //Vector3 strafe = Input.GetAxis("Horizontal") * transform.right * speed;
        //rb.velocity = move + strafe;

        //float xSpeed = Input.GetAxisRaw("Horizontal") * speed;
        //float zSpeed = Input.GetAxisRaw("Vertical") * speed;

        //rb.velocity = new Vector3(xSpeed, rb.velocity.y, zSpeed);
        //if (Input.GetButtonDown("Jump"))
        //{
        //    rb.AddForce(new Vector3(0, jumpForce, 0));
        //}
    }

}
