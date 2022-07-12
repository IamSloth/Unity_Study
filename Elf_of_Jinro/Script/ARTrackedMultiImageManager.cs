using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ARTrackedMultiImageManager : MonoBehaviour
{

    public GameObject startMessage;
    public GameObject startImage;

    [SerializeField]
    private GameObject[] trackedPrefabs;

    private Dictionary<string, GameObject> spawnedObjects = new Dictionary<string, GameObject>();
    private ARTrackedImageManager trackedImageManager;


    private void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();

        foreach(GameObject prefab in trackedPrefabs)
        {
            GameObject clone = Instantiate(prefab);
            clone.name = prefab.name;
            clone.SetActive(false);
            spawnedObjects.Add(clone.name, clone);
        }
    }


    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            spawnedObjects[trackedImage.name].SetActive(false);
        }
    }

    bool numSelected = false;
    int imageNum = 3;

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        GameObject trackedObject = spawnedObjects[name];

        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            trackedObject.transform.position = trackedImage.transform.position;
            trackedObject.transform.rotation = trackedImage.transform.rotation;
            trackedObject.SetActive(true);

           if(numSelected == false)
            {
                imageNum = Random.Range(1, 4);
                numSelected = true;
            }

           if(SceneManager.GetActiveScene().name == "withWoman")
            {
                trackedObject.transform.Find("iu" + imageNum.ToString()).gameObject.SetActive(true);
                startMessage.SetActive(false);
                startImage.SetActive(false);
            }

            else if (SceneManager.GetActiveScene().name == "withMan")
            {
                trackedObject.transform.Find("gong" + imageNum.ToString()).gameObject.SetActive(true);
                startMessage.SetActive(false);
                startImage.SetActive(false);
            }

            else if (SceneManager.GetActiveScene().name == "withAnimal")
            {
                trackedObject.transform.Find("ggup" + imageNum.ToString()).gameObject.SetActive(true);
                startMessage.SetActive(false);
                startImage.SetActive(false);
            }

            
        }

        else
        {
            if (SceneManager.GetActiveScene().name == "withWoman")
            {
                trackedObject.transform.Find("iu" + imageNum.ToString()).gameObject.SetActive(false);
            }

            else if(SceneManager.GetActiveScene().name == "withMan")
            {
                trackedObject.transform.Find("gong" + imageNum.ToString()).gameObject.SetActive(false);
            }

            else if( SceneManager.GetActiveScene().name == "withAnimal")
            {
                trackedObject.transform.Find("ggup" + imageNum.ToString()).gameObject.SetActive(false);
            }
                
            trackedObject.SetActive(false);            
            startMessage.SetActive(true);
            startImage.SetActive(true);
            numSelected = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
