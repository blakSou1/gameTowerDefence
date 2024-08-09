using UnityEngine;

public class FlashMove : MonoBehaviour
{
    [HideInInspector] public int damage;
    private float time = 0;
    private readonly float healTime = 2f;
    [HideInInspector] public ParticleSystem par;

    private void Update()
    {
        time += Time.deltaTime;
        if (time > healTime)
        {
            time = 0;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        EnemyHp v;
        if (collision.gameObject.TryGetComponent<EnemyHp>(out v))
            v.EnemyDamage(damage);
        time = 0;
        UnityEngine.ParticleSystem p = par.GetParticle();
        if(p != null)
        {
            p.transform.position = transform.position;
            p.transform.rotation = transform.rotation;
            p.Play();
            par.ReturnPar(p);
        }
        gameObject.SetActive(false);
    }
}
