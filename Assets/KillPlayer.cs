using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    // Drag your Death Screen Panel into this slot in the Inspector
    public GameObject DeathScreen;

    private void OnTriggerEnter(Collider other) // Use OnTriggerEnter2D if your game is 2D
    {
        // Check if the thing that touched us is the Player
        if (other.CompareTag("Player"))
        {
            // Activate the death screen
            DeathScreen.SetActive(true);

            // Optional: Freeze the game
            Time.timeScale = 0f;

            // Optional: Log it for testing
            print("Player has died!");
        }
    }
}
