
using UnityEngine;

public class Ball : MonoBehaviour {

    public float speed, speedMultiplier;
    private float baseSpeed;

    private int playerScore, enemyScore;

    public SpriteRenderer sprite;

    Vector3 direction;

    private bool play = false;

    // Start is called before the first frame update
    void Start() {
        if(speed <= 0.0f)
            speed = 2.0f;

        baseSpeed = speed;

        if(speedMultiplier <= 0.0f)
            speedMultiplier = 5.0f;

        MakeDirection();

        playerScore = enemyScore = 0;
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            play = true;
            Cursor.visible = false;
        }

        if(play == false)
            return;

        CheckBoundaries();
        transform.position += direction * speed * Time.deltaTime;
    }

    void MakeDirection() {
        switch(Random.Range(0, 4)) {
            case 0:
                direction = Vector2.up + Vector2.left;
                break;

            case 1:
                direction = Vector2.up + Vector2.right;
                break;

            case 2:
                direction = Vector2.down + Vector2.left;
                break;

            case 3:
                direction = Vector2.down + Vector2.right;
                break;

            default:
                break;
        }

    }

    void CheckBoundaries() {
        Camera cam = Camera.main;

        float height = cam.orthographicSize;
        float width = height * cam.aspect;

        float spriteX = sprite.size.x * sprite.transform.localScale.x / 2;
        float spriteY = sprite.size.y * sprite.transform.localScale.y / 2;

        Vector2 pos = transform.position;

        if(transform.position.y <= -height + spriteY)
            direction.y *= -1;
        else if(transform.position.y >= height - spriteY)
            direction.y *= -1;

        if(transform.position.x >= width - spriteX) { //hit enemy side
            ResetBall();
            playerScore += 1;
            return;
        } else if(transform.position.x <= -width + spriteX) { //hit player side
            ResetBall();
            enemyScore += 1;
            return;
        }

        pos.x = Mathf.Clamp(pos.x, -width + spriteX, width - spriteX);
        pos.y = Mathf.Clamp(pos.y, -height + spriteY, height - spriteY);

        transform.position = pos;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        direction.x *= -1;
        speed *= 1 + (speedMultiplier / 100);
    }

    public float GetSpeed() {
        return speed;
    }

    public int GetPlayerScore() {
        return playerScore;
    }

    public int  GetEnemyScore() {
        return enemyScore;
    }

    void ResetBall() {
        transform.position = Vector2.zero;
        MakeDirection();
        speed = baseSpeed;
        play = false;
        Cursor.visible = true;
    }
}