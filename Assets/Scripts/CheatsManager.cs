using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CheatsManager : MonoBehaviour
{
    [SerializeField] private float timeLive;
    [SerializeField] private CheatItem[] _cheats;


    private string inputString;

    private void Awake()
    {
        Keyboard.current.onTextInput += TextInput;
    }

    private void OnDestroy()
    {
        Keyboard.current.onTextInput -= TextInput;
    }

    private void TextInput(char obj)
    {
        CancelInvoke("ClearString");
        inputString += obj;
        foreach (var cheat in _cheats)
        {
            if (inputString.Contains(cheat.name))
            {
                cheat._event?.Invoke();
                inputString = null;
            }
        }
        Invoke("ClearString", timeLive);
    }

    private void ClearString()
    {
        inputString = String.Empty;
    }
}
[Serializable]
public class CheatItem
{
    public string name;
    public UnityEvent _event;
}
