using UnityEngine;

public class Spawnprefab : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    [SerializeField] private Vector3 spawnPos;

    [SerializeField] private bool random;

    [SerializeField] private float spawnTime = 10; 
    
    private float timer;

    private void Awake()
    {
        onPrefabSpawn();
    }

    private void Update()
    {
        timer += Time.time;
        if(timer >= spawnTime)
        {
            onPrefabSpawn();
        }
    }

    public void onPrefabSpawn()
    {
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
