

using System.Collections;
using UnityEngine;

public class ControlsSwap : PowerUp {

    Player p;

    void Awake() {
        p = FindObjectOfType<Player>();
        speed = 5;
    }

    IEnumerator InvertControls() {
        sprite.sprite = null;
        p.speed = -p.speed;
        yield return new WaitForSeconds(3.0f);
        p.speed = -p.speed;
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collision) {
        StartCoroutine(InvertControls());
    }
}
