using UnityEngine;
using System.Collections;


public class Block_Spawner : MonoBehaviour
{
    public float width = 8; 
    private int numBlocks = 8; 
    public GameObject blockPrefab;
    public GameObject[] blocks;


    void SpawnBlockAtIndex(int index) {
        if (index < 0 || index >= numBlocks) {
            Debug.LogError("Index out of bounds in SpawnBlockAtIndex: " + index);
            return;
        }
        // GameObject block = GameObject.Instantiate(blockPrefab, this.transform);
        GameObject block = GameObject.Instantiate(blocks[Random.Range(0, blocks.Length)], this.transform);
        block.transform.SetParent(null, true); // unparent the block so it doesn't move with the spawner 
        block.transform.position = new Vector2(index * width / numBlocks * this.transform.localScale.x - this.transform.localScale.x * width / 2, this.transform.position.y);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // Wait for a random time between 1 and 3 seconds
            float delay = Random.Range(1f, 3f);
            yield return new WaitForSeconds(delay);

            SpawnBlockAtIndex(Random.Range(0, numBlocks));
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}
