
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed;
    public SpriteRenderer sprite;

    private float offset = 0.5f, dt;
    private Vector2 pos;

    private Ball currentBall;
    private List<Ball> balls;
    void Awake() {
        if(speed <= 0)
            speed = 3;

        pos = transform.position;
    }

    void Start() {
        dt = Time.deltaTime;        
    }

    void FixedUpdate() {
        FollowBall();
    }

    void Update() {
        CheckBoundaries();
    }

    void FollowBall() {
        float posY = currentBall.transform.position.y;

        if(posY > transform.position.y + offset) //move up
            pos.y += speed * dt;

        else if(posY < transform.position.y - offset) //move down
            pos.y -= speed * dt;

        else
            pos.y = Mathf.Lerp(pos.y, posY, speed * dt);

        transform.position = pos;
    }

    void CheckBoundaries() {
        float size = Camera.main.orthographicSize;
        float spriteLen = (sprite.size.x * sprite.transform.localScale.x) / 2;

        pos.y = Mathf.Clamp(pos.y, -size + spriteLen, size - spriteLen);

        transform.position = pos;
    }

    public void SetCurrentBall(Ball ball_) {
        currentBall = ball_;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position + Vector3.up, Vector3.left);
        Gizmos.DrawRay(transform.position + Vector3.up / 2, Vector3.left);
        Gizmos.DrawRay(transform.position, Vector3.left);
        Gizmos.DrawRay(transform.position + Vector3.down / 2, Vector3.left);
        Gizmos.DrawRay(transform.position + Vector3.down, Vector3.left);
    }
}
