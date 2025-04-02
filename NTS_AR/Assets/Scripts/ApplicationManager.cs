using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    public List<GameObject> PrefabsEnnemys;
    public Transform camTransform;
    public int MaxEnemyNumber = 10;
    private int EnemyNumber = 0;
    public float SpawnRange = 3f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy(MaxEnemyNumber);
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyNumber < MaxEnemyNumber){
            int a = MaxEnemyNumber-EnemyNumber;
            SpawnEnemy(a);
        }
    }
    public void SpawnEnemy(int needSpawn)
    {
        for (int i = 0; i < needSpawn; i++)
        {
            EnemyNumber += 1;
            float x = camTransform.position.x + Random.Range(-SpawnRange, SpawnRange);
            float y = camTransform.position.y + Random.Range(-SpawnRange, SpawnRange);
            float z = camTransform.position.z + Random.Range(-SpawnRange, SpawnRange);
            Vector3 spawnPos = new Vector3(x, y, z);

            int ennemyPrefInd = Random.Range(PrefabsEnnemys.Count, 0);
            Instantiate(PrefabsEnnemys[ennemyPrefInd], spawnPos, Quaternion.identity);
        }
    }

}
