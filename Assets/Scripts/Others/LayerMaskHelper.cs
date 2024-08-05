using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerMaskHelper
{
    public static readonly LayerMask ObstacleLayer = LayerMask.NameToLayer("Obstacle");
    public static readonly LayerMask PlayerLayer = LayerMask.NameToLayer("Player");
    public static readonly LayerMask EnemyLayer = LayerMask.NameToLayer("Enemy");
    public static readonly LayerMask RoomLayer = LayerMask.NameToLayer("Room");
    public static readonly LayerMask WallLayer = LayerMask.NameToLayer("Wall");

    public static readonly LayerMask PlayerMask = LayerMask.GetMask(new[] { "Player" });
    public static readonly LayerMask EnemyMask = LayerMask.GetMask(new[] { "Enemy" });
    public static readonly LayerMask EnemyTakeDamageMask = LayerMask.GetMask(new[] { "EnemyTakeDamage" });
    public static readonly LayerMask ObstacleMask = LayerMask.GetMask(new[] { "Obstacle" });
    public static readonly LayerMask RoomMask = LayerMask.GetMask(new[] { "Room" });
    public static readonly LayerMask WallMask = LayerMask.GetMask(new[] { "Wall" });

    public static LayerMask MergeLayerMask(LayerMask layer1, LayerMask layer2)
    {
        LayerMask layerMask = layer1;
        layerMask |= layer2;
        return layerMask;
    }
}
