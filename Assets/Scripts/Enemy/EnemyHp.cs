using System;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public int hp = 100;
    public int maxHp = 100;
    public int coin = 100;

    public event Action EnemyDeadEvent;

    public void EnemyDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            if (gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
                EnemyDeadEvent?.Invoke();
            }
        }
    }

}
