using UnityEngine;

public class SpawnPrefabTimer : MonoBehaviour
{
    public GameObject Prefab;
    public int Amount = 1;
    public float Interval;
    public Vector2 RandomOffset;

    private float timer = 0;

    private void Start()
    {
        timer = Interval;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > Interval)
        {
            timer -= Interval;
            Instantiate(Prefab, transform.position + new Vector3(Random.Range(-RandomOffset.x, RandomOffset.x), Random.Range(-RandomOffset.y, RandomOffset.y), -1), transform.rotation);
            Amount -= 1;

            if (Amount == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
