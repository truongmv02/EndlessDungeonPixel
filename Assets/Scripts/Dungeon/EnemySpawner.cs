using MVT.Base.Dungeon;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemySpawner : SingletonMonoBehaviour<EnemySpawner>
{

    private void Start()
    {
        Observer.Instance.AddObserver(ConstanVariable.ENEMY_DIE_KEY, HandleEnemyDie);
    }
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

    private void HandleEnemyDie(object enemyObj)
    {
        var enemy = enemyObj as EnemyController;
        RoomController room = enemy.Owner;
        room.Enemies.Remove(enemy);
        var enemyInStage = room.Enemies.Count;
        if (enemyInStage == 0 && room.Stage == room.roomInfo.enemySpawnByLevel.turnCount)
        {
            Observer.Instance.Notify(ConstanVariable.ROOM_CLEAR_KEY, room);
            room.OpenDoors();
            return;
        }
        if (enemyInStage == 0)
        {
            room.SpawnEnemy();
        }

    }


    public IEnumerator SpawnEnemyCoroutine(EnemySpawnByLevel enemySpawnByLevel, SpawnPointInfo[] spawnPointInfos, int count, RoomController room)
    {
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

                    EnemyInfo enemyInfo = DataManager.Instance.EnemyData.GetInfo(enemyRatio.name);
                    GameObject enemyPrefab = Resources.Load<GameObject>(enemyInfo.prefab);

                    EnemyController enemy = ObjectPool.Instance.GetObject(
                        enemyPrefab,
                        room.transform.position + positionRandom,
                        Vector2.up, enemyInfo
                        ).GetComponent<EnemyController>();
                    enemy.OriginalPosition = enemy.transform.position;
                    enemy.Owner = room;
                    room.Enemies.Add(enemy);
                    break;
                }
                else
                {
                    randomValue -= enemyRatio.ratio;
                }

            }

            if (room.roomInfo.roomType == MVT.Base.Dungeon.MapDesign.RoomType.BossRoom)
            {
                GamePlayUIManager.Instance.hpBossUI.Show(room.Enemies[0].Stats);
            }
            yield return null;
        }
    }


    private void OnDestroy()
    {
        Observer.Instance.RemoveObserver(ConstanVariable.ENEMY_DIE_KEY, HandleEnemyDie);
    }




}
