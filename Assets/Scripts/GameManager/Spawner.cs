using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{

    public static Spawner instance;
    private void Awake()
    {
        if (instance != null) return;
        instance = this;
    }

    public GameObject[] monsters;
    public float        SpawnDelay;
    public float        mapMaxMonsters;
    public float        mapLeft;
    public float        mapRight;
    public float        mapBottom;
    public float        mapTop;

    private                  float Z_Coord        = 0f;
    [HideInInspector] public float monsterCounter = 0f;

    private void Start()
    {
        InvokeRepeating("CalculateRandomData", 0f, SpawnDelay);
    }

    void CalculateRandomData()
    {
        if (monsterCounter >= mapMaxMonsters)
            return;
        
        float X = Random.Range(mapLeft, mapRight);
        float Y = Random.Range(mapBottom, mapTop);

        Vector3 p = new Vector3(X, Y, Z_Coord);
        Quaternion rotation = Quaternion.Euler(0f,0f,0f);

        GameObject monsterPrefab = monsters[new System.Random().Next(0, monsters.Length - 1)];
        
        GameObject mob = Instantiate(monsterPrefab,p, rotation);
        
        mob.SetActive(true);

        monsterCounter += 1f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        
        Gizmos.DrawLine(new Vector2(mapLeft, mapTop), new Vector2(mapRight, mapTop));
        Gizmos.DrawLine(new Vector2(mapRight, mapTop), new Vector2(mapRight, mapBottom));
        Gizmos.DrawLine(new Vector2(mapRight, mapBottom), new Vector2(mapLeft, mapBottom));
        Gizmos.DrawLine(new Vector2(mapLeft, mapBottom), new Vector2(mapLeft, mapTop));
    }
}
