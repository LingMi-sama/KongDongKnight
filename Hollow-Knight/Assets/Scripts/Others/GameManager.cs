using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool enableInput = false;

    public bool IsEnableInput()
    {
        return enableInput;
    }

    public void SetEnableInput(bool enabled)
    {
        enableInput = enabled;
    }
}
