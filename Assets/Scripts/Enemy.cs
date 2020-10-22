
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed;
    public SpriteRenderer sprite;

    private float offset = 0.2f;
    private Ball ball;
    private Vector2 pos;

    void Awake() {
        if(speed <= 0)
            speed = 3;

        pos = transform.position;
    }

    void FixedUpdate() {
        FollowBall();
    }

    void Update() {
        CheckBoundaries();
    }

    void FollowBall() {
        float posY = ball.transform.position.y;

        if(posY > transform.position.y + offset) //move up
            pos.y += speed * Time.deltaTime;

        else if(posY < transform.position.y - offset) //move down
            pos.y -= speed * Time.deltaTime;

        else
            pos.y = Mathf.Lerp(pos.y, posY, speed * Time.deltaTime);

        transform.position = pos;
    }

    void CheckBoundaries() {
        float size = Camera.main.orthographicSize;
        float spriteLen = (sprite.size.x * sprite.transform.localScale.x) / 2;

        pos.y = Mathf.Clamp(pos.y, -size + spriteLen, size - spriteLen);

        transform.position = pos;
    }

    public void SetBall(Ball ball_) {
        ball = ball_;
    }
}
