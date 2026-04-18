using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [Header("Prefabs de comida")]
    public GameObject[] foodPrefabs;

    [Header("Configuración")]
    public int maxFood = 15;
    public float range = 10f;

    private int currentFood = 0;

    public Transform floor;

    void Start()
    {
        Random.InitState(12345);

        for (int i = 0; i < maxFood; i++)
        {
            SpawnFood();
        }
    }

    public void SpawnFood()
    {
        if (currentFood >= maxFood) return;

        Vector3 scale = floor.localScale;

        float x = Random.Range(-scale.x * 5f, scale.x * 5f);
        float z = Random.Range(-scale.z * 5f, scale.z * 5f);

        Vector3 pos = new Vector3(x, 0.5f, z) + floor.position;

        GameObject randomFood = foodPrefabs[Random.Range(0, foodPrefabs.Length)];

        Instantiate(randomFood, pos, Quaternion.identity);

        currentFood++;
    }

    public void FoodEaten()
    {
        currentFood--;
    }
}