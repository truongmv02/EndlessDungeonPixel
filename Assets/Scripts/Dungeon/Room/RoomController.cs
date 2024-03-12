using MVT.Base.Dungeon.MapDesign;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MVT.Base.Dungeon
{
    public class RoomController : MonoBehaviour
    {
        public RoomInfo roomInfo;
        [HideInInspector] public Tilemap ground1Tilemap;
        [HideInInspector] public Tilemap ground2Tilemap;
        [HideInInspector] public Tilemap decoration1Tilemap;
        [HideInInspector] public Tilemap decoration2Tilemap;
        [HideInInspector] public Tilemap frontTilemap;
        [HideInInspector] public Tilemap collisionTilemap;
        [HideInInspector] public Tilemap minimapTilemap;
        [HideInInspector] public Tilemap walkableTilemap;

        public void Init()
        {
            LoadTilemap();
            BlockUnusedDoorWays();
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

    }
}