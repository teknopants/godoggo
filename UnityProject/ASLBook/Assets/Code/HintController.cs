using UnityEngine;

public class HintController : MonoBehaviour
{
    public GameObject HintEffectPrefab;
    public Transform[] Hints;
    private int HintIndex = 0;
    public float CurrentTime = 0;
    private float HintInterval = 4;

    void Start()
    {
        ResetInterval();
        var children = transform.GetComponentsInChildren<Transform>();
        Hints = new Transform[children.Length - 1];
        for (var i = 1; i < children.Length; i++)
        {
            Hints[i - 1] = children[i];
        }
    }

    void ResetInterval()
    {
        HintInterval = Random.Range(4, 6);
        CurrentTime = 0;
    }

    void Update()
    {
        CurrentTime += Time.deltaTime;
        if (CurrentTime > HintInterval)
        {
            ResetInterval();
            HintIndex += 1;
            if (HintIndex >= Hints.Length)
            {
                HintIndex = 0;
            }

            var currentHint = Hints[HintIndex];
            Instantiate(HintEffectPrefab, currentHint.transform.position, Quaternion.identity);
        }
    }
}
