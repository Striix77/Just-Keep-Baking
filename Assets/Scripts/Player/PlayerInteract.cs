using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask interactableLayer;
    private PlayerUI playerUI;
    void Start()
    {
        cam = Camera.main;
        playerUI = GetComponent<PlayerUI>();
    }

    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        // Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, interactableLayer))
        {
            Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                playerUI.UpdateText(interactable.promptMessage);
                if (InputManager.playerActions.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
        else
        {
            playerUI.UpdateText("");
        }
    }
}
