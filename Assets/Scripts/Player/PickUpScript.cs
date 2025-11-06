using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    private bool isHolding = false;
    private Camera cam;

    [SerializeField]
    float maxDistance = 3f;

    TempParent tempParent;
    Rigidbody rb;

    Vector3 objectPosition;

    void Start()
    {
        tempParent = TempParent.instance;
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        // If we're already holding something, don't rely on the raycast to keep it.
        // Only drop when the hold input is released. Also keep the rigidbody stable.
        if (isHolding)
        {
            if (!InputManager.IsHolding())
            {
                Drop();
            }
            else if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                // Optional: snap to tempParent position to avoid tunneling when moving fast
                // rb.transform.position = Vector3.MoveTowards(rb.transform.position, tempParent.transform.position, 50f * Time.deltaTime);
                // rb.transform.rotation = tempParent.transform.rotation;
            }
            return;
        }

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.blue);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, maxDistance))
        {
            if (hitInfo.transform.CompareTag("Holdable") && InputManager.IsHolding())
            {
                // Skip UI/RectTransform prefabs
                if (hitInfo.transform.GetComponent<RectTransform>() != null)
                    return;

                isHolding = true;
                // Prefer collider's rigidbody, fall back to parent
                rb = hitInfo.collider.GetComponent<Rigidbody>() ?? hitInfo.collider.GetComponentInParent<Rigidbody>();
                if (rb == null)
                {
                    isHolding = false;
                    return;
                }

                rb.useGravity = false;
                rb.detectCollisions = true;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                hitInfo.transform.SetParent(tempParent.transform, true);
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    private void Drop()
    {
        if (rb != null && isHolding)
        {
            isHolding = false;
            rb.useGravity = true;
            rb.detectCollisions = true;
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rb.transform.SetParent(null, true);
            rb = null;
        }
    }
}
