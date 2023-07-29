using UnityEngine;

public class PlayerInteractor : MonoBehaviour, IInteractor
{
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact(this);
        }
    }
}
