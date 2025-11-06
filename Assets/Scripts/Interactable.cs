using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    public string promptMessage;

    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        Debug.Log("Interacting with " + gameObject.name);
    }
}
