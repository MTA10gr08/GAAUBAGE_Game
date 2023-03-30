using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class ImageSegmenter : MonoBehaviour
{
    public Button FinishBTN;
    public SegmentContainer SegmentContainerPrefab = null;
    [SerializeField] SpriteFromURL spriteFromURL = null;
    private string URL = string.Empty;
    private SegmentContainer currentSegmentContainer = null;
    private List<SegmentContainer> segmentContainers = new List<SegmentContainer>();
    private int CurrentImageId;
    //private int CurrentUserId;
    public UnityEvent UploadedSegment;
    //private int AmountOfAnnotatedImages = 0;
    //[SerializeField] StatsPopulator statsPopulator;


    private void Start() {
        NewImage();
    }

    public void AddOrContinueSegment(Vector2 pos) {
        if (!currentSegmentContainer) {
            currentSegmentContainer = Instantiate(SegmentContainerPrefab, transform);
            currentSegmentContainer.imageSegmenter = this;
            segmentContainers.Add(currentSegmentContainer);
        }
        currentSegmentContainer.AddPoint(pos);
    }

    public void FinishSegment() {
        currentSegmentContainer = null;

    }

    public void EnableSubmission() {
        FinishBTN.interactable = true;
    }

    [UnityEngine.ContextMenu("NewImage")]
    public void NewImage() {
        StartCoroutine(GetImage());
        foreach (var item in segmentContainers) {
            if (item)
                Destroy(item.gameObject);
        }
        segmentContainers.Clear();

        //string URL = DatabaseTest.GetNewImage(ref CurrentImageID, TestManager.Instance.GetCurrentTest().imageSet, TestManager.Instance.UserID, TestManager.Instance.GetCurrentTest().annotationSet);
        
    }

    [UnityEngine.ContextMenu("AddCurrentSegmentationsToDatabase")]
    public async Task AddCurrentSegmentationsToDatabaseAsync() {
        if (segmentContainers.Count <= 0 || segmentContainers.TakeWhile(x => x != null).Any(x => x.imageCatagory == null)) {
            return;
        }
        List<Task> tasks = new List<Task>();
        foreach (var item in segmentContainers) {
            if (!item)
                continue;

            tasks.Add(ApiHelper.PostAsync("Annotations", new AnnotationModel {
                imageMetaId = CurrentImageId,
                userId = PlayerPrefs.GetInt("UserID"),
                segmentation = item.points.Select(x => new SimplePoint() {X = x.pos.x, Y = x.pos.y }).ToList(),
                annotationLabel = item.imageCatagory.name

            }));
            Debug.Log("Done with an item");
        }
        Debug.Log("Done with All the Items");
        await Task.WhenAll(tasks);
        Debug.Log("Done with Tasks");
        var user = await ApiHelper.GetAsync<UserModel>("Users/" + PlayerPrefs.GetInt("UserID"));
        //PlayerPrefs.SetInt("StoryProgress", PlayerPrefs.GetInt("StoryProgress")+1);
        user.energySpent++;
        user = await ApiHelper.PutAsync("Users/" + user.id, user);
        if (PlayerPrefs.GetInt("StoryProgress") >= 42) {
            SceneManager.LoadScene("FreePlayMenu");
        } else {
            //Debug.Log("going to Story");
            SceneManager.LoadScene("StoryScene");
        }
    }
    public void awdawd() {
        _ = AddCurrentSegmentationsToDatabaseAsync();
    }
    IEnumerator GetImage() {
        var user = ApiHelper.GetAsync<UserModel>("Users/" + PlayerPrefs.GetInt("UserID"));
        yield return new WaitUntil(() => user.IsCompleted);
        //CurrentUserId = user.Result;
        Debug.Log("Tivoli");
        //Debug.Log("ImageMeta/" + user.Result.storyProgress);

        var task = ApiHelper.GetAsync<ImageMetaModel>("ImageMetas/" + (user.Result.energySpent+1));
        yield return new WaitUntil(() => task.IsCompleted);
        CurrentImageId = task.Result.id;
        URL = ApiHelper.APIURL + "images/" + task.Result.FileName;
        Debug.Log(URL);
        spriteFromURL.LoadImage(URL);
        //task.Result;
    }
}
