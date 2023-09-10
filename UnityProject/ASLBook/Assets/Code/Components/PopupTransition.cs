using UnityEngine;

public class PopupTransition : MonoBehaviour
{
    public float InY = 0;
    public float OutY = -625;
    private RectTransform RectTransform;

    private void Start()
    {
        RectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (TryGetComponent<TransitionIn>(out var transitionIn))
        {
            //var y = Mathf.Lerp(OutY, InY, EaseInOutQuart(transitionIn.T));
            //RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, y);

            if (transitionIn.T == 1)
            {
                Destroy(transitionIn);
            }
        }

        if (TryGetComponent<TransitionOut>(out var transitionOut))
        {
            //var y = Mathf.Lerp(InY, OutY, EaseInOutQuart(transitionOut.T));
            //RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, y);

            if (transitionOut.T == 1)
            {
                Destroy(transitionOut);
                GameObject.FindFirstObjectByType<MouseEngine>().ShowingVideo = false;
            }
        }
    }

    float EaseInOutQuart(float x)
    {
        return x < 0.5 ? 8 * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 4) / 2;
    }
}
