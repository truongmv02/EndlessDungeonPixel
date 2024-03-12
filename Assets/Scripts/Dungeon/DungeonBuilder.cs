using MVT.Base.Dungeon.MapDesign;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace MVT.Base.Dungeon
{
    public class DungeonBuilder : MonoBehaviour
    {
        [SerializeField] public DungeonLevelSO level;

        private List<RoomController> roomList = new List<RoomController>();
        private List<RoomInfo> roomInfoList = new List<RoomInfo>();


        private void Start()
        {
            GenerateDungeon();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ClearAllRoom();
                GenerateDungeon();
            }
        }

        public void GenerateDungeon()
        {
            var roomMap = GetRandomRoomMap(level.roomMapList);
            if (BuildDungeon(roomMap))
            {
            }
        }

        public bool BuildDungeon(RoomMapSO roomMap)
        {

            bool result = false;
            for (int i = 0; i < 50; i++)
            {
                RoomNodeSO startRoomNode = roomMap.GetRoom(RoomType.StartRoom);
                if (startRoomNode == null)
                {
                    Debug.Log("Start Room not found");
                    return false;
                }
                RoomInfo startRoom = CreateStartRoom(startRoomNode);
                result = CreateRoom(startRoomNode.childRoomList, startRoom);
                if (result) break;
                ClearAllRoom();
            }

            if (!result)
            {
                Debug.Log("Can create dungeon");
            }
            else
            {
                CreateRoomObject();
            }

            return true;
        }

        public RoomInfo CreateStartRoom(RoomNodeSO roomNode)
        {
            RoomTemplateSO roomTemplate = GetRandomRoomTemplate(roomNode.roomType);
            var room = CreateRoomFromRoomTemplate(roomTemplate, roomNode);
            roomInfoList.Add(room);
            return room;
        }

        public bool CreateRoom(List<RoomNodeSO> childRoomNodeList, RoomInfo parent)
        {

            foreach (var roomNode in childRoomNodeList)
            {
                bool isConnect;
                RoomInfo room;
                do
                {
                    var doorWays = GetUnConnectedDoorWay(parent);
                    if (doorWays.Count <= 0) return false;

                    var parentDoorWay = doorWays[Random.Range(0, doorWays.Count)];
                    RoomTemplateSO roomTemplate = GetRandomRoomTemplate(roomNode.roomType, parentDoorWay);

                    room = CreateRoomFromRoomTemplate(roomTemplate, roomNode);
                    isConnect = ConnectRoom(parent, room, parentDoorWay);

                } while (!isConnect);

                //    room.transform.position = new Vector3(room.lowerBounds.x - room.lowerBoundsTemplate.x, room.upperBounds.y - room.upperBoundsTemplate.y, 0);
                roomInfoList.Add(room);

                if (!CreateRoom(roomNode.childRoomList, room))
                {
                    return false;
                }
            }

            return true;
        }

        public RoomInfo CreateRoomFromRoomTemplate(RoomTemplateSO roomTemplate, RoomNodeSO roomSO)
        {
            RoomInfo room = new RoomInfo();
            room.id = roomSO.id;
            room.lowerBounds = roomTemplate.lowerBounds;
            room.upperBounds = roomTemplate.upperBounds;
            room.lowerBoundsTemplate = roomTemplate.lowerBounds;
            room.upperBoundsTemplate = roomTemplate.upperBounds;
            room.roomType = roomTemplate.roomType;
            room.prefab = roomTemplate.prefab;
            foreach (var doorWay in roomTemplate.doorWayList)
            {
                room.doorWayList.Add(doorWay.Clone());
            }
            return room;
        }

        public bool ConnectRoom(RoomInfo parent, RoomInfo child, DoorWay parentDoorWay)
        {

            DoorWay doorWay = FindDoorWayConnect(child.doorWayList, parentDoorWay);
            if (doorWay == null)
            {
                return false;
            }

            Vector2Int adjustment = Vector2Int.zero;

            switch (doorWay.orientation)
            {
                case Orientation.North:
                adjustment = new Vector2Int(0, -1);
                break;
                case Orientation.South:
                adjustment = new Vector2Int(0, 1);
                break;
                case Orientation.East:
                adjustment = new Vector2Int(-1, 0);
                break;
                case Orientation.West:
                adjustment = new Vector2Int(1, 0);
                break;
            }


            Vector2Int parentDoorWayPosition = parent.lowerBounds + parentDoorWay.position - parent.lowerBoundsTemplate;

            child.lowerBounds = parentDoorWayPosition + adjustment + child.lowerBoundsTemplate - doorWay.position;
            child.upperBounds = child.lowerBounds + child.upperBoundsTemplate - child.lowerBoundsTemplate;

            if (IsRoomOverLap(child))
            {
                parentDoorWay.isUnavaiable = true;
                return false;
            }
            else
            {
                doorWay.isConected = true;
                parentDoorWay.isConected = true;
                child.parent = parent;
                parent.childRoomList.Add(child);
                return true;
            }


        }
        public DoorWay FindDoorWayConnect(List<DoorWay> doorWayList, DoorWay parentDoorWay)
        {
            foreach (var doorWay in doorWayList)
            {
                if (parentDoorWay.orientation == Orientation.North && doorWay.orientation == Orientation.South) return doorWay;
                if (parentDoorWay.orientation == Orientation.South && doorWay.orientation == Orientation.North) return doorWay;
                if (parentDoorWay.orientation == Orientation.West && doorWay.orientation == Orientation.East) return doorWay;
                if (parentDoorWay.orientation == Orientation.East && doorWay.orientation == Orientation.West) return doorWay;

            }
            return null;

        }

        public List<DoorWay> GetUnConnectedDoorWay(RoomInfo room)
        {
            List<DoorWay> doorWays = new List<DoorWay>();

            foreach (var doorWay in room.doorWayList)
            {
                if (!doorWay.isConected && !doorWay.isUnavaiable)
                    doorWays.Add(doorWay);
            }
            return doorWays;
        }

        public bool IsRoomOverLap(RoomInfo roomCheck)
        {
            foreach (var room in roomInfoList)
            {

                bool isOverLapX = IsOverLap(roomCheck.lowerBounds.x, roomCheck.upperBounds.x, room.lowerBounds.x, room.upperBounds.x);
                bool isOverLapY = IsOverLap(roomCheck.lowerBounds.y, roomCheck.upperBounds.y, room.lowerBounds.y, room.upperBounds.y);
                if (isOverLapX && isOverLapY) return true;
            }

            return false;
        }

        public bool IsOverLap(int min1, int max1, int min2, int max2)
        {
            return Mathf.Max(min1, min2) <= Mathf.Min(max1, max2);
        }

        RoomMapSO GetRandomRoomMap(List<RoomMapSO> roomMapList)
        {
            if (roomMapList != null && roomMapList.Count > 0)
                return level.roomMapList[Random.Range(0, roomMapList.Count)];

            Debug.Log("RoomMap not found!");
            return null;
        }

        public RoomTemplateSO GetRandomRoomTemplate(RoomType type, DoorWay parentDoorWay)
        {
            if (type != RoomType.Corridor)
                return GetRandomRoomTemplate(type);

            if (parentDoorWay.orientation == Orientation.North || parentDoorWay.orientation == Orientation.South)
            {
                return GetRandomRoomTemplate(RoomType.CorridorNS);
            }
            else
            {
                return GetRandomRoomTemplate(RoomType.CorridorEW);
            }

        }

        RoomTemplateSO GetRandomRoomTemplate(RoomType type)
        {
            var list = new List<RoomTemplateSO>();

            foreach (var roomTemplate in level.roomTemplateList)
            {
                if (roomTemplate.roomType == type)
                {
                    list.Add(roomTemplate);
                }
            }

            if (list.Count == 0) return null;

            return list[Random.Range(0, list.Count)];
        }

        public void ClearAllRoom()
        {
            roomInfoList.Clear();
            foreach (var room in roomList)
            {
                Destroy(room.gameObject);
            }
            roomList.Clear();
        }

        public void CreateRoomObject()
        {
            foreach (var roomInfo in roomInfoList)
            {
                RoomController room = Instantiate(roomInfo.prefab, transform).GetComponent<RoomController>();
                room.roomInfo = roomInfo;
                room.Init();
                room.transform.position = new Vector3(
                    roomInfo.lowerBounds.x - roomInfo.lowerBoundsTemplate.x,
                    roomInfo.upperBounds.y - roomInfo.upperBoundsTemplate.y, 0
                    );
                roomList.Add(room);

            }
        }

    }
}