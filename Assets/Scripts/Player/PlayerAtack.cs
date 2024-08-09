using UnityEngine;

public class PlayerAtack : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;//префаб снаряда
    [SerializeField] private int _maxProject = 20;//количество снарядов в пуле
    [SerializeField] private Transform _firePoint;//точка для стрельбы
    [SerializeField] private float speed = 10f;
    [SerializeField] private int _damage = 15;

    private AudioSource ay;

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
    }

    bool mouseControl = false;
    private void Update()
    {
        if (StatusVoln.statVoln)
        {
            if (Input.GetMouseButtonDown(0) && !mouseControl)
            {
                ay.Play();
                mouseControl = true;
                FireProjectile();
            }
            else
                mouseControl = false;
        }
    }
    public void UpDamage(int Up)
    {
        _damage += Up;
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
                return;
            }
        }
    }

}