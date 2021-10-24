using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class spawnPoint
{
    public Transform pos;
    public string discription;
}

public class PlayerSpawn : MonoBehaviour
{
    public List<spawnPoint> m_SpawnPoints;
    public GameObject player;
    private bool isMoving = false;
    // 在多个位置快速跳转
    // F1-F7
    // 跳转后按下P键接触 Player位移的锁定
    void Start()
    {
        isMoving = false;
    }
    IEnumerator MovingObject(GameObject obj, Vector3 position)
    {
        isMoving = true;
        float timer = 0;
        while (timer < 1)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, position, Time.deltaTime * 5);
            timer += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = position;
        isMoving = false;
    }
    public void Teleport(int i)
    {
        //player.transform.position = m_SpawnPoints[0].pos.position + new Vector3(0, 1.1f, 0);
        if(i<m_SpawnPoints.Count)
            StartCoroutine(MovingObject(player, m_SpawnPoints[i].pos.position));

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            player.GetComponent<CharacterController>().enabled = false;
            Teleport(0);

        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            player.GetComponent<CharacterController>().enabled = false;
            Teleport(1);

        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            player.GetComponent<CharacterController>().enabled = false;
            Teleport(2);

        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            player.GetComponent<CharacterController>().enabled = false;
            Teleport(3);

        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            player.GetComponent<CharacterController>().enabled = false;
            Teleport(4);

        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            player.GetComponent<CharacterController>().enabled = false;
            Teleport(5);

        }
        else if (Input.GetKeyDown(KeyCode.F7))
        {
            player.GetComponent<CharacterController>().enabled = false;
            Teleport(6);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            player.GetComponent<CharacterController>().enabled = true;

        }
    }
}