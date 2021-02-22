using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SceneController : MonoBehaviour
{
    public GameObject player;
    public GameDatabase gameDatabase;

    private GameObject terrain;
    private List<GameObject> terrainAmount;

    public float widthTerrain = 25;
    public float heightTerrain;

    void Start()
    {
        terrainAmount = new List<GameObject>();
        terrain = gameDatabase.terrain;
        InstantiateTerrain(terrain); 
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
}
