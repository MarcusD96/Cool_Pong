
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Variables
    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject enemyPrefab, ballPrefab;

    private GameObject enemy, ball;

    [Header("Spawns")]
    public Transform playerSpawn;
    public Transform enemySpawn, ballSpawn;

    [Header("UI")]
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI enemyScoreText;
    private static int playerPoints = 0, enemyPoints = 0;

    private List<Ball> balls = new List<Ball>();
    private Enemy enemyComp;

    public float numBalls = 1;

    public static GameManager instance;

    #endregion

    #region Unity
    void Start() {
        instance = this;

        Instantiate(playerPrefab, playerSpawn.position, Quaternion.identity);
        enemy = Instantiate(enemyPrefab, enemySpawn.position, Quaternion.identity);
        ball = Instantiate(ballPrefab, ballSpawn.position, Quaternion.identity);

        balls.Add(ball.GetComponent<Ball>());
        if(!balls[0]) {
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

        enemyComp.SetCurrentBall(balls[0]);
    }

    void FixedUpdate() {
        enemyComp.SetCurrentBall(FindNearestBall());
    }

    void LateUpdate() {
        playerScoreText.text = "Player\n" + playerPoints;
        enemyScoreText.text = "Enemy\n" + enemyPoints;
    }
    #endregion

    public static void IncrementScore(bool isPlayer) {
        if(isPlayer)
            playerPoints++;
        else
            enemyPoints++;
    }

    Ball FindNearestBall() {
        float closestDistance = float.PositiveInfinity;
        Ball closestBall = null;
        foreach(var b in balls) {
            float dist = Vector3.Distance(b.transform.position, enemy.transform.position);
            if(dist < closestDistance) {
                closestDistance = dist;
                closestBall = b;
            } else
                continue;
               
        }
        return closestBall;
    }

    /// <summary>
    /// returns true if ball is removed
    /// </summary>
    public static bool ModifyBall_Static(Ball ball_, bool isRemoved) {
        return instance.ModifyBall(ball_, isRemoved);
    }

    public bool ModifyBall(Ball ball_, bool isRemoved) {
        if(isRemoved) {
            if(numBalls > 1) { //always keep at least 1 ball                
                balls.Remove(ball_);
                Destroy(ball_.gameObject);
                numBalls--;
                enemyComp.SetCurrentBall(balls[0]);
                return true;
            } else
                return false;
        } else {
            balls.Add(ball_);
            numBalls++;
            return false;
        }
    }
}