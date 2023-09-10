using UnityEngine;

public class PlayVideoOnClick : MonoBehaviour
{
    public string VideoName;
    [HideInInspector]
    public string URL;

    void Start()
    {
        URL = System.IO.Path.Combine(Application.streamingAssetsPath, VideoName + ".mp4");
        if (!System.IO.File.Exists(URL))
        {
            Debug.LogError(gameObject.name + " video '" + VideoName + "' does not exist");
        }
    }
}
