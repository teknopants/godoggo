using UnityEngine;

public class SetVideoURL : MonoBehaviour
{
    void Start()
    {
        GetComponent<UnityEngine.Video.VideoPlayer>().url = System.IO.Path.Combine(Application.streamingAssetsPath, "dog.mp4");
    }
}
