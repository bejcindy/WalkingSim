using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    [SerializeField]
    private Dice dice;

    private void Start()
    {
        dice.SetVisibility(false);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            dice.SetVisibility(true);
            dice.Roll();
            
        }

        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            dice.SetVisibility(false);
        }
        }
    }
