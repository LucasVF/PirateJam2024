using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ObstacleScriptableObject", order = 1)]
public class ObstacleScriptableObject : ScriptableObject
{
    public ObstacleType type;
    public GameObject prefab;
    public List<Vector3> spawnPoints;
}
