using MVT.Base.Dungeon.MapDesign;
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
        [SerializeField] private SpawnPointInfo[] spawnInfos;
        List<DoorController> doors;

        public bool IsVisited { set; get; }

        #region TILE_MAP
        Tilemap ground1Tilemap;
        Tilemap ground2Tilemap;
        Tilemap decoration1Tilemap;
        Tilemap decoration2Tilemap;
        Tilemap frontTilemap;
        Tilemap collisionTilemap;
        Tilemap minimapTilemap;
        Tilemap walkableTilemap;
        #endregion

        public void Init()
        {
            LoadTilemap();
            BlockUnusedDoorWays();
            AddDoorsToRoom();
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
            if (roomInfo.roomType == RoomType.StartRoom || roomInfo.roomType == RoomType.ChestRoom) return;
            CloseDoors();
            IsVisited = true;
            Observer.Instance.Notify("OnRoomChange", this);
        }

        #region SET UP TILEMAP METHOD
        public void AddDoorsToRoom()
        {
            if (roomInfo.roomType == RoomType.CorridorNS || roomInfo.roomType == RoomType.CorridorEW) return;
            doors = new List<DoorController>();
            foreach (var doorWay in roomInfo.doorWayList)
            {
                if (doorWay.doorPrefab != null && doorWay.isConected)
                {
                    DoorController door = null;
                    door = Instantiate(doorWay.doorPrefab, gameObject.transform).GetComponent<DoorController>();
                    if (doorWay.orientation == Orientation.North)
                    {
                        door.transform.localPosition = new Vector2(doorWay.position.x + 0.5f, doorWay.position.y + 1);
                    }
                    else if (doorWay.orientation == Orientation.South)
                    {
                        door.transform.localPosition = new Vector2(doorWay.position.x + 0.5f, doorWay.position.y - 1);
                    }
                    else if (doorWay.orientation == Orientation.East)
                    {
                        door.transform.localPosition = new Vector2(doorWay.position.x + 1, doorWay.position.y);
                    }
                    else if (doorWay.orientation == Orientation.West)
                    {
                        door.transform.localPosition = new Vector2(doorWay.position.x - 0.5f, doorWay.position.y);
                    }

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
            foreach (var doorWay in roomInfo.doorWayList)
            {
                if (doorWay.isConected) continue;
                BlockDoorWayOnTilemap(doorWay, ground1Tilemap);
                BlockDoorWayOnTilemap(doorWay, ground2Tilemap);
                BlockDoorWayOnTilemap(doorWay, decoration1Tilemap);
                BlockDoorWayOnTilemap(doorWay, decoration2Tilemap);
                BlockDoorWayOnTilemap(doorWay, frontTilemap);
                BlockDoorWayOnTilemap(doorWay, collisionTilemap);
                BlockDoorWayOnTilemap(doorWay, minimapTilemap);
                BlockDoorWayOnTilemap(doorWay, walkableTilemap);
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
                else if (tilemap.CompareTag("WalkableTilemap"))
                {
                    walkableTilemap = tilemap;
                    walkableTilemap.gameObject.GetComponent<TilemapRenderer>().enabled = false;
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