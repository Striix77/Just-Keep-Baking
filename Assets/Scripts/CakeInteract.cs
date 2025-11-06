using UnityEngine;

public class CakeInteract : Interactable
{
    void Start()
    {
    }

    void Update()
    {

    }

    protected override void Interact()
    {
        Debug.Log("Eating Cake!");
    }
}
