using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour {

    public float TimeToNextBoxSpawn;
    public float TimeToNextBurstBoxSpawn;
    public int BurstAmount;

    public bool SpawnerActiv;

    [SerializeField]
    private GameObject[] BoxPrefabs;
    


    [SerializeField]
    private Transform[] SpawnPoints;

    // Use this for initialization
    void Start () {

        SpawnerActiv = true;
      ;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


   public IEnumerator Spawner()
    {
        while(SpawnerActiv)
        {
           // Debug.Log("SPAWNING BOX");
            Instantiate(BoxPrefabs[Random.Range(0 ,BoxPrefabs.Length)], SpawnPoints[Random.Range(0, SpawnPoints.Length)].position,Quaternion.identity);


            yield return new WaitForSeconds(TimeToNextBoxSpawn);
        }
    }



   public IEnumerator BurstSpawnSpawner()
    {
        while (SpawnerActiv)
        {
           // Debug.Log("BRUSTING BOX");
            var spawnPos = SpawnPoints[Random.Range(0, SpawnPoints.Length)].position;
            var BoxType = BoxPrefabs[Random.Range(0, BoxPrefabs.Length)];

            for (int i = 0; i < BurstAmount; i++)
            {
                Vector3 dank = new Vector3(spawnPos.x + Random.Range(0f, 1.0f), spawnPos.y + Random.Range(0f, 1.0f), 0);
                var boxRB =  Instantiate(BoxType, dank, Quaternion.identity);

               // boxRB.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-50000, 50000f), Random.Range(-50000, 50000f)));
                boxRB.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-50, 50f), Random.Range(-50, 50f));

            }
            yield return new WaitForSeconds(TimeToNextBurstBoxSpawn);
        }
    }
  
  
}
