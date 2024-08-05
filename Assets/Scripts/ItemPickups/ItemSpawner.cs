using System.Collections;
using UnityEngine;

public class ItemSpawner : SingletonMonoBehaviour<ItemSpawner>
{
    [SerializeField] ItemBuffPickupController energyPrefab;
    private void Start()
    {
    }

    private void OnEnable()
    {
        Observer.Instance.AddObserver(ConstanVariable.ENEMY_DIE_KEY, OnEnemyDie);
    }

    private void OnDisable()
    {
        Observer.Instance.RemoveObserver(ConstanVariable.ENEMY_DIE_KEY, OnEnemyDie);
    }

    void OnEnemyDie(object enemyObj)
    {

        var randomValue = Random.Range(0, 100);
        if (randomValue > 40) return;

        EnemyController enemy = enemyObj as EnemyController;
        var infos = RandomItemEnemyDie();

        if (infos.Length == 1)
        {
            SpawnItem(infos[0], enemy.transform.position);
        }
        else
        {
            foreach (var info in infos)
            {
                float posx = Random.Range(-1f, 1f);
                float posy = Random.Range(-1f, 1f);
                Vector2 position = (Vector2)enemy.transform.position + new Vector2(posx, posy);
                SpawnItem(info, position);
            }
        }
    }

    void SpawnItem(BaseInfo info, Vector2 position)
    {
        var item = Instantiate(info.itemPrefab).GetComponent<ItemPickupController>();
        item.transform.position = position;
        item.SetInfo(info);
    }

    BaseInfo[] RandomItemEnemyDie()
    {
        var value = Random.Range(0, 100);
        if (value < 80)
        {
            int energyCount = Random.Range(3, 7);
            ItemBuffInfo[] infos = new ItemBuffInfo[energyCount];

            for (int i = 0; i < energyCount; i++)
            {
                infos[i] = energyPrefab.Info;

                StatInfo statInfo = new() { statName = "CurrentEnergy", value = Random.Range(1, 3) };
                infos[i].statInfos = new()
                {
                    stats = new StatInfo[1]
                };
                infos[i].statInfos.stats[0] = statInfo;
            }
            return infos;
        }
        else
        {
            return new[] { RandomWeapon() };
        }

    }
    WeaponInfo RandomWeapon()
    {
        var weaponInfos = DataManager.Instance.WeaponData.GetAllInfo();
        var index = Random.Range(0, weaponInfos.Count);
        return weaponInfos[index];
    }
}
