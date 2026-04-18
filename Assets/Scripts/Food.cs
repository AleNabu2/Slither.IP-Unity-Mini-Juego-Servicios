using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour
{
    public int value = 1;
    private FoodSpawner spawner;

    void Start()
    {
        spawner = FindObjectOfType<FoodSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerScore score = other.GetComponent<PlayerScore>();

        if (score != null)
        {
            score.AddScore(value);

            SnakeBody snake = other.GetComponent<SnakeBody>();

            if (snake != null)
            {
                snake.AddSegment();
            }

            if (spawner != null)
            {
                spawner.FoodEaten();
                spawner.SpawnFood();
            }

            StartCoroutine(DisableFood());
        }
    }

    IEnumerator DisableFood()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }
}