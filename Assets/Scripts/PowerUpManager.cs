
using UnityEngine;
using Mirror;

public class PowerUpManager : NetworkBehaviour {

    RectTransform rt;

    public MultiBall mbPrefab;
    public ControlsSwap csPrefab;
    public MouseControl mcPrefab;

    void Awake() {
        rt = GetComponent<RectTransform>();
        InvokeRepeating(nameof(PickRandomPU), 0, 5);
    }

    void SpawnMultiBall() {
        Instantiate(mbPrefab, RandomSpawnPoint(), Quaternion.identity);
    }

    void SpawnControlsSwap() {
        Instantiate(csPrefab, RandomSpawnPoint(), Quaternion.identity);
    }

    void SpawnMouseControl() {
        Instantiate(mcPrefab, RandomSpawnPoint(), Quaternion.identity);
    }

    Vector3 RandomSpawnPoint() {
        float randX = Random.Range(rt.rect.xMin, rt.rect.xMax);
        float randY = Random.Range(rt.rect.yMin, rt.rect.yMax);

        return new Vector3(randX, randY, 0);
    }
     
    void PickRandomPU() {
        if(!Player.started)
            return;

        switch(Random.Range(0, 3)) {
            case 0:
                SpawnMultiBall();
                break;
            case 1:
                SpawnMouseControl();
                break;
            case 2:
                SpawnControlsSwap();
                break;
        }
    }
}
