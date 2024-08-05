using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace MVT.Base.Dungeon.MapDesign
{
    public class RoomMapEditor : EditorWindow
    {

        private static RoomMapSO roomMap;
        private Vector2 mousePosition;
        private RoomNodeSO roomSelected;

        private RoomNodeSO roomToDrawLineFrom;
        private Vector2 lineDrag;

        private RoomType corridorType;

        #region ROOM VARIABLE

        private GUIStyle roomStyle;
        private GUIStyle roomSelectedStyle;

        private GUIStyle chestRoomStyle;
        private GUIStyle chestRoomSelectedStyle;

        private GUIStyle bossRoomStyle;
        private GUIStyle bossRoomSelectedStyle;



        private const int roomPadding = 20;
        private const int roomBorder = 12;

        private const float roomWidth = 130f;
        private const float roomHeight = 60f;

        public bool isPressCrtlKey;

        #endregion

        #region GRID VARIABLE
        private const float gridSmall = 20f;
        private const float gridLarge = 100f;

        #endregion

        private void OnEnable()
        {
            roomStyle = CreateRoomStyle("node1");
            roomSelectedStyle = CreateRoomStyle("node1 on");

            chestRoomStyle = CreateRoomStyle("node4");
            chestRoomSelectedStyle = CreateRoomStyle("node4 on");

            bossRoomStyle = CreateRoomStyle("node6");
            bossRoomSelectedStyle = CreateRoomStyle("node6 on");
        }

        public GUIStyle CreateRoomStyle(string style)
        {
            var roomStyle = new GUIStyle();
            roomStyle.normal.textColor = Color.white;
            roomStyle.normal.background = EditorGUIUtility.Load(style) as Texture2D;
            roomStyle.padding = new RectOffset(roomPadding, roomPadding, roomPadding, roomPadding);
            roomStyle.border = new RectOffset(roomBorder, roomBorder, roomBorder, roomBorder);
            return roomStyle;

        }

        private void OnGUI()
        {
            DrawGrid(gridSmall, Color.black);
            DrawGrid(gridLarge, Color.black, 0.5f);

            DrawDraggedLine();

            HandleEvents(Event.current);

            DrawRoomConnections();

            DrawRooms();

            /*if (GUI.changed)
            {
                Repaint();
            }*/
        }


        #region HANDLE EVENTS
        private void HandleEvents(Event currentEvent)
        {
            mousePosition = currentEvent.mousePosition;
            switch (currentEvent.type)
            {
                case EventType.MouseDown:
                HandleMouseDownEvents(currentEvent);
                break;

                case EventType.MouseUp:
                HandleMouseUpEvents(currentEvent);
                break;

                case EventType.MouseDrag:
                HandleMouseDragEvents(currentEvent);
                break;

                case EventType.KeyDown:
                if (currentEvent.keyCode == KeyCode.LeftControl || currentEvent.keyCode == KeyCode.RightControl)
                {
                    isPressCrtlKey = true;
                }
                break;

                case EventType.KeyUp:
                if (currentEvent.keyCode == KeyCode.LeftControl || currentEvent.keyCode == KeyCode.RightControl)
                {
                    isPressCrtlKey = false;
                }
                break;


            }
        }

        private void HandleMouseDownEvents(Event currentEvent)
        {
            roomSelected = IsMouseOverRoom(currentEvent);
            if (currentEvent.button == 0)
            {
                HandleLeftMouseDownEvents(currentEvent);
            }
            else if (currentEvent.button == 1)
            {
                HandleRightMouseDownEvents(currentEvent);
            }
        }

        public void HandleLeftMouseDownEvents(Event currentEvent)
        {
            if (roomToDrawLineFrom && roomSelected)
            {
                if (roomMap.CanConnectRoom(roomToDrawLineFrom, roomSelected))
                {
                    var corridor = CreateRoom(corridorType);
                    if (corridor)
                    {
                        roomMap.ConnectRoom(roomToDrawLineFrom, roomSelected, corridor);
                    }
                }
            }
            SelectRoom(roomSelected);
            ClearLineDrag();
        }

        public void HandleRightMouseDownEvents(Event currentEvent)
        {
            if (roomSelected)
            {
                ClearAllRoomSelected();
                roomSelected.ChangeState(true);
                EditorUtility.SetDirty(roomSelected);

                InitMenuRoom();
            }
            else
            {
                InitMenuRoomMap();
            }
        }

        private void HandleMouseDragEvents(Event currentEvent)
        {
            if (currentEvent.button == 0)
            {
                DragRooms(currentEvent.delta, true);
            }
            else if (currentEvent.button == 1)
            {

            }
            else if (currentEvent.button == 2)
            {
                DragRooms(currentEvent.delta, false);
            }
        }

        private void HandleMouseUpEvents(Event currentEvent)
        {

        }

        #endregion

        #region ROOM 

        private void CreateNewRoom(object roomTypeObj)
        {
            if (roomMap.roomList.Count == 0)
            {
                CreateRoom(RoomType.StartRoom, new Vector2(150, 150));

            }
            CreateRoom((RoomType)roomTypeObj, mousePosition);
        }

        private RoomNodeSO CreateRoom(RoomType roomType, Vector2 position = new Vector2())
        {
            RoomNodeSO newRoom = CreateInstance(typeof(RoomNodeSO)) as RoomNodeSO;
            newRoom.Initialise(new Rect(position, new Vector2(roomWidth, roomHeight)), roomType);
            var result = roomMap.AddRoom(newRoom);
            if (result)
            {
                AssetDatabase.AddObjectToAsset(newRoom, roomMap);
                AssetDatabase.SaveAssets();
                return newRoom;
            }
            return null;
        }

        private void DeleteConnection(object child)
        {
            roomMap.RemoveConnection(roomSelected, (RoomNodeSO)child);
        }

        private void DeleteRoom()
        {
            roomMap.RemoveRoom(roomSelected);
        }

        private void DeleteSelectedRooms()
        {
            for (int i = roomMap.roomList.Count - 1; i >= 0; i--)
            {
                var room = roomMap.roomList[i];
                if (room.isSelected)
                {
                    roomMap.RemoveRoom(room);
                }

            }
        }

        public void SelectRoom(RoomNodeSO roomSelected)
        {
            if (!roomSelected)
            {
                ClearAllRoomSelected();
            }
            if (roomSelected)
            {
                if (!isPressCrtlKey && !roomSelected.isSelected)
                {
                    ClearAllRoomSelected();
                }

                if (roomSelected.isSelected && isPressCrtlKey)
                {
                    roomSelected.ChangeState(false);
                }
                else
                {
                    roomSelected.ChangeState(true);
                }

            }
        }

        private void SelectAllRoom()
        {
            foreach (var room in roomMap.roomList)
            {
                if (room.DisplayInMapRoom)
                    room.ChangeState(true);
            }
        }

        private void ClearAllRoomSelected()
        {
            foreach (var room in roomMap.roomList)
            {
                if (room.isSelected)
                {
                    room.ChangeState(false);
                }
            }
        }

        private void DragRooms(Vector2 delta, bool checkSelected)
        {
            foreach (var room in roomMap.roomList)
            {
                room.Drag(delta, checkSelected);
            }
        }

        private RoomNodeSO IsMouseOverRoom(Event currentEvent)
        {
            foreach (var room in roomMap.roomList)
            {
                if (room.DisplayInMapRoom && room.rect.Contains(currentEvent.mousePosition))
                {
                    return room;
                }
            }
            return null;
        }
        #endregion

        #region DRAW
        private void DrawGrid(float gridSize, Color color, float opacity = 0.3f)
        {
            int verticalLineCount = Mathf.CeilToInt(position.width / gridSize);
            int horizontalLineCount = Mathf.CeilToInt(position.height / gridSize);

            Handles.color = new Color(color.r, color.g, color.b, opacity);

            for (int i = 0; i < verticalLineCount; i++)
            {
                Handles.DrawLine(new Vector3(gridSize * i, 0f, 0f), new Vector3(gridSize * i, position.height, 0f));
            }

            for (int i = 0; i < horizontalLineCount; i++)
            {
                Handles.DrawLine(new Vector3(0, gridSize * i, 0f), new Vector3(position.width, gridSize * i, 0f));
            }

            Handles.color = Color.white;

        }
        private void DrawRoomConnections()
        {
            foreach (var room in roomMap.roomList)
            {
                if (!room.DisplayInMapRoom || room.childRoomList == null) continue;
                foreach (var corridor in room.childRoomList)
                {
                    if (corridor.childRoomList == null) continue;
                    var childRoom = corridor.childRoomList[0];
                    DrawConnectionLine(room.rect.center, childRoom.rect.center);
                    GUI.changed = true;
                }
            }
        }
        private void DrawRooms()
        {
            foreach (var room in roomMap.roomList)
            {
                if (!room.DisplayInMapRoom) continue;

                if (room.isSelected)
                {
                    if (room.roomType == RoomType.ChestRoom)
                    {
                        room.Draw(chestRoomSelectedStyle);
                    }
                    else if (room.roomType == RoomType.BossRoom)
                    {
                        room.Draw(bossRoomSelectedStyle);
                    }
                    else
                    {
                        room.Draw(roomSelectedStyle);
                    }
                }
                else
                {
                    if (room.roomType == RoomType.ChestRoom)
                    {
                        room.Draw(chestRoomStyle);
                    }
                    else if (room.roomType == RoomType.BossRoom)
                    {
                        room.Draw(bossRoomStyle);
                    }
                    else
                    {
                        room.Draw(roomStyle);
                    }
                }
            }
            GUI.changed = true;
        }


        private void DrawDraggedLine()
        {
            if (roomToDrawLineFrom != null)
            {
                var startPos = roomToDrawLineFrom.rect.center;
                DrawConnectionLine(startPos, mousePosition);
            }
        }
        private void DrawConnectionLine(Vector2 startPosition, Vector2 endPosition)
        {
            float arrowSize = 8f;
            float arrowLineWidth = 5f;
            Vector2 midPosition = (endPosition + startPosition) / 2;

            Vector2 direction = endPosition - startPosition;

            Vector2 arrowTailPoint1 = midPosition - new Vector2(-direction.y, direction.x).normalized * arrowSize;
            Vector2 arrowTailPoint2 = midPosition + new Vector2(-direction.y, direction.x).normalized * arrowSize;

            Vector2 arrowHeadPoint = midPosition + direction.normalized * arrowSize;

            Handles.DrawBezier(arrowHeadPoint, arrowTailPoint1, arrowHeadPoint, arrowTailPoint1, Color.white, null, arrowLineWidth);
            Handles.DrawBezier(arrowHeadPoint, arrowTailPoint2, arrowHeadPoint, arrowTailPoint2, Color.white, null, arrowLineWidth);

            Handles.DrawBezier(arrowTailPoint1, arrowTailPoint2, arrowTailPoint1, arrowTailPoint2, Color.white, null, arrowLineWidth);

            Handles.DrawBezier(startPosition, endPosition, startPosition, endPosition, Color.white, null, 4);
            GUI.changed = true;
        }

        #endregion

        #region OTHER
        private void InitMenuRoomMap()
        {
            GenericMenu menu = new GenericMenu();

            var roomtypes = Enum.GetValues(typeof(RoomType));
            foreach (RoomType roomType in roomtypes)
            {
                if (roomType == RoomType.Corridor || roomType == RoomType.CorridorNS
                    || roomType == RoomType.CorridorEW || roomType == RoomType.StartRoom) continue;

                menu.AddItem(new GUIContent("Creat Room/" + roomType), false, CreateNewRoom, roomType);
                menu.AddSeparator("Creat Room/");
            }
            menu.AddSeparator("");

            menu.AddItem(new GUIContent("Select All Room"), false, SelectAllRoom);
            menu.AddSeparator("");

            menu.AddItem(new GUIContent("Delete Selected Rooms"), false, DeleteSelectedRooms);

            menu.ShowAsContext();
        }

        private void InitMenuRoom()
        {
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("Create Corridor "), false, CreateCorridor, RoomType.Corridor);

            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Delete Room"), false, DeleteRoom);


            for (int i = 0; i < roomSelected.childRoomList.Count; i++)
            {
                var child = roomSelected.childRoomList[i];
                var connectionString = roomSelected.roomType + " -> " + child.childRoomList[0].roomType;

                menu.AddItem(new GUIContent("Delete Link/" + (i + 1) + ". " + connectionString), false, DeleteConnection, child.childRoomList[0]);
            }

            menu.ShowAsContext();
        }

        private void CreateCorridor(object corridorType)
        {
            roomToDrawLineFrom = roomSelected;
            lineDrag = mousePosition;
            this.corridorType = (RoomType)corridorType;
        }

        private void ClearLineDrag()
        {
            roomToDrawLineFrom = null;
            lineDrag = Vector2.zero;
            GUI.changed = true;
        }

        #endregion

        #region EDITOR
        [OnOpenAsset(0)]
        public static bool OnDoubleClick(int instanceID, int line)
        {
            var roomMapSO = EditorUtility.InstanceIDToObject(instanceID) as RoomMapSO;

            if (roomMapSO != null)
            {
                roomMap = roomMapSO;
                ShowWindow();
                return true;
            }
            return false;
        }

        [MenuItem("Window/Dungeon/RoomMap")]
        public static void ShowWindow()
        {
            RoomMapEditor wnd = GetWindow<RoomMapEditor>();
            wnd.titleContent = new GUIContent("RoomMap");
        }
        #endregion

    }
}