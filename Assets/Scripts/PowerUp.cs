
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public SpriteRenderer sprite;
    float spriteSize;

    public BoxCollider2D col;

    protected float speed;

    Vector3 dir;

    void Start() {
        if(!GameMode.isSingle) {
            int rand = Random.Range(0, 2);
            if(rand == 1) {
                dir = Vector3.left;
            } else
                dir = Vector3.right;
        } else
            dir = Vector3.left;
    }

    protected void Update() {
        spriteSize = sprite.size.x * sprite.transform.localScale.x;

        col.size = new Vector2(spriteSize, spriteSize);

        transform.Translate(dir.normalized * Time.deltaTime * speed, Space.World);
    }
}
