using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] Transform ballTransform;

    [SerializeField] float speed = 2.5f;
    [Tooltip("Multiply speed on ball collision")]
    [SerializeField] float multiplySpeedOnTouch = 1.5f;

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, 
            new Vector2(transform.position.x, ballTransform.position.y), speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            speed *= Random.Range(0.8f, multiplySpeedOnTouch);
        }
    }
}
