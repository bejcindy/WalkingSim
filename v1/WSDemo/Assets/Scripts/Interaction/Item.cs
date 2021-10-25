using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ReplaceMesh
{
	public Mesh mesh;
	public float scale;

}

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class Item : ScriptableObject
{
	public bool requiredItem;

	public bool grabbable;

	public AudioClip audioClip;
	public string itemName;
	public Sprite image;

	public bool rollableItem;   // **important
	public RollType m_RollType;

	[Header("Inventory")]
	public bool inventoryItem;
	public string collectMessage;

	[Header("Models")]
	public List<ReplaceMesh> meshes;

}
