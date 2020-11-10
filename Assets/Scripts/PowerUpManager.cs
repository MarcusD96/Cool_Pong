
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    RectTransform rt;

    public MultiBall mbPrefab;


    void Awake() {
        rt = GetComponent<RectTransform>();
        InvokeRepeating(nameof(SpawnMultiBall), 0, 5);
    }

    void SpawnMultiBall() {
        Instantiate(mbPrefab, RandomSpawnPoint(), Quaternion.identity);
    }

    Vector3 RandomSpawnPoint() {
        float randX = Random.Range(rt.rect.xMin, rt.rect.xMax);
        float randY = Random.Range(rt.rect.yMin, rt.rect.yMax);

        return new Vector3(randX, randY, 0);
    }
}
