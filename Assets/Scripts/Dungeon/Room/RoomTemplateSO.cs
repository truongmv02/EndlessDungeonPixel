using MVT.Base.Dungeon.MapDesign;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


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