
using UnityEngine;

public class MultiBall : PowerUp {

    private GameManager gm;

    public Ball ballPrefab;

    void Awake() {
        gm = FindObjectOfType<GameManager>();
        speed = 4;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        gm.ModifyBall(Instantiate(ballPrefab, Vector3.zero, Quaternion.identity), false);
        Destroy(gameObject);
    }
}
