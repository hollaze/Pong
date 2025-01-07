using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private PointsHandler pointsHandler;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private AudioClip bounceBallPaddleSFX;
    private AudioSource audioSource;

    [Header("Launch ball")]
    [SerializeField] private float launchSpeed = 150f;

    [Tooltip("Randomize range value between this value and vector values")]
    [SerializeField] private float launchRandomRange = 45;
    [SerializeField] private Vector2 launchBallStart = new Vector2(90, 90);

    [Header("Ball in game")]
    [Tooltip("Multiply bounce force when touching paddles")]
    [SerializeField] private float bouncePaddleForceMultiply = 1.8f;

    #region Launch ball
    /// <summary>
    /// Randomize between positive 1 and negative -1 values
    /// </summary>
    /// <returns>1 or -1</returns>
    private int RandomPositiveNegative()
    {
        if (Random.value >= 0.5)
            return -1;

        return 1;
    }

    /// <summary>
    /// Randomize ball launching forward or backward
    /// </summary>
    /// <returns>Randomized ball launch</returns>
    private Vector2 launchRandom()
    {
        Vector2 randomizeLaunch = new Vector2(Random.Range(launchRandomRange, launchBallStart.x), Random.Range(launchRandomRange, launchBallStart.y));

        randomizeLaunch.Normalize();

        return new Vector2(randomizeLaunch.x * RandomPositiveNegative(), randomizeLaunch.y * RandomPositiveNegative());
    }

    /// <summary>
    /// Launch ball
    /// </summary>
    private void launchBall()
    {
        rb2D.AddForce(launchRandom() * launchSpeed * Time.deltaTime, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Launch ball on restart / point scored
    /// </summary>
    public void RestartLaunch()
    {
        // Reset velocity
        rb2D.linearVelocity = Vector2.zero;

        launchBall();

        GameState.restartLaunch = false;
    }
    #endregion



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();

        launchBall();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Multiply ball speed on touching paddle
        // Play audio
        if (collision.gameObject.CompareTag("Paddle"))
        {
            rb2D.AddForce(rb2D.linearVelocity * bouncePaddleForceMultiply * Time.deltaTime, ForceMode2D.Impulse);

            audioSource.pitch = Random.Range(0.5f, 1.5f);
            audioSource.PlayOneShot(bounceBallPaddleSFX);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Left Trigger"))
        {
            pointsHandler.PlayerPoints += 1;
        }

        if (collision.CompareTag("Right Trigger"))
        {
            pointsHandler.EnemyPoints += 1;
        }
    }

    private void FixedUpdate()
    {
        if (GameState.restartLaunch)
        {
            RestartLaunch();
        }
    }
}
