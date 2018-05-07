using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement movement;

    private void OnCollisionEnter(Collision collision)
    {
        string collidingObjectTag = collision.collider.tag;

        if (collidingObjectTag == "Obstacle")
        {
            movement.enabled = false;
            GameManager gm = FindObjectOfType<GameManager>();
            gm.EndGame();

        }
    }
}
