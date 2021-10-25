using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInteraction : MonoBehaviour
{
	public float rayDistance = 2f;
	public float rotateSpeed = 200;

	public AudioClip writingSound;

	public Transform objectViewer;

	public UnityEvent OnView;
	public UnityEvent OnFinishView;

	private Camera myCam;

	[SerializeField]
	private bool isViewing;
	[SerializeField]
	private bool canFinish;

	[SerializeField]
	private Interactables currentInteractable;

	[SerializeField]
	private Item currentItem;
	private Vector3 originPosition;
	private Quaternion originRotation;

	private AudioPlayer audioPlayer;
	private PlayerInventory inventory;

	[SerializeField]
	private Material m_HighlightMat;
	[SerializeField]
	private Material m_ItemOriginMat;

	private void Awake()
	{
		audioPlayer = GetComponent<AudioPlayer>();
		inventory = GetComponent<PlayerInventory>();
	}

	// Start is called before the first frame update
	void Start()
    {
		myCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
		CheckInteractables();
    }
	void RollItem()
    {
		Debug.Log("YOU ROLL!!");
		currentInteractable.OnRoll.Invoke();
	}
	void CheckInteractables()
	{

		if (isViewing)
		{
			currentInteractable.GetComponent<MeshRenderer>().material = m_HighlightMat;

			if (currentInteractable.item.grabbable && Input.GetMouseButton(0))
			{
				RotateObject();
			}

			if (canFinish && Input.GetMouseButtonDown(1))
			{
				FinishView();
			}

			if (currentInteractable.item.rollableItem && Input.GetMouseButton(2))
			{
				// ROLL IT!!
				RollItem();
			}
			return;
		}

		RaycastHit hit;
		Vector3 rayOrigin = myCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));

		if (Physics.Raycast(rayOrigin, myCam.transform.forward, out hit, rayDistance))
		{
			Interactables interactable = hit.collider.GetComponent<Interactables>();
			if (interactable != null)
			{
				UIManager.m_Instance.SetHandCursor(true);
				if (Input.GetMouseButtonDown(0))
				{
					if (interactable.isMoving)
					{
						return;
					}

					currentInteractable = interactable;
					m_ItemOriginMat = currentInteractable.GetComponent<MeshRenderer>().material;
					currentInteractable.OnInteract.Invoke();

					if (currentInteractable.item != null)
					{
						OnView.Invoke();

						isViewing = true;

						bool hasPreviousItem = false;

						/*
						for (int i = 0; i < currentInteractable.previousItem.Length; i++)
						{
							if (inventory.itens.Contains(currentInteractable.previousItem[i].requiredItem))
							{
								Interact(currentInteractable.previousItem[i].interactionItem);
								currentInteractable.previousItem[i].OnInteract.Invoke();
								hasPreviousItem = true;
								break;
							}
						}

						if (hasPreviousItem)
						{
							return;
						}
						*/

						Interact(currentInteractable.item);

						if (currentInteractable.item.grabbable)
						{
							originPosition = currentInteractable.transform.position;
							originRotation = currentInteractable.transform.rotation;
							StartCoroutine(MovingObject(currentInteractable, objectViewer.position));
						}
					}


				}
			}
			else
			{
				UIManager.m_Instance.SetHandCursor(false);
			}
		}
		else
		{
			UIManager.m_Instance.SetHandCursor(false);
		}
	}

	void Interact(Item item)
	{
		currentItem = item;

		if(item.image != null)
		{
			UIManager.m_Instance.SetImage(item.image);
		}

		if (item.audioClip)
        {
			audioPlayer.PlayAudio(item.audioClip);
			Invoke("CanFinish", item.audioClip.length + 0.5f);
        }
        else
        {
			Invoke("CanFinish",  0.5f);
		}
		
		UIManager.m_Instance.SetCaptions(item.itemName);

	}

	void CanFinish()
	{
		canFinish = true;

		if(currentItem.image == null && !currentItem.grabbable)
		{
			FinishView();
		}
		else
		{

			UIManager.m_Instance.SetBackImage(true);
		}
		
		UIManager.m_Instance.SetCaptions("");
	}

	void FinishView()
	{
		canFinish = false;
		isViewing = false;
		UIManager.m_Instance.SetBackImage(false);
		Debug.Log("Finish viewing obj and left");
        if (currentItem.rollableItem)
        {
			currentInteractable.LeaveItem.Invoke();
		}
		if (currentItem.inventoryItem)
		{
			inventory.AddItem(currentItem);
            if (writingSound)
            {
				audioPlayer.PlayAudio(writingSound);
			}
			
			currentInteractable.CollectItem.Invoke();
		}
		if (currentItem.grabbable)
		{
			currentInteractable.transform.rotation = originRotation;
			StartCoroutine(MovingObject(currentInteractable, originPosition));
		}

		if (currentItem.requiredItem)
		{
			inventory.AddRequiredItems(currentItem);
		}
		
		// reset Item Material
		currentInteractable.GetComponent<MeshRenderer>().material = m_ItemOriginMat;

		OnFinishView.Invoke();
	}

	IEnumerator MovingObject(Interactables obj, Vector3 position)
	{
		obj.isMoving = true;
		float timer = 0;
		while (timer < 1)
		{
			obj.transform.position = Vector3.Lerp(obj.transform.position, position, Time.deltaTime * 5);
			timer += Time.deltaTime;
			yield return null;
		}

		obj.transform.position = position;
		obj.isMoving = false;
	}

	void RotateObject()
	{
		float x = Input.GetAxis("Mouse X");
		float y = Input.GetAxis("Mouse Y");
		currentInteractable.transform.Rotate(myCam.transform.right, -Mathf.Deg2Rad * y * rotateSpeed, Space.World);
		currentInteractable.transform.Rotate(myCam.transform.up, -Mathf.Deg2Rad * x * rotateSpeed, Space.World);
	}

    public void ItemHighlight()
    {
		currentInteractable.GetComponent<MeshRenderer>().material = m_HighlightMat;
	}

}
