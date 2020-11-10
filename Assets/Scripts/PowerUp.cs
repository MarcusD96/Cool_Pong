
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public SpriteRenderer sprite;
    float spriteSize;

    public BoxCollider2D col;

    public float speed;
    
    protected void Update() {
        spriteSize = sprite.size.x * sprite.transform.localScale.x;
        col.size = new Vector2(spriteSize, spriteSize);
        transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
    }
}
