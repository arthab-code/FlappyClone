using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDatabase", menuName = "Game Database", order = 1)]
public class GameDatabase : ScriptableObject
{ 
    public GameObject terrain;
    public GameObject scoreArea;
}
