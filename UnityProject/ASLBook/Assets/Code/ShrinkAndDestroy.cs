using UnityEngine;

public class ShrinkAndDestroy : MonoBehaviour
{
    private Vector3 startScale;
    private float CurrentTime = 0;
    private float EndTime = 0.25f;

    private void Start()
    {
        var scale = transform.localScale;
        startScale = new Vector3(scale.x * Flip(), scale.y * Flip(), 1);
        transform.localScale = startScale;
        transform.Rotate(new Vector3(0, 0, Random.value * 360));
    }

    void Update()
    {
        var t = CurrentTime / EndTime;
        transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);
        CurrentTime += Time.deltaTime;

        if (CurrentTime >= EndTime)
        {
            Destroy(gameObject);
        }
    }

    float Flip()
    {
        return Random.value >= 0.5f ? 1 : -1;
    }
}
