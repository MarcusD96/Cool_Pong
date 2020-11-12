
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour {

    public float speed, mouseSpeed;
    public SpriteRenderer sprite;

    private float offset = 1.0f, dt;
    private Vector2 pos;

    Camera cam;

    private bool mouseControl = false;

    public static bool started = false;

    void Awake() {
        pos = transform.position;

        if(speed <= 0)
            speed = 3;
        if(offset <= 0)
            offset = 0.5f;
        mouseSpeed = speed * 8;
        cam = Camera.main;
    }

    void Start() {
        started = true;
        dt = Time.deltaTime;
    }

    void FixedUpdate() {
        //FollowMouseOnY();
    }


    void Update() {
        //single player
        if(GameMode.isSingle) {
            MoveVertically();
            CheckBoundaries();
            return;
        } 
        
        //multiplayer
        if(isLocalPlayer) {
            MoveVertically();
            CheckBoundaries();
        }
    }

    void FollowMouseOnY() {
        if(!mouseControl)
            return;

        float targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        if(targetPos > transform.position.y + offset) //move up
            transform.Translate(Vector3.up * mouseSpeed * dt, Space.World);

        else if(targetPos < transform.position.y - offset) //move down
            transform.Translate(Vector3.down * mouseSpeed * dt, Space.World);

        else {
            pos.y = Mathf.Lerp(pos.y, targetPos, mouseSpeed * dt);
            transform.position = pos;
            return;
        }
        pos = transform.position;
    }

    void MoveVertically() {
        if(mouseControl)
            return;

        if(Input.GetKey(KeyCode.W)) {
            transform.Translate(Vector3.up * speed * dt, Space.World);
        }
        if(Input.GetKey(KeyCode.S)) {
            transform.Translate(Vector3.down * speed * dt, Space.World);
        }
        pos = transform.position;
    }

    void CheckBoundaries() {
        float size = cam.orthographicSize;
        float spriteLen = (sprite.size.x * sprite.transform.localScale.x) / 2;

        pos.y = Mathf.Clamp(pos.y, -size + spriteLen, size - spriteLen);

        transform.position = pos;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position + Vector3.up, Vector3.right);
        Gizmos.DrawRay(transform.position + Vector3.up / 2, Vector3.right);
        Gizmos.DrawRay(transform.position, Vector3.right);
        Gizmos.DrawRay(transform.position + Vector3.down / 2, Vector3.right);
        Gizmos.DrawRay(transform.position + Vector3.down, Vector3.right);
    }
}
