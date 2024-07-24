using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType
{
    Pitfall
}

public class ObstacleSpawner : MonoBehaviour
{
    Dictionary<ObstacleType, List<GameObject>> _activeObstacles = new Dictionary<ObstacleType, List<GameObject>>();
    Dictionary<ObstacleType, List<GameObject>> _inactiveObstacles = new Dictionary<ObstacleType, List<GameObject>>();

    public void SpawnObstacles(ObstacleScriptableObject obstacleConfig)
    {
        ObstacleType type = obstacleConfig.type;
        foreach (Vector3 point in obstacleConfig.spawnPoints)
        {
            if (_inactiveObstacles.ContainsKey(type) && _inactiveObstacles[type].Count > 0)
            {

            }
            else
            {
                Debug.Log("New Obstacle");
                GameObject newObstacle = Instantiate(obstacleConfig.prefab, point, Quaternion.identity);
                newObstacle.transform.parent = gameObject.transform;
                newObstacle.SetActive(true);
                if (_activeObstacles.ContainsKey(type))
                {
                    _activeObstacles[type].Add(newObstacle);
                }
                else
                {
                    _activeObstacles.Add(type, new List<GameObject>() { newObstacle });
                }                
            }            
        }
    }

    public void ResetObstacles()
    {
        List<GameObject> obstacles;
        foreach (ObstacleType type in _activeObstacles.Keys)
        {
            obstacles = _activeObstacles[type];
            foreach (GameObject obstacle in obstacles)
            {
                obstacle.SetActive(false);
            }
            _activeObstacles.Remove(type);
            if (_inactiveObstacles.ContainsKey(type))
            {
                _inactiveObstacles[type].AddRange(obstacles);
            }
            else
            {
                _inactiveObstacles.Add(type, obstacles);
            }
        }
    }
}
