using MVT.Base.Dungeon;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : SingletonMonoBehaviour<EnemySpawner>
{
    public EnemySpawnByLevel GetEnemySpawnByLevel(int level, DungeonSpawnEnemyInfo[] dungeonSpawnEnemyInfos)
    {
        if (dungeonSpawnEnemyInfos == null) return null;

        foreach (var dungeonSpawnEnemy in dungeonSpawnEnemyInfos)
        {
            if (dungeonSpawnEnemy.level != level) continue;
            if (dungeonSpawnEnemy.enemySpawnByLevels == null || dungeonSpawnEnemy.enemySpawnByLevels.Length == 0) return null;
            int index = Random.Range(0, dungeonSpawnEnemy.enemySpawnByLevels.Length);
            return dungeonSpawnEnemy.enemySpawnByLevels[index];
        }
        return null;
    }

    public List<EnemyController> SpawnEnemy(EnemySpawnByLevel enemySpawnByLevel, SpawnPointInfo[] spawnPointInfos, int count, Vector3 roomPosition)
    {
        List<EnemyController> enemyList = new List<EnemyController>();
        int totalRatio = 0;
        foreach (var enemyRatio in enemySpawnByLevel.enemies)
        {
            totalRatio += enemyRatio.ratio;
        }

        for (int i = 0; i < count; i++)
        {
            int randomValue = Random.Range(0, totalRatio);
            foreach (var enemyRatio in enemySpawnByLevel.enemies)
            {
                if (randomValue <= enemyRatio.ratio)
                {
                    SpawnPointInfo spawnPointRandom = spawnPointInfos[Random.Range(0, spawnPointInfos.Length)];
                    float randomX = Random.Range(-spawnPointRandom.radius, spawnPointRandom.radius);
                    float randomY = Random.Range(-spawnPointRandom.radius, spawnPointRandom.radius);
                    Vector3 positionRandom = spawnPointRandom.position + new Vector2(randomX, randomY);
                    var enemyInfo = DataManager.Instance.EnemyData.GetInfo(enemyRatio.name);
                    var enemyPrefab = Resources.Load<GameObject>(enemyInfo.prefab);
                    var enemy = Instantiate(enemyPrefab, roomPosition + positionRandom, Quaternion.identity).GetComponent<EnemyController>();
                    enemy.SetInfo(enemyInfo);
                    enemyList.Add(enemy);
                    break;
                }
                else
                {
                    randomValue -= enemyRatio.ratio;
                }
            }
        }

        return enemyList;
    }


}
