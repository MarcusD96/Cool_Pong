
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject enemyPrefab, ballPrefab;

    private GameObject enemy, ball;

    [Header("Spawns")]
    public Transform playerSpawn;
    public Transform enemySpawn, ballSpawn;

    [Header("UI")]
    public TextMeshProUGUI speed;
    public TextMeshProUGUI playerScore, enemyScore;

    private Ball ballComp;
    private Enemy enemyComp;

    void Start() {
        Instantiate(playerPrefab, playerSpawn.position, Quaternion.identity);
        enemy = Instantiate(enemyPrefab, enemySpawn.position, Quaternion.identity);
        ball = Instantiate(ballPrefab, ballSpawn.position, Quaternion.identity);

        ballComp = ball.GetComponent<Ball>();
        if(!ballComp) {
            print("no ball comp");
            UnityEditor.EditorApplication.isPlaying = false;
            return;
        }

        enemyComp = enemy.GetComponent<Enemy>();
        if(!enemyComp) {
            print("no enemy comp");
            UnityEditor.EditorApplication.isPlaying = false;
            return;
        }

        enemyComp.SetBall(ballComp);
    }

    void LateUpdate() {
        speed.text = "Speed: " + ballComp.GetSpeed().ToString("F1") + " units/s";
        playerScore.text = "Player\n" + ballComp.GetPlayerScore();
        enemyScore.text = "Enemy\n" + ballComp.GetEnemyScore();
    }
}