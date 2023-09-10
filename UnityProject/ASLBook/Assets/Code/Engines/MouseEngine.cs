using UnityEngine;
using UnityEngine.Video;

public class MouseEngine : MonoBehaviour
{
    public Vector2 MousePosition;
    public float mouseZOffset= 0;
    public GameObject HighlightedObject = null;
    public GameObject LastHighlightedObject = null;
    public GameObject VideoImage;
    private VideoPlayer VideoPlayer;
    public int CurrentPage = 0;
    public GameObject[] Pages;
    public GameObject NextPageGraphic;
    public GameObject PreviousPageGraphic;
    public float ArrowSizeNormal = 250;
    public float ArrowSizeHover = 300;
    public bool ShowingVideo = false;
    public bool playMainVideo = false;

    void Start()
    {
        VideoPlayer = FindObjectOfType<VideoPlayer>();
    }

    private void Update()
    {
        // This removes the video UI when it is done playing
        if (ShowingVideo && VideoPlayer.frame >= (long) VideoPlayer.frameCount - 1 && VideoPlayer.frameCount > 0)
        {
            HideVideo();
        }
        
        NextPageGraphic.GetComponent<ScaleGoal>().Goal = ArrowSizeNormal;
        PreviousPageGraphic.GetComponent<ScaleGoal>().Goal = ArrowSizeNormal;

        PreviousPageGraphic.SetActive(CurrentPage > 0 && !ShowingVideo);
        NextPageGraphic.SetActive(CurrentPage < (Pages.Length - 1) && !ShowingVideo);

        var videoRect = VideoImage.GetComponent<RectTransform>();
        if (Pages[CurrentPage].TryGetComponent<VideoPosition>(out var videoPosition))
        {
            Debug.Log("found video position");
            var gotoRect = videoPosition.VideoPositionObject.GetComponent<RectTransform>();
            videoRect.position = gotoRect.position;
            videoRect.localScale = gotoRect.localScale;
        }

        if (HighlightedObject)
        {
            var showHighlight = !ShowingVideo;

            // Previous Page
            if (HighlightedObject.TryGetComponent<GoToPreviousPageOnClick>(out var _))
            {
                PreviousPageGraphic.GetComponent<ScaleGoal>().Goal = ArrowSizeHover;
                if (CurrentPage == 0)
                {
                    showHighlight = false;
                }
            }

            // Next Page
            if (HighlightedObject.TryGetComponent<GoToNextPageOnClick>(out var _))
            {
                NextPageGraphic.GetComponent<ScaleGoal>().Goal = ArrowSizeHover;
                if (CurrentPage == Pages.Length - 1)
                {
                    showHighlight = false;
                }
            }

            if (showHighlight)
            {
                if (HighlightedObject != LastHighlightedObject)
                {
                    LastHighlightedObject = HighlightedObject;
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            // If you click when video is playing, it stops the video and removes the UI
            if (ShowingVideo)
            {
                HideVideo();
            }
            else
            {
                if (HighlightedObject)
                {
                    Debug.Log("Clicked " + HighlightedObject.name);

                    // Previous Page
                    if (HighlightedObject.TryGetComponent<GoToPreviousPageOnClick>(out var _))
                    {
                        if (CurrentPage > 0)
                        {
                            Pages[CurrentPage].SetActive(false);
                            CurrentPage -= 1;
                            Pages[CurrentPage].SetActive(true);
                            Pages[CurrentPage].transform.position = Vector3.zero;
                        }
                    }

                    // Next Page
                    if (HighlightedObject.TryGetComponent<GoToNextPageOnClick>(out var _))
                    {
                        if (CurrentPage < Pages.Length - 1)
                        {
                            Pages[CurrentPage].SetActive(false);
                            CurrentPage += 1;
                            Pages[CurrentPage].SetActive(true);
                            Pages[CurrentPage].transform.position = Vector3.zero;
                        }
                    }

                    // Show video
                    if (HighlightedObject.TryGetComponent<PlayVideoOnClick>(out var playVideoOnClick))
                    {
                        ShowVideo(playVideoOnClick.URL);
                    }
                }
            }
        }
    }

    private void ShowVideo(string url)
    {
        ShowingVideo = true;
        VideoImage.SetActive(true);
        VideoPlayer.url = url;
        VideoPlayer.time = 0;
        VideoPlayer.frame = 0;
        VideoPlayer.Prepare();
        VideoPlayer.Play();
    }

    private void HideVideo()
    {
        ShowingVideo = false;
        VideoImage.SetActive(false);
    }

    void FixedUpdate()
    {
        MousePosition = Input.mousePosition;

        Ray castPoint = Camera.main.ScreenPointToRay(MousePosition);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity) && !ShowingVideo)
        {
            HighlightedObject = hit.transform.gameObject;
        }
        else
        {
            HighlightedObject = null;
            LastHighlightedObject = null;
        }
    }
}
