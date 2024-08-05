using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MVT.Base.Dungeon.MapDesign
{
    public enum RoomType
    {
        StartRoom,
        SmallRoom,
        MediumRoom,
        LargeRoom,
        BossRoom,
        ChestRoom,
        CorridorNS,
        CorridorEW,
        Corridor
    }
    public class RoomNodeSO : ScriptableObject
    {
        [HideInInspector] public string id;
        [HideInInspector] public RoomNodeSO parentRoom;
        [HideInInspector] public List<RoomNodeSO> childRoomList = new List<RoomNodeSO>();
        [HideInInspector] public RoomType roomType;

#if UNITY_EDITOR

        public const int MaxChildRoom = 3;
        [HideInInspector] public bool isSelected;
        [HideInInspector] public Rect rect;

        public bool DisplayInMapRoom => roomType != RoomType.Corridor && roomType != RoomType.CorridorNS && roomType != RoomType.CorridorEW;

        public void Initialise(Rect rect, RoomType roomType)
        {
            id = Guid.NewGuid().ToString();
            this.roomType = roomType;
            this.rect = rect;
            this.name = roomType.ToString();
        }


        public void ChangeState(bool selected)
        {
            isSelected = selected;
            GUI.changed = true;
            Selection.activeObject = this;
            AssetDatabase.SaveAssets();
        }

        public bool AddRoomParentRoom(RoomNodeSO room)
        {
            if (room == this || room == parentRoom) return false;
            parentRoom = room;
            EditorUtility.SetDirty(this);
            return true;
        }

        public bool AddChildRoom(RoomNodeSO room)
        {
            if (room == this || childRoomList.Contains(room) || childRoomList.Count > MaxChildRoom) return false;
            childRoomList.Add(room);
            EditorUtility.SetDirty(this);
            return true;
        }


        public void Draw(GUIStyle style)
        {

            GUILayout.BeginArea(rect, style);

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.LabelField(roomType.ToString());

            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(this);
            GUILayout.EndArea();
        }



        public void Drag(Vector2 delta, bool checkSelected)
        {
            if (!checkSelected)
            {
                Drag(delta);
                return;
            }
            if (isSelected)
            {
                Drag(delta);
            }
        }

        private void Drag(Vector2 delta)
        {
            rect.position += delta;
            GUI.changed = true;
        }
#endif
    }
}
