using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [HideInInspector] public EnemySpawnManager manager;
    [SerializeField] private Transform targetPosition;
    [HideInInspector] readonly private List<GameObject> enemyPool = new();
    [SerializeField] private List<GameObject> enemyPrefab = new();

    private void OnEnable()
    {
        manager.CreatePoolEnemy += CreatePoolEnemy;
        manager.DeletePullEnemy += DeletePoolEnemy;
        manager.CreatEnemy += CreateEnemyInScene;
    }
    private void OnDisable()
    {
        manager.CreatePoolEnemy -= CreatePoolEnemy;
        manager.DeletePullEnemy -= DeletePoolEnemy;
        manager.CreatEnemy -= CreateEnemyInScene;
    }

    public void DeadEnemy()
    {
        Coin.coin += enemyPool[0].GetComponent<EnemyHp>().coin;
    }

    private void CreateEnemyInScene()
    {
        foreach (var enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.transform.position = transform.position;
                enemy.GetComponent<EnemyHp>().hp = enemy.GetComponent<EnemyHp>().maxHp;
                enemy.SetActive(true);
                break;
            }
        }
    }

    private void CreatePoolEnemy()
    {
        GameObject activPrefab = null;
        if (enemyPrefab.Count > 0)
        {
            int rani = Random.Range(0, enemyPrefab.Count);
            activPrefab = enemyPrefab[rani];
        }
        for (int i = 0; i < manager.poolSize; i++)
        {
            GameObject enemy = Instantiate(activPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            enemy.GetComponent<TrigerMoveEnemy>().trTarget = targetPosition;
            if (enemy.activeInHierarchy)
                enemy.SetActive(false);
            enemyPool.Add(enemy);
        }
        foreach(var a in enemyPool)
        {
            a.GetComponent<EnemyHp>().EnemyDeadEvent += DeadEnemy;
            a.GetComponent<EnemyHp>().EnemyDeadEvent += DeadEnemy;
        }
    }
    private void DeletePoolEnemy()
    {
        foreach (var enemy in enemyPool)
        {
            enemy.GetComponent<EnemyHp>().EnemyDeadEvent -= DeadEnemy;
            enemy.SetActive(false);
            Destroy(enemy, 0.0f);
        }
        enemyPool.Clear();
    }
}
