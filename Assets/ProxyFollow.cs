//using System.Numerics;
using UnityEngine;

public class ProxyFollow : MonoBehaviour
{
    public Transform playerTransform;

    void Start()
    {
        if (playerTransform == null)
        {
            print("[ProxyFollow] PlayerTransform variable not assigned in editor.");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Create a new position while keeping this object's current X and Z
            Vector3 newPosition = new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);

            // Apply the new position
            transform.position = newPosition;
        }
    }
}