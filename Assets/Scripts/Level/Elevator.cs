using System;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private Animator _elevatorAnimator;

    private void Awake()
    {
        _elevatorAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
            _elevatorAnimator.SetTrigger("Activate");
    }
}