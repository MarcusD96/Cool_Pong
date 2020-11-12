
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    RectTransform rt;

    public MultiBall mbPrefab;
    public ControlsSwap csPrefab;


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

    Vector3 RandomSpawnPoint() {
        float randX = Random.Range(rt.rect.xMin, rt.rect.xMax);
        float randY = Random.Range(rt.rect.yMin, rt.rect.yMax);

        return new Vector3(randX, randY, 0);
    }

    void PickRandomPU() {
        if(!Player.started)
            return;

        int n = Random.Range(0, 4);
        if(n < 1) {
            SpawnControlsSwap();
        } else
            SpawnMultiBall();
    }
}
