using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MVT.Base.Dungeon.MapDesign
{
    [CreateAssetMenu(fileName = "RoomMap", menuName = "Scriptable Objects/Dungeon/Room Map", order = 0)]
    public class RoomMapSO : ScriptableObject
    {
        [HideInInspector] public List<RoomNodeSO> roomList = new List<RoomNodeSO>();

        public RoomNodeSO GetRoom(string id)
        {
            return roomList.Find(x => x.id == id);
        }

        public RoomNodeSO GetRoom(RoomType type)
        {
            return roomList.Find(x => x.roomType == type);
        }


#if UNITY_EDITOR

        public bool AddRoom(RoomNodeSO room)
        {
            if (IsValidRoom(room))
            {
                roomList.Add(room);
                AssetDatabase.SaveAssets();
                return true;
            }
            return false;
        }

        public void RemoveRoom(RoomNodeSO room)
        {
            if (room.roomType == RoomType.StartRoom) return;

            if (room.parentRoom)
            {
                var parent = room.parentRoom.parentRoom;
                RemoveConnection(parent, room);

            }

            for (int i = room.childRoomList.Count - 1; i >= 0; i--)
            {
                var child = room.childRoomList[i];
                RemoveConnection(room, child.childRoomList[0]);
            }
            roomList.Remove(room);

            DestroyImmediate(room, true);
        }

        public void RemoveConnection(RoomNodeSO parent, RoomNodeSO child)
        {
            var corridor = child.parentRoom;

            child.parentRoom = null;

            parent.childRoomList.Remove(corridor);
            roomList.Remove(corridor);

            DestroyImmediate(corridor, true);
        }

        public bool ConnectRoom(RoomNodeSO parent, RoomNodeSO child, RoomNodeSO corridor)
        {
            parent.AddChildRoom(corridor);
            corridor.AddRoomParentRoom(parent);
            corridor.AddChildRoom(child);
            child.AddRoomParentRoom(corridor);
            return true;
        }

        public bool CanConnectRoom(RoomNodeSO parent, RoomNodeSO child)
        {
            if (parent.id == child.id) return false;

            if (child.roomType == RoomType.StartRoom) return false;

            if (parent.childRoomList.Count >= RoomNodeSO.MaxChildRoom) return false;

            if (child.parentRoom) return false;

            foreach (var corridor in parent.childRoomList)
            {
                if (corridor.childRoomList.Contains(child)) return false;
            }

            return true;
        }

        private bool IsValidRoom(RoomNodeSO room)
        {
            return true;
        }
#endif

    }
}