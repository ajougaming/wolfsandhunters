using UnityEngine;
using System.Collections;

public class MonsterManager : MonoBehaviour {
    public float playerHP;
    public GameObject player;
    public GameObject monster;
    public float spawnTime = 5.0f;
    public Transform[] spawnPoints;

	// Use this for initialization
	void Start () {
        
        InvokeRepeating("SpawnMonster", spawnTime, spawnTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void SpawnMonster()
    {
        playerHP = player.GetComponent<PlayerCtrl>().playerHP;
        if (playerHP <= 0.0f)
        {
            return;
        }
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(monster, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
