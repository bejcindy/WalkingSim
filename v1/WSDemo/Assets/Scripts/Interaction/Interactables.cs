using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PreviousItem
{
	public Item requiredItem;
	public Item interactionItem;
	public UnityEvent OnInteract;
}

public class Interactables : MonoBehaviour
{
	public Item item;

	public PreviousItem[] previousItem;

	public UnityEvent OnInteract;
	public UnityEvent OnRoll;
	public UnityEvent CollectItem;
	public UnityEvent LeaveItem;

	[HideInInspector]
	public bool isMoving;

	[Header("Fungus")]
	[SerializeField] public string m_InteractBlockName;
	[SerializeField] public string m_RolledBlockName;
	[SerializeField] public string m_LeftItemName;

	// envoke when roll a <rollable> item
	virtual public void ItemRoll()
	{

		UIManager.m_Instance.m_Flowchart.ExecuteBlock(m_RolledBlockName);

	}

	// envoke when player drops an item
	virtual public void RollCheck()
	{
	
	}

	// envoke when player collect the item
	// may not useful
	virtual public void ItemCollect()
    {
		Debug.Log("Why you need that?");
	}

	virtual public void ItemInteract()
    {
		Debug.Log("It shows current time");
	}
}
