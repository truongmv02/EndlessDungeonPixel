using System;
using UnityEngine;

[Serializable]
public class AddObjectInfo
{
    public string prefab;
    public string objectInfo;
    public int turnCount = 1;
    public float spawnTurnDelay;
    public float angleVariation;
    public float spawnAngleDelay;
    public int amount = 1;
}

public class AddObjectHandle : BaseComponent<AddObjectInfo>, IHandle
{
    GameObject prefab;
    public Vector3 Position { set; get; }
    public Vector3 Direction { set; get; }
    public event Action<GameObject> OnCreateObjectFinish;

    private void Awake()
    {

    }
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
        GameObject obj = ObjectPool.Instance.GetObject(prefab);
        ISetInfo setInfo = obj.GetComponent<ISetInfo>();

        if (setInfo != null)
            SetObjectInfo(setInfo);

        obj.transform.up = direction;
        obj.transform.position = position;
        OnCreateObjectFinish?.Invoke(obj);
    }

    protected virtual void SetObjectInfo(ISetInfo setInfo)
    {

    }

    public override void SetInfo(object info)
    {
        base.SetInfo(info);
        prefab = Resources.Load<GameObject>(Info.prefab);
    }
}
