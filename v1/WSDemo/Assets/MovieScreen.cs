using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieScreen : Interactables
{

    public int m_MinIndex, m_MaxIndex;
    public int m_CurrentMeshId = 0;

    private void Start()
    {
        base.item.rollableItem = true;
        base.item.grabbable = false;

    }
    public override void ItemInteract()
    {
        base.ItemInteract();
        if (m_InteractBlockName != "")
        {
            UIManager.m_Instance.m_Flowchart.ExecuteBlock(m_InteractBlockName);
        }

    }
    public override void ItemRoll()
    {
        base.ItemRoll();
        if (m_RolledBlockName != "")
        {
            UIManager.m_Instance.m_Flowchart.ExecuteBlock(m_RolledBlockName);
        }
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
