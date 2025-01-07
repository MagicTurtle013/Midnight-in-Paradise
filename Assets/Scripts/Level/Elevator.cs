using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Animator _elevatorAnimator;
    private void OnTriggerEnter(Collider other)
    {
            _elevatorAnimator.SetTrigger("Activate");
    }
}