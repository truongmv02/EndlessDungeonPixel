using MVT.Base.Dungeon.MapDesign;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVT.Base.Dungeon
{
    [CreateAssetMenu(fileName = "Level_", menuName = "Scriptable Objects/Dungeon/Level")]
    public class DungeonLevelSO : ScriptableObject
    {
        public List<RoomTemplateSO> roomTemplateList;
        public List<RoomMapSO> roomMapList;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (roomTemplateList == null || roomMapList == null) return;

            foreach (var roomMap in roomMapList)
            {
                if (roomMap == null) continue;
                foreach (var room in roomMap.roomList)
                {
                    if (room.roomType == RoomType.Corridor) continue;
                    bool isRoomTypeFound = false;
                    foreach (var roomTemplate in roomTemplateList)
                    {
                        if (roomTemplate.roomType == room.roomType)
                        {
                            isRoomTypeFound = true;
                            break;
                        }
                    }

                    if (!isRoomTypeFound)
                    {
                        Debug.Log(this.name + ": No room template " + room.roomType + " for Room Map " + roomMap.name);
                    }
                }
            }
        }
#endif
    }
}