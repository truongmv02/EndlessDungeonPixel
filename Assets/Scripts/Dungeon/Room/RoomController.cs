using MVT.Base.Dungeon.MapDesign;
using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MVT.Base.Dungeon
{
    [System.Serializable]
    public class SpawnPointInfo
    {
        public Vector2 position;
        public float radius;
    }
    public class RoomController : MonoBehaviour
    {
        public RoomInfo roomInfo;
        public SpawnPointInfo[] spawnInfos;
        List<DoorController> doors;

        public bool IsVisited { set; get; }
        public int Stage { set; get; }

        public List<EnemyController> Enemies { set; get; } = new();

        #region TILE_MAP
        Tilemap ground1Tilemap;
        Tilemap ground2Tilemap;
        Tilemap decoration1Tilemap;
        Tilemap decoration2Tilemap;
        Tilemap frontTilemap;
        Tilemap collisionTilemap;
        Tilemap modifierTilemap;
        Tilemap minimapTilemap;
        #endregion

        NavMeshSurface navMesh;

        private void Awake()
        {
            navMesh = transform.Find("Navmesh").GetComponent<NavMeshSurface>();
            Init();
        }

        private void Start()
        {
            navMesh.BuildNavMeshAsync();
        }

        public void Init()
        {
            LoadTilemap();
            BlockUnusedDoorWays();
            AddDoorsToRoom();
        }



        public void SpawnEnemy()
        {
            Stage++;
            var enemySpawnByLevel = roomInfo.enemySpawnByLevel;
            var enemyInStage = enemySpawnByLevel.enemyCount / enemySpawnByLevel.turnCount;

            if (Stage == roomInfo.enemySpawnByLevel.turnCount)
            {
                enemyInStage = roomInfo.enemySpawnByLevel.enemyCount - enemyInStage * (Stage - 1);
            }
            StartCoroutine(EnemySpawner.Instance.SpawnEnemyCoroutine(roomInfo.enemySpawnByLevel, spawnInfos, enemyInStage, this));
        }


        public void CloseDoors()
        {

            foreach (DoorController door in doors)
            {
                door.CloseDoor();
            }
        }

        public void OpenDoors()
        {
            foreach (DoorController door in doors)
            {
                door.OpenDoor();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsVisited || !collision.CompareTag("Player")) return;
            GameManager.Instance.SetCurrentRoom(this);
            if (roomInfo == null || roomInfo.roomType == RoomType.StartRoom || roomInfo.roomType == RoomType.ChestRoom) return;
            CloseDoors();
            SpawnEnemy();
            IsVisited = true;
            Observer.Instance.Notify(ConstanVariable.ROOM_CHANGE_KEY, this);
        }



        #region SET UP TILEMAP METHOD
        public void AddDoorsToRoom()
        {
            if (roomInfo == null) return;
            if (roomInfo.roomType == RoomType.CorridorNS || roomInfo.roomType == RoomType.CorridorEW) return;
            doors = new List<DoorController>();
            foreach (var doorWay in roomInfo.doorWayList)
            {
                if (doorWay.doorPrefab != null && doorWay.isConected)
                {
                    DoorController door;
                    door = Instantiate(doorWay.doorPrefab, gameObject.transform).GetComponent<DoorController>();
                    var doorPosition = new Vector2(doorWay.position.x, doorWay.position.y);
                    if (doorWay.orientation == Orientation.West)
                    {
                        doorPosition.x += 1;
                    }
                    door.transform.localPosition = doorPosition;
                    doors.Add(door);
                    if (roomInfo.roomType == RoomType.BossRoom)
                    {
                        door.IsOpen = false;
                    }
                }
            }
        }

        public void BlockUnusedDoorWays()
        {
            if (roomInfo == null) return;
            foreach (var doorWay in roomInfo.doorWayList)
            {
                if (doorWay.isConected) continue;
                BlockDoorWayOnTilemap(doorWay, ground1Tilemap);
                BlockDoorWayOnTilemap(doorWay, ground2Tilemap);
                BlockDoorWayOnTilemap(doorWay, decoration1Tilemap);
                BlockDoorWayOnTilemap(doorWay, decoration2Tilemap);
                BlockDoorWayOnTilemap(doorWay, frontTilemap);
                BlockDoorWayOnTilemap(doorWay, collisionTilemap);
                BlockDoorWayOnTilemap(doorWay, modifierTilemap);
                BlockDoorWayOnTilemap(doorWay, minimapTilemap);
            }


        }

        private void BlockDoorWayOnTilemap(DoorWay doorWay, Tilemap tilemap)
        {
            if (tilemap == null || doorWay == null)
            {
                Debug.Log("null");
                return;
            }

            switch (doorWay.orientation)
            {
                case Orientation.North:
                case Orientation.South:
                BlockDoorWayHorizontal(doorWay, tilemap);
                break;

                case Orientation.East:
                case Orientation.West:
                BlockDoorWayVertical(doorWay, tilemap);
                break;

            }
        }

        private void BlockDoorWayHorizontal(DoorWay doorWay, Tilemap tilemap)
        {
            Vector2Int startPosition = doorWay.doorWayStartCoppyPosition;

            for (int y = 0; y < doorWay.doorWayCoppyHeight; y++)
            {
                for (int x = 0; x < doorWay.doorWayCoppyWidth; x++)
                {
                    Vector3Int currentTilePosition = new Vector3Int(startPosition.x + x, startPosition.y - y, 0);
                    Vector3Int nextTilePosition = currentTilePosition + new Vector3Int(1, 0, 0);

                    Matrix4x4 transformMaxtrix = tilemap.GetTransformMatrix(currentTilePosition);
                    var tile = tilemap.GetTile(currentTilePosition);
                    tilemap.SetTile(nextTilePosition, tile);
                    tilemap.SetTransformMatrix(nextTilePosition, transformMaxtrix);
                }
            }
        }

        private void BlockDoorWayVertical(DoorWay doorWay, Tilemap tilemap)
        {
            Vector2Int startPosition = doorWay.doorWayStartCoppyPosition;

            for (int x = 0; x < doorWay.doorWayCoppyWidth; x++)
            {
                for (int y = 0; y < doorWay.doorWayCoppyHeight; y++)
                {
                    Vector3Int currentTilePosition = new Vector3Int(startPosition.x + x, startPosition.y - y, 0);
                    Vector3Int nextTilePosition = currentTilePosition + new Vector3Int(0, -1, 0);

                    Matrix4x4 transformMaxtrix = tilemap.GetTransformMatrix(currentTilePosition);
                    var tile = tilemap.GetTile(currentTilePosition);
                    tilemap.SetTile(nextTilePosition, tile);
                    tilemap.SetTransformMatrix(nextTilePosition, transformMaxtrix);
                }
            }
        }

        private void LoadTilemap()
        {
            var tilemaps = GetComponentsInChildren<Tilemap>();

            foreach (Tilemap tilemap in tilemaps)
            {
                if (tilemap.CompareTag("Ground1Tilemap"))
                {
                    ground1Tilemap = tilemap;
                }
                else if (tilemap.CompareTag("Ground2Tilemap"))
                {
                    ground2Tilemap = tilemap;
                }
                else if (tilemap.CompareTag("Decoration1Tilemap"))
                {
                    decoration1Tilemap = tilemap;
                }
                else if (tilemap.CompareTag("Decoration2Tilemap"))
                {
                    decoration2Tilemap = tilemap;
                }
                else if (tilemap.CompareTag("FrontTilemap"))
                {
                    frontTilemap = tilemap;
                }
                else if (tilemap.CompareTag("CollisionTilemap"))
                {
                    collisionTilemap = tilemap;
                    collisionTilemap.gameObject.GetComponent<TilemapRenderer>().enabled = false;
                }
                else if (tilemap.CompareTag("MinimapTilemap"))
                {
                    minimapTilemap = tilemap;
                }
                else if (tilemap.CompareTag("ModifierTilemap"))
                {
                    modifierTilemap = tilemap;
                    modifierTilemap.gameObject.GetComponent<TilemapRenderer>().enabled = false;
                }
                else
                {
                    Debug.Log(tilemap.tag);
                }
            }

        }

        #endregion

        private void OnDrawGizmosSelected()
        {
            if (spawnInfos == null || spawnInfos.Length == 0) return;

            foreach (var spawnInfo in spawnInfos)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(new Vector3(transform.position.x + spawnInfo.position.x, transform.position.y + spawnInfo.position.y), spawnInfo.radius);
                Gizmos.color = Color.white;
            }

        }
    }

}