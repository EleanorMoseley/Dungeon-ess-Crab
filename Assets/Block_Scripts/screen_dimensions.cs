using UnityEngine;

public class screen_dimensions : MonoBehaviour
{
    public static float screen_width;
    public static float wall_width;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screen_width = Screen.width;
        wall_width = screen_width * 0.1f; // Example calculation, adjust as needed
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

// public Component[] spawnPositions; 
// void Start () 
// { 
//     spawnPositions = GetComponentsInChildren<Transform>(); 
// }