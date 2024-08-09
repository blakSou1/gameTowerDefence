using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDopGan : MonoBehaviour
{
    private List<GameObject> enemy = new();
    private GameObject targetEnemy;
    private AudioSource ay;

    [SerializeField] private GameObject _projectilePrefab;//префаб снаряда
    [SerializeField] private int _maxProject = 8;//количество снарядов в пуле
    [SerializeField] private Transform _firePoint;//точка для стрельбы
    [SerializeField] private float speed = 10f;
    [SerializeField] private int _damage = 15;
    [SerializeField] private float coolDawnTime = 3f;

    private GameObject[] projectiles;//масив снарядов в пуле

    private void Start()
    {
        ay = GetComponent<AudioSource>();

        projectiles = new GameObject[_maxProject];
        for (int i = 0; i < _maxProject; i++)
        {
            projectiles[i] = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);
            projectiles[i].GetComponent<FlashMove>().par = GetComponent<ParticleSystem>();
            projectiles[i].GetComponent<FlashMove>().damage = _damage;
            projectiles[i].SetActive(false);
        }

        StartCoroutine(TargetEnemy());
        StartCoroutine(AtackDopGan());
    }

    private void Atack()
    {
        ay.Play();
        FireProjectile();
    }

    private void FireProjectile()
    {
        foreach (var a in projectiles)
        {
            if (!a.activeInHierarchy)
            {
                a.transform.position = _firePoint.position;
                a.transform.rotation = transform.rotation;
                a.GetComponent<Rigidbody>().velocity = transform.forward * speed;
                a.SetActive(true);
                break;
            }
        }
    }

    private IEnumerator AtackDopGan()
    {
        while (true)
        {
            if (StatusVoln.statVoln)
            {
                if (targetEnemy != null)
                {
                    Vector3 targ = targetEnemy.transform.position - transform.position;
                    Quaternion tar = Quaternion.LookRotation(targ);
                    transform.rotation = Quaternion.Lerp(transform.rotation, tar, 100 * Time.deltaTime);
                    Atack();
                }
            }
            yield return new WaitForSeconds(coolDawnTime);
        }
    }
    private IEnumerator TargetEnemy()
    {
        while (true)
        {
            if (StatusVoln.statVoln)
            {
                DeleteDeadEnemy();

                GameObject target = null;
                float minDistance = 1000;
                foreach (var a in enemy)
                {
                    float dis = Vector3.Distance(transform.position, a.transform.position);
                    if (dis < minDistance)
                    {
                        minDistance = dis;
                        target = a;
                    }
                }
                targetEnemy = target;
            }
            else
            {
                enemy.Clear();
                targetEnemy = null;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void DeleteDeadEnemy()
    {
        for (int i = 0; i < t;)
        {
            foreach (var a in enemy)
            {
                if (targetEnemy == a)
                {
                    a.GetComponent<EnemyHp>().EnemyDeadEvent -= DeadEnemy;
                    t--;
                    enemy.Remove(a);
                    targetEnemy = null;
                    break;
                }
                else if (!a.activeInHierarchy)
                {
                    a.GetComponent<EnemyHp>().EnemyDeadEvent -= DeadEnemy;
                    t--;
                    enemy.Remove(a);
                    break;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.TryGetComponent(out EnemyHp enem))
        {
            enem.EnemyDeadEvent += DeadEnemy;
            enemy.Add(enem.gameObject);
        }
    }
    int t = 0;
    private void DeadEnemy()
    {
        t++;
    }
}

public static class StatusVoln
{
    public static bool statVoln = false;
}
