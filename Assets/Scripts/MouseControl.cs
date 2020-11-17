using System.Collections;
using UnityEngine;

public class MouseControl : PowerUp {

    Player p;

    void Awake() {
        p = FindObjectOfType<Player>();
        speed = 2;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        StartCoroutine(UseMouse());
    }

    IEnumerator UseMouse() {
        sprite.sprite = null;
        speed = 0;
        col.enabled = false;
        p.ToggleMouseControl();
        yield return new WaitForSeconds(5.0f);
        p.ToggleMouseControl();
        Destroy(gameObject);
    }
}
