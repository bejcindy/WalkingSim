using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : Interactables
{
    public List<Texture> m_RollTextures;

    static public ElevatorButton m_Instance;
    public int m_MinIndex, m_MaxIndex;
    public int m_CurrentButton = 0;

    public DoorController m_BackDoor;

    public Animator m_Anim;

    private void Awake()
    {
        m_Instance = this;
    }
    private void Start()
    {
        base.item.rollableItem = true;
        base.item.grabbable = false;
        m_Anim = GetComponent<Animator>();
    }
    public override void ItemInteract()
    {
        base.ItemInteract();

    }
    public override void ItemRoll()
    {
        base.ItemRoll();
        m_CurrentButton = Random.Range(m_MinIndex, m_MaxIndex);

        Debug.Log("Button id: " + m_CurrentButton);
        UIManager.m_Instance.m_Flowchart.SetIntegerVariable("ElevatorButton", m_CurrentButton);

        SetTexture();

    }
    IEnumerator PressingButton()
    {
        m_Anim.SetBool("IsPress", true);
        float timer = 0;
        while (timer < 0.4f)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        m_Anim.SetBool("IsPress", false);
    }
    public void SetTexture()
    {
        Debug.Log("CUrrent patter: " + m_CurrentButton);


    }

    public override void RollCheck()
    {
        base.RollCheck();
        StartCoroutine(PressingButton());
        if (m_LeftItemBlock != "")
        {
            UIManager.m_Instance.m_Flowchart.ExecuteBlock(m_LeftItemBlock);
        }
        if (m_CurrentButton == 3)
        {
            OpenElevator();
        }
    }

    public void OpenElevator()
    {
        m_BackDoor.locked = false;
        m_BackDoor.doorAnimator.SetBool("IsOpen", true);
    }

}
