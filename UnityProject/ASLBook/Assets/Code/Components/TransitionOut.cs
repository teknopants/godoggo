using UnityEngine;

public class TransitionOut : MonoBehaviour
{
    public float CurrentTime;
    public float EndTime = .5f;
    public float T { get { return CurrentTime / EndTime; } }

    private void Update()
    {
        CurrentTime += Time.deltaTime;
        CurrentTime = Mathf.Clamp(CurrentTime, 0, EndTime);
    }
}
