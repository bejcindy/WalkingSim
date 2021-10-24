using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    public static WeatherThumbnail m_Instance;
    public List<Mesh> m_MeshArray;  // 6

    public int m_MinIndex, m_MaxIndex;
    public int m_CurrentMeshId = 0;

    public Animator m_Anim;

    private void Awake()
    {
        m_Instance = this;
    }
    private void Start()
    {
        base.item.rollableItem = true;
        base.item.grabbable = true;

        m_Anim = GetComponent<Animator>();
    }
    public override void ItemInteract()
    {
        base.ItemInteract();

    }
    public override void ItemRoll()
    {
        base.ItemRoll();
        m_CurrentMeshId = Random.Range(m_MinIndex, m_MaxIndex);

        Debug.Log("Weather id: " + m_CurrentMeshId);
        UIManager.m_Instance.m_Flowchart.SetIntegerVariable("WeatherId", m_CurrentMeshId);

        SetMesh(m_CurrentMeshId);

    }

    IEnumerator SwitchWeather()
    {
        m_Anim.SetBool("IsPress", true);
        float timer = 0;
        while (timer < 0.5f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        m_Anim.SetBool("IsPress", false);
    }
    public void SetMesh(int id)
    {
        Debug.Log("Current mesh: " + m_CurrentMeshId);

        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        Mesh mesh2 = m_MeshArray[id];
        StartCoroutine(SwitchWeather());
        GetComponent<MeshFilter>().sharedMesh = mesh2;

    }

    public override void RollCheck()
    {
        base.RollCheck();

        if (m_LeftItemBlock != "")
        {
            UIManager.m_Instance.m_Flowchart.ExecuteBlock(m_LeftItemBlock);
        }

    }
}
