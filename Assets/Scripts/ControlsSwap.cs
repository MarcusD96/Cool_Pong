

using System.Collections;
using UnityEngine;

public class ControlsSwap : PowerUp {

    Player p;

    void Awake() {
        p = FindObjectOfType<Player>();
        speed = 7;
    }

    IEnumerator InvertControls() {
        sprite.sprite = null;
        speed = 0;
        col.enabled = false;
        p.ReverseSpeed();
        yield return new WaitForSeconds(5.0f);
        p.ReverseSpeed();
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        StartCoroutine(InvertControls());
    }
}
