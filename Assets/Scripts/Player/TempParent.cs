using UnityEngine;

public class TempParent : MonoBehaviour
{
    public static TempParent instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
