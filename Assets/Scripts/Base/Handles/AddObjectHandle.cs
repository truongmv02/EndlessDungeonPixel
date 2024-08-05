using System;
using UnityEngine;

[Serializable]
public class AddObjectInfo
{
    public string objectInfo;
    public int turnCount = 1;
    public float spawnTurnDelay;
    public float angleVariation;
    public float spawnAngleDelay;
    public int amount = 1;
}

public class AddObjectHandle : BaseComponent<AddObjectInfo>, IHandle
{
    public GameObject prefab;
    public Vector3 Position { set; get; }
    public Vector3 Direction { set; get; }

    public event Action<GameObject> OnCreateObjectFinish;

    ProjectileInfo projectileInfo;
    public GameObject Object { protected set; get; }
    public void Handle()
    {
        for (int i = 0; i < Info.turnCount; i++)
        {
            Vector3 currentDirection;
            if (Info.amount == 1)
            {
                currentDirection = Direction;
            }
            else
            {
                var rotation = Quaternion.Euler(0f, 0f, -((Info.amount - 1) * Info.angleVariation / 2f));
                currentDirection = rotation * Direction;
            }
            var rotationQuaternion = Quaternion.Euler(0f, 0f, Info.angleVariation);

            for (int j = 0; j < Info.amount; j++)
            {
                float delay = Info.spawnAngleDelay * j + i * Info.spawnTurnDelay;
                LeanTween.delayedCall(delay, () =>
                {
                    SpawnObject(Position, currentDirection);
                    currentDirection = rotationQuaternion * currentDirection;
                });
            }
        }
    }

    private void SpawnObject(Vector3 position, Vector3 direction)
    {
        Object = ObjectPool.Instance.GetObject(prefab, position, direction, projectileInfo);
        OnCreateObjectFinish?.Invoke(Object);
    }

    public override void SetInfo(object info)
    {
        base.SetInfo(info);
        projectileInfo = DataManager.Instance.ProjectileData.GetInfo(Info.objectInfo);
        prefab = Resources.Load<GameObject>(projectileInfo.prefab);
        prefab.name = prefab.name;
    }

}
