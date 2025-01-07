using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private PointsHandler pointsHandler;

    [SerializeField] private Transform playerStartPos;
    [SerializeField] private Transform enemyStartPos;
    [SerializeField] private Transform ballStartPos;

    [SerializeField] GameObject restartText;

    public static bool restartLaunch = false;

    [HideInInspector] public bool pointScored = false;
    [HideInInspector] public bool endGame = false;


    /// <summary>
    /// Reset Paddles and ball position, pause the game until player click any button
    /// </summary>
    public void RestartPosition()
    {
        playerStartPos.GetChild(0).transform.position = playerStartPos.position;
        enemyStartPos.GetChild(0).transform.position = enemyStartPos.position;
        ballStartPos.GetChild(0).transform.position = ballStartPos.position;
    }


    public void EndGame()
    {
        endGame = true;

        restartText.SetActive(true);

        Time.timeScale = 0f;
    }

    private void Restart()
    {
        if (Input.anyKeyDown)
        {
            Time.timeScale = 1f;

            restartText.SetActive(false);

            endGame = false;

            pointsHandler.PlayerPoints = 0;
            pointsHandler.EnemyPoints = 0;
        }
    }

    public void PointScored()
    {
        RestartPosition();

        restartLaunch = true;

        pointScored = false;
    }

    private void Update()
    {
        if (pointScored)
        {
            PointScored();
        }

        if (endGame)
        {
            Restart();
        }
    }
}
