using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool ToggleCursorState;

    // Start is called before the first frame update
    void Start()
    {
        ToggleCursorState = false;
        //false = unlocked and visible
        //true = locked and invisible
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("c"))
        {
            ToggleCursorState = !ToggleCursorState;
        }

        if(ToggleCursorState)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }
}
