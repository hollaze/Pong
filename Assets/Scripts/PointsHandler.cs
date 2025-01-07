using UnityEngine;
using TMPro;

public class PointsHandler : MonoBehaviour
{
    [SerializeField] private GameState gameState;

    [SerializeField] private TMP_Text playerPointsText;
    [SerializeField] private TMP_Text enemyPointsText;

    [SerializeField] private int maxPoints = 5;
    

    private int playerPoints = 0;

    public int PlayerPoints
    {
        get
        {
            return playerPoints;
        }
        set
        {
            playerPoints = Mathf.Clamp(value, 0, maxPoints);

            playerPointsText.text = playerPoints.ToString();

            // If point == max points, won, restart
            if (playerPoints >= maxPoints)
            {
                gameState.EndGame();

                PlayerPoints = 0;
            }

            gameState.pointScored = true;
        }
    }

    private int enemyPoints = 0;

    public int EnemyPoints
    {
        get
        {
            return enemyPoints;
        }
        set
        {
            enemyPoints = Mathf.Clamp(value, 0, maxPoints);

            enemyPointsText.text = enemyPoints.ToString();

            // If point == max points, lost, restart
            if (enemyPoints >= maxPoints)
            {
                gameState.EndGame();
            }

            gameState.pointScored = true;
        }
    }

    private void Start()
    {
        playerPointsText.text = playerPoints.ToString();
        enemyPointsText.text = enemyPoints.ToString();
    }
}
