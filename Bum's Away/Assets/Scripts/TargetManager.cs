using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    #region Singleton
    private static TargetManager _instance;
    public static TargetManager Instance => _instance;


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

    public List<Target> targetList; // List of created targets
    public List<Target> targetPrefabs;  // List of available target prefabs

    // Start is called before the first frame update
    void Start()
    {
        targetList = new List<Target>();
    }


    public void CreateTarget()
    {
        int rand = UnityEngine.Random.Range(0, targetPrefabs.Count);
        Target newTarget = Instantiate(targetPrefabs[rand], new Vector3(8.6f, -2.1518f, -8.184762f), Quaternion.identity) as Target;
        targetList.Add(newTarget);
    }

    public void DeleteTarget(Target targ)
    {
        targetPrefabs.Remove(targ);
    }
}


