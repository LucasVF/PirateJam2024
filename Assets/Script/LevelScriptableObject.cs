using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelScriptableObject", order = 1)]
public class LevelScriptableObject : ScriptableObject
{
    public int levelID;
    public List<Vector3> trueCollectibleSpawnPoint;
    public List<Vector3> fakeCollectibleSpawnPoint;
    public List<ObstacleScriptableObject> obstacles;
    public Vector3 playerStartPoint;
    public Vector3 dragonStartPoint;
    public float dragonAnimationSpeedFactor;
    public FireBallType dragonFireBallType;
}
