using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    #region Singleton
    private static CloudManager _instance;
    public static CloudManager Instance => _instance;


    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public List<Cloud> cloudList; // List of created clouds
    public Cloud cloudPrefab;

    private int MAX_CLOUDS = 12;

    // Start is called before the first frame update
    void Start()
    {
        cloudList = new List<Cloud>();
        Cloud.OnCloudDeath += OnCloudDeath;
    }

    void Update()
    {
        if (cloudList.Count < MAX_CLOUDS)
        {
            int rand = UnityEngine.Random.Range(0, 1000);
            if (rand == 0)
            {
                Cloud cloud = Instantiate(cloudPrefab, new Vector3(0, 0, 0), Quaternion.identity) as Cloud;
                cloudList.Add(cloud);
                cloud.MakeCloud(UnityEngine.Random.Range(2, cloud.MAX_CLOUD_NUM - 4));
            }
        }
    }

    public void CreateStartClouds()
    {
        int rand = UnityEngine.Random.Range(1, 3);
        int idx;
        for (idx = 0; idx < rand; idx++)
        {
            Cloud cloud = Instantiate(cloudPrefab, new Vector3(0, 0, 0), Quaternion.identity) as Cloud;
            cloudList.Add(cloud);
            cloud.MakeCloud(UnityEngine.Random.Range(2, cloud.MAX_CLOUD_NUM - 4));
        }
    }

    public void OnCloudDeath(Cloud cloud)
    {
        cloudList.Remove(cloud);
    }

    public void RemoveClouds()
    {
        int idx;
        for (idx = cloudList.Count; idx > 0; idx--)
        {
            cloudList.RemoveAt(idx);
        }
    }

    private void OnDisable()
    {
        Cloud.OnCloudDeath -= OnCloudDeath;
    }
}

