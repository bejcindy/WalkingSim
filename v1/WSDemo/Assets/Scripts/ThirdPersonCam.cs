using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public Transform player;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 3.5f, -4);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;
    }
}
