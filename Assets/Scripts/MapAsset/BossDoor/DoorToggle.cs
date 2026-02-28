using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorToggle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool IsDoorActive = false;
    public bool WasTriggered = false;
    [SerializeField] GameObject Gates;
    // Update is called once per frame
    void Awake()
    {
        Gates.SetActive(false);   
    }
    public void ToggleGate()
    {
        Debug.Log("Door Toggled");
        IsDoorActive=!IsDoorActive;
        Gates.SetActive(IsDoorActive);
    }
}
