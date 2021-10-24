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

public enum RollType
{
	normal = 0,
	special,
}

public enum ResultType
{
	Fail = 0,
	Neutual,
	Success,
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
	[SerializeField] public string m_LeftItemBlock;

	[Header("ROLL")]
	[SerializeField]
	private int m_RollResult = 0;
	//public float[] m_Possibilities = new float[3];

	public int GetRollResult()
    {
		return m_RollResult;
    }
	float Choose(float[] probs)
	{
		//将事件元素加入到数组中，如上面有4个元素，分别为50,25,20,5
		{
			float total = 0;
			for(int i = 0 ; i < probs.Length;i++)
				total += probs[i];
		}
		//Random.value方法返回一个0—1的随机数
		float randomPoint = Random.value;
		for (int i = 0; i < probs.Length; i++)
		{
			if (randomPoint < probs[i])
				return i;
			else
				randomPoint -= probs[i];
		}
		return probs.Length - 1;
	}

	// envoke when roll a <rollable> item
	virtual public void ItemRoll()
	{
        if (item.m_RollType == RollType.normal)
        {
			// 3 possible results: FAIL, NEUTUAL, SUCCESS
			// possibilities for example: 1/6, 4/6, 1/6
		}
		else if(item.m_RollType == RollType.special)
        {
			//
        }


		if(m_RolledBlockName!="")
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
