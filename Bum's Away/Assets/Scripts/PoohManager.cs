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
    public Pooh[] poohPrefab;

    private float POOH_DROP_SPEED = -50.0f;//-0.5f;

    private bool poohActive = false;

    // Start is called before the first frame update

    private void Start()
    {
    }

    public void SpawnPooh(Vector3 position, float x_speed)
    {
        int rand = UnityEngine.Random.Range(0, poohPrefab.Length);
        if (poohActive == false)
        {
            if (poohPrefab[rand] != null)
            {
                float rotation = UnityEngine.Random.Range(-0.06f, 0.06f);
                Pooh spawnedPooh = Instantiate(poohPrefab[rand], position, Quaternion.identity);
                Rigidbody2D spawnedPoohRb = spawnedPooh.GetComponent<Rigidbody2D>();
                spawnedPoohRb.isKinematic = false;
                spawnedPoohRb.angularVelocity = 1.0f;
                spawnedPoohRb.inertia = 0.0f;
                spawnedPoohRb.AddForce(new Vector2(x_speed, POOH_DROP_SPEED));
                spawnedPoohRb.AddTorque(rotation);
                poohActive = true;
                Pooh.OnPoohDeath += OnPoohDeath;

            }
            else
            {
                Debug.Log("SpawnPooh - poohPrefab is NULL!!");
            }
        }
    }
    private void OnPoohDeath(Pooh obj)
    {
        poohActive = false;
    }

    private void OnDestroy()
    {
        Pooh.OnPoohDeath -= OnPoohDeath;

    }
}
