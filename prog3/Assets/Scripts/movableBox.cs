using UnityEngine;

public class MovableBox : MonoBehaviour
{
    public float pushSpeed = 5f;
    private Rigidbody2D rigbod;

    void Start()
    {
        rigbod = GetComponent<Rigidbody2D>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();

        if (player == null)
        {
            return;
        }

        if (player.IsDashing())
        {
            float dashDirection = player.GetDashDirection();

            Vector2 newPos = rigbod.position + new Vector2(dashDirection * pushSpeed * Time.deltaTime, 0f);
            rigbod.MovePosition(newPos);
        }
        else if (player.IsWalking())
        {
            rigbod.linearVelocity = Vector2.zero;
        }
    }
}
