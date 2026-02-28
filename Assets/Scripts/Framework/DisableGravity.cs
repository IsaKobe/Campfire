using UnityEngine;

public class DisableGravity : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Physics.gravity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
