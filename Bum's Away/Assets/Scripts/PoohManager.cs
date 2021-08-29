using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoohManager : MonoBehaviour
{
    #region Singleton
    private static PoohManager _instance;

    public static PoohManager Instance => _instance;

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
    [SerializeField]
    public Pooh poohPrefab;

    private float POOH_DROP_SPEED = -50.0f;//-0.5f;

    // Start is called before the first frame update
    private List<Pooh> Poohs { get; set; }
    private List<Rigidbody2D> PoohRbs { get; set; }

    private void Start()
    {
        Poohs = new List<Pooh>();
        PoohRbs = new List<Rigidbody2D>();
    }

    public void SpawnPooh(Vector3 position, float x_speed)
    {
        if (poohPrefab != null)
        {
            float rotation = UnityEngine.Random.Range(-0.01f, 0.01f);
            Pooh spawnedPooh = Instantiate(poohPrefab, position, Quaternion.identity);
            Rigidbody2D spawnedPoohRb = spawnedPooh.GetComponent<Rigidbody2D>();
            spawnedPoohRb.isKinematic = false;
            spawnedPoohRb.angularVelocity = 1.0f;
            spawnedPoohRb.inertia = 0.0f;
            spawnedPoohRb.AddForce(new Vector2(x_speed, POOH_DROP_SPEED));
            spawnedPoohRb.AddTorque(rotation);
            //            Poohs.Add(spawnedPooh);
        }
        else
        {
            Debug.Log("SpawnPooh - poohPrefab is NULL!!");
        }
    }

    public void DestroyPoohs()
    {
        int numPoohs = Poohs.Count - 1;
        for (int idx = numPoohs; idx >= 0; idx--)
        {
            var pooh = Poohs[idx];
            Destroy(pooh.gameObject);
        }
    }
}
