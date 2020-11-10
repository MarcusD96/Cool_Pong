
using UnityEngine;

public class Ball : MonoBehaviour {

    public float speedMultiplier, speedBonus, velocityCurrent = 0;
    private float baseSpeed, dt;

    public SpriteRenderer sprite;

    Vector3 direction;

    private bool play = false;

    Camera cam;

    Vector3 lastPos = Vector3.zero;

    public LayerMask mask;

    private float startTime, spawnTime = 5.0f;

    // Start is called before the first frame update
    void Start() {
        if(speedMultiplier <= 0.0f)
            speedMultiplier = 1.0f;

        baseSpeed = speedMultiplier;

        if(speedBonus <= 0.0f)
            speedBonus = 5.0f;

        cam = Camera.main;

        MakeDirection();

        dt = Time.deltaTime;

        lastPos = transform.position;

        startTime = Time.deltaTime;
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) {
            play = true;
            Cursor.visible = false;
        }

        if(Time.unscaledDeltaTime >= startTime + spawnTime)
            play = true;

        if(play == false)
            return;

        //check actual speed
        if(lastPos != Vector3.zero) {
            velocityCurrent = ((transform.position - lastPos) / dt).magnitude;
        }

        lastPos = transform.position;

        CheckBoundaries();
        transform.Translate(direction * dt * speedMultiplier, Space.World);
        CheckCollision();
    }

    void MakeDirection() {
        switch(Random.Range(0, 3)) {
            case 0:
                direction = Vector3.up + Vector3.left;
                break;

            case 1:
                direction = Vector3.down + Vector3.left;
                break;

            case 2:
                direction = Vector3.left;
                break;
        }
        direction.Normalize();
    }

    void CheckBoundaries() {
        float height = cam.orthographicSize;
        float width = height * cam.aspect;

        float spriteSize = sprite.size.x * sprite.transform.localScale.x / 2;


        Vector2 pos = transform.position;

        if(pos.y <= -height + spriteSize)
            direction.y *= -1;
        else if(pos.y >= height - spriteSize)
            direction.y *= -1;

        if(pos.x >= width - spriteSize) { //hit enemy side
            ResetBall();
            GameManager.IncrementScore(true);
            return;
        } else if(pos.x <= -width + spriteSize) { //hit player side
            ResetBall();
            GameManager.IncrementScore(false);
            return;
        }

        pos.x = Mathf.Clamp(pos.x, -width + spriteSize, width - spriteSize);
        pos.y = Mathf.Clamp(pos.y, -height + spriteSize, height - spriteSize);

        transform.position = pos;
    }

    void CheckCollision() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1.0f, mask);
        if(hit) {
            if(Vector3.Distance(hit.point, transform.position) < sprite.size.x) {
                var hitDiff = hit.point.y - hit.collider.transform.position.y;

                float end = 1.0f;
                float topMiddle = end - 0.5f;
                float botMiddle = -end + 0.5f;
                float bot = -end;

                //hits player
                if(direction.x < 0) {
                    //choose return direction
                    if(hitDiff >= end) {
                        direction = (Vector3.up * 1.5f) + Vector3.right;
                    } else if(hitDiff >= topMiddle) {
                        direction = Vector3.up + Vector3.right;
                    } else if(hitDiff > botMiddle) {
                        direction.x *= -1;
                    } else if(hitDiff <= botMiddle) {
                        direction = Vector3.down + Vector3.right;
                    } else if(hitDiff <= bot) {
                        direction = (Vector3.down * 1.5f) + Vector3.right;
                    }

                    //increase ball speed
                    if(speedMultiplier < 2.0f) {
                        speedMultiplier += speedBonus / 100.0f;
                    }
                }

                //hits enemy
                else {
                    //choose return direction
                    if(hitDiff >= end) { //top region
                        direction = (Vector3.up * 1.5f) + Vector3.left;
                    } else if(hitDiff >= topMiddle) {
                        direction = Vector3.up + Vector3.left;
                    } else if(hitDiff > botMiddle) {
                        direction.x *= -1;
                    } else if(hitDiff <= botMiddle) {
                        direction = Vector3.down + Vector3.left;
                    } else if(hitDiff <= bot) {
                        direction = (Vector3.down * 1.5f) + Vector3.left;
                    }
                }

                direction.Normalize();
            }
        }
    }

    void ResetBall() {
        if(GameManager.ModifyBall_Static(this, true)) {
            return;
        }
        transform.position = Vector3.zero;
        velocityCurrent = 0.0f;
        MakeDirection();
        speedMultiplier = baseSpeed;
        play = false;
        startTime = Time.deltaTime;
        Cursor.visible = true;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, direction);
    }
}