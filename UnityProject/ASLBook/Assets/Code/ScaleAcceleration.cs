using UnityEngine;

public class ScaleAcceleration : MonoBehaviour
{
    public float Value = 0;
    private float Speed = 0;
    public float Factor = 0.25f;
    public float Friction = .9f;
    public float XFactor = 1;

    void Update()
    {
        if (!TryGetComponent<ScaleGoal>(out var scaleGoal)) return;

        Speed *= Friction;
        Speed += (scaleGoal.Goal - Value) * Factor;
        Value += Speed;

        transform.localScale = new Vector3(Value * XFactor, Value, .1f);
    }
}
