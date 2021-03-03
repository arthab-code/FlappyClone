using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SceneController : MonoBehaviour, IRestartableObject
{
    public GameObject player;
    public GameDatabase gameDatabase;
    public AudioSource audioSource;

    private GameObject terrain;
    private List<GameObject> terrainAmount;

    public float widthTerrain = 27;
    public float heightTerrain;

    private Vector3 originalPosition;

    void Start()
    {
        terrain = gameDatabase.terrain;
        originalPosition = terrain.transform.position;
        terrainAmount = new List<GameObject>();   
        InstantiateTerrain(terrain);

        PlayMusic();
    }

    void Update()
    {
        if (GreaterPlayerPosition())
            InstantiateTerrain(terrain);

        if (FirstTerrainOffCamera())
            RemoveFirstTerrain();
    }

    private void InstantiateTerrain(GameObject go)
    {
        GameObject newInstance;
        newInstance = Instantiate(go);

        if (terrainAmount.Count > 0)
        {
            float newXposition = terrainAmount[terrainAmount.Count - 1].transform.position.x + widthTerrain;

            Vector3 newTerrainPosition = new Vector3(newXposition, 0, 0);

            newInstance.transform.position = newTerrainPosition;
        }

        AddTerrainAmount(newInstance);
    }

    public void AddTerrainAmount(GameObject go)
    {
        terrainAmount.Add(go);
    }

    public void RemoveFirstTerrain()
    {
        Destroy(terrainAmount[0].gameObject);
        terrainAmount.RemoveAt(0);
    }

    private bool GreaterPlayerPosition()
    {
        return player.transform.position.x > terrainAmount[terrainAmount.Count - 1].transform.position.x ? true : false;

    }

    private bool FirstTerrainOffCamera()
    {
        return terrainAmount[0].transform.position.x < transform.position.x - widthTerrain ? true : false;
    }

    private void SetOriginalPosition()
    {
        foreach(GameObject terrainEach in terrainAmount)
        {
            Destroy(terrainEach.gameObject);
        }

        terrainAmount.Clear();
        terrain.transform.position = originalPosition;
        InstantiateTerrain(terrain);
    }

    private void PlayMusic()
    {
        audioSource.Play();
    }

    public void DoRestart()
    {
        SetOriginalPosition();
    }
}
