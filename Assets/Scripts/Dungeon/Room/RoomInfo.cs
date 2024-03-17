using MVT.Base.Dungeon.MapDesign;
using MVT.Base.Dungeon;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfo
{
    public string id;

    public GameObject prefab;

    public Vector2Int lowerBounds;
    public Vector2Int upperBounds;

    public Vector2Int lowerBoundsTemplate;
    public Vector2Int upperBoundsTemplate;

    public RoomType roomType;

    public RoomInfo parent;
    public List<RoomInfo> childRoomList = new List<RoomInfo>();

    public List<DoorWay> doorWayList = new List<DoorWay>();
    public EnemySpawnByLevel enemySpawnByLevel;
}
