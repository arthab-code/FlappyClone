using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : Singleton<InputController>
{
    private bool isPressed = false;

    public bool IsPressed
    {
        get { return isPressed; }

        set
        {
            isPressed = value;
        }
    }

    private void Update()
    {
        CheckPress();

    }

    public void CheckPress()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            IsPressed = true;
        else
            IsPressed = false;
    }

}
