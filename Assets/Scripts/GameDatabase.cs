using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDatabase", menuName = "Game Database", order = 1)]
public class GameDatabase : ScriptableObject
{ 
    [Header("Terrain Prefab")]
    public GameObject terrain;
    [Header("ScoreArea with Columns Prefab")]
    public GameObject scoreArea;

}
