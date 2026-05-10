using UnityEngine;

public class Block_Movement : MonoBehaviour
{
   
    private enum BlockState {
        Falling,
        Stopped
    }
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        // for (int i = 0; i < numBlocks; i++)
        // {
        //     GameObject block = GameObject.Instantiate(prefab, this.transform);
        //     block.transform.position = new Vector2(i * width / numBlocks, 0);
        //     block.transform.localScale = new Vector2(width / numBlocks, width / numBlocks);
        // }
    }
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("Ground")) {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
    // Update is called once per frame
    void Update()
    {    
    }
}
