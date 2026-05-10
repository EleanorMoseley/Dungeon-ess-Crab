using UnityEngine;

public class Block_Spawner : MonoBehaviour
{
    public float width = 8; 
    private int numBlocks = 8; 
    public GameObject blockPrefab;
    public Component[] spawnPositions; 



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 1; i < numBlocks+1 ; i++)
        {
            if (i == 4 ){
                continue; // Skip the middle block
            }
            GameObject block = GameObject.Instantiate(blockPrefab, this.transform);
            block.transform.position = new Vector2(i* width/numBlocks*this.transform.localScale.x - this.transform.localScale.x*width/2 , 0);
            // block.transform.localScale = new Vector2(width / numBlocks, width / numBlocks);

        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    // spawnPositions = GetComponentsInChildren<Transform>(); 

}
