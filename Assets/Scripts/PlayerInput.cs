using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public KeyCode currentInput;
    public bool hasPlayerReacted = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.anyKeyDown)
        {
            currentInput = CheckInput();
        }
    }

    public KeyCode CheckInput()
    {
        if (Input.anyKeyDown)
        {
            currentInput = KeyCode.None;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            hasPlayerReacted = true;
            currentInput = KeyCode.UpArrow;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            hasPlayerReacted = true;
            currentInput = KeyCode.DownArrow;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            hasPlayerReacted = true;
            currentInput = KeyCode.LeftArrow;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            hasPlayerReacted = true;
            currentInput = KeyCode.RightArrow;
        }
        return currentInput;
    }
}
