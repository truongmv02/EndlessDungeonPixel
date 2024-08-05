using System;
using System.Collections;
using UnityEngine;

namespace MVT.Base.Dungeon
{
    public enum Orientation
    {
        North,
        East,
        South,
        West,
        None
    }

    [Serializable]
    public class DoorWay
    {
        public Vector2Int position;
        public Orientation orientation;
        public GameObject doorPrefab;
        public Vector2Int doorWayStartCoppyPosition;

        public int doorWayCoppyWidth;
        public int doorWayCoppyHeight;

        [HideInInspector] public bool isConected;
        [HideInInspector] public bool isUnavaiable;

        public DoorWay Clone()
        {
            var doorWay = new DoorWay();

            doorWay.position = position;
            doorWay.orientation = orientation;
            doorWay.doorPrefab = doorPrefab;
            doorWay.doorWayStartCoppyPosition = doorWayStartCoppyPosition;
            doorWay.doorWayCoppyWidth = doorWayCoppyWidth;
            doorWay.doorWayCoppyHeight = doorWayCoppyHeight;
            doorWay.isConected = isConected;
            doorWay.isUnavaiable = isUnavaiable;
            return doorWay;
        }
    }
}