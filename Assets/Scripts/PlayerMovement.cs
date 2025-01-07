using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 125f;
    [Tooltip("Time to join value, between 0.x and 1, lower value = longer time to join")]
    [SerializeField] float damping = 0.1f;

    private float yAxis;

    // Update is called once per frame
    void Update()
    {
        yAxis = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, transform.position.y + yAxis * speed * Time.deltaTime), damping);
    }
}
