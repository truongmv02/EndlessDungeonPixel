using MVT.Base.Dungeon.MapDesign;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class SpawnEnemyRatio
{
    public string name;
    public int ratio;
}

[System.Serializable]
public class EnemySpawnByLevel
{
    public int enemyCount;
    public int turnCount;
    public SpawnEnemyRatio[] enemies;
}

[System.Serializable]
public class DungeonSpawnEnemyInfo
{
    public int level;
    public EnemySpawnByLevel[] enemySpawnByLevels;
}

namespace MVT.Base.Dungeon
{
    [CreateAssetMenu(fileName = "Room_", menuName = "Scriptable Objects/Dungeon/Room")]
    public class RoomTemplateSO : ScriptableObject
    {
        public string id;
        public GameObject prefab;
        public RoomType roomType;

        public Vector2Int lowerBounds;
        public Vector2Int upperBounds;

        public List<DoorWay> doorWayList;
        public DungeonSpawnEnemyInfo[] dungeonSpawnInfos;

#if UNITY_EDITOR

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(id))
                id = GUID.Generate().ToString();
        }
#endif
    }

}