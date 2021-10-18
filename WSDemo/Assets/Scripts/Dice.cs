using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    bool hasLanded = false;
    bool thrown;

    Vector3 initPos;
    public int diceValue;

    [Header("Torque")]
    public float xTorque, yTorque, zTorque;

 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initPos = transform.position;
        rb.useGravity = false;
    }

    private void Update()
    {
        if (!thrown && Input.GetKeyDown(KeyCode.R))
        {
            SetVisibility(true);
            Roll();

        }
    }

    public void SetVisibility(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }

    public void Roll()
    {

        if(!thrown && !hasLanded)
        {
            Debug.Log("Roll it");
            thrown = true;
            rb.useGravity = true;
            rb.AddTorque(Random.Range(0, xTorque), Random.Range(0, yTorque), Random.Range(0, zTorque));
        }


    }
}
