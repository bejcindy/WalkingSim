using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : Interactables
{
    public List<Mesh> m_MeshArray;  // 6

    public int m_MinIndex, m_MaxIndex;
    public int m_CurrentMeshId = 0;
    private Vector3 originScale;

    public Animator m_Anim;

    private void Start()
    {
        base.item.rollableItem = true;
        base.item.grabbable = true;

        m_Anim = GetComponent<Animator>();

        originScale = transform.localScale;
    }
    public override void ItemInteract()
    {
        base.ItemInteract();

    }
    public override void ItemRoll()
    {
        base.ItemRoll();
        m_CurrentMeshId = Random.Range(m_MinIndex, m_MaxIndex);

        Debug.Log("Gift id: " + m_CurrentMeshId);
        UIManager.m_Instance.m_Flowchart.SetIntegerVariable("GiftId", m_CurrentMeshId);

        SetMesh(m_CurrentMeshId);

    }

    IEnumerator SwitchGift()
    {
        //m_Anim.SetBool("IsPress", true);
        //float timer = 0;
        //while (timer < 0.5f)
        //{
        //    timer += Time.deltaTime;
        //    yield return null;
        //}
        //m_Anim.SetBool("IsPress", false);
        yield return null;
    }
    public void SetMesh(int id)
    {
        Debug.Log("Current mesh: " + m_CurrentMeshId);

        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        Mesh mesh2 = item.meshes[id].mesh;
        StartCoroutine(SwitchGift());
        GetComponent<MeshFilter>().sharedMesh = mesh2;
        transform.localScale = originScale * item.meshes[id].scale;

    }
    public override void ItemCollect()
    {
        base.ItemCollect();
        gameObject.SetActive(false);
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
