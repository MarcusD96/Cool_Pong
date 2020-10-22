
using UnityEngine;

public class Paddle : MonoBehaviour {

    public float speed;
    public SpriteRenderer sprite;

    private float offset = 0.2f;
    private Vector2 pos;

    void Awake() {
        pos = transform.position;
        if(speed <= 0)
            speed = 3;
        if(offset <= 0)
            offset = 0.5f;
    }

    void FixedUpdate() {
        FollowMouseOnY();
    }


    void Update() {
        CheckBoundaries();
    }

    void FollowMouseOnY() {
        float targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        if(targetPos > transform.position.y + offset) //move up
            pos.y += speed * Time.deltaTime;

        else if(targetPos < transform.position.y - offset) //move down
            pos.y -= speed * Time.deltaTime;

        else
            pos.y = Mathf.Lerp(pos.y, targetPos, speed * Time.deltaTime);

        transform.position = pos;
    }

    void CheckBoundaries() {
        float size = Camera.main.orthographicSize;
        float spriteLen = (sprite.size.x * sprite.transform.localScale.x) / 2;

        pos.y = Mathf.Clamp(pos.y, -size + spriteLen, size - spriteLen);

        transform.position = pos;
    }
}
