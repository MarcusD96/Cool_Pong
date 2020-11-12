
using UnityEngine;

public class MultiBall : PowerUp {

    private GameManager gm;

    public Ball ballPrefab;

    void Awake() {
        gm = FindObjectOfType<GameManager>();
        speed = 4;
    }

    new void Update() {
        base.Update();
        CheckDie();
    }

    void CheckDie() {
        if(transform.position.x <= -8) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        gm.ModifyBall(Instantiate(ballPrefab, Vector3.zero, Quaternion.identity), false);
        Destroy(gameObject);
    }
}
