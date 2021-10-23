using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : Interactables
{
    public List<Texture> m_RollTextures;

    static public ElevatorButton m_Instance;
    public int m_MinIndex, m_MaxIndex;
    public int m_CurrentButton = 0;

    private void Awake()
    {
        m_Instance = this;
    }
    private void Start()
    {
        base.item.rollableItem = true;
        base.item.grabbable = false;
    }
    public override void ItemInteract()
    {
        base.ItemInteract();

    }
    public override void ItemRoll()
    {
        base.ItemRoll();
        m_CurrentButton = Random.Range(m_MinIndex, m_MaxIndex);

        SetTexture();

    }

    public void SetTexture()
    {
        Debug.Log("CUrrent patter: " + m_CurrentButton);
        UIManager.m_Instance.m_Flowchart.ExecuteBlock(m_LeftItemName);

    }

    public override void RollCheck()
    {
        base.RollCheck();
    }


}
