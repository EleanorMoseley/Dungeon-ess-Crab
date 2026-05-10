using UnityEngine;

public class WallSensor : MonoBehaviour
{
    public bool IsTouchingWall { get; private set; }

    // Use triggers so the sensors don't physically "push" the wall
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            IsTouchingWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            IsTouchingWall = false;
        }
    }
}