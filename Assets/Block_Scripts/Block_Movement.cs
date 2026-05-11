using UnityEngine;

public class Block_Movement : MonoBehaviour
{
   
    private enum BlockState {
        Falling,
        Stopped
    }
    private Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null) {
            Debug.LogError("[Block_Movement] No Rigidbody2D found in children of " + gameObject.name);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            return;
            // TODO: Kill the player instead.
        }

        if (other.gameObject.CompareTag("Ground")) {
            gameObject.layer = 6;   // this is the ground layer. You can also pass a string. "GroundLayer"
            Transform[] children = this.GetComponentsInChildren<Transform>(true);
            foreach (var child in children)
            {
                child.gameObject.layer = 6;
            }
            rb.linearVelocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    // Update is called once per frame
    void Update()
    {    
    }
}
