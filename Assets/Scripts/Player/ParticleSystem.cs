using UnityEngine;
using System.Collections.Generic;

public class ParticleSystem : MonoBehaviour
{
    [SerializeField] private UnityEngine.ParticleSystem par;
    [SerializeField] private int pool = 10;
    private List<UnityEngine.ParticleSystem> parPool = new();
    [SerializeField] private float timeParticle = 3f;

    private void Awake()
    {
        for(int i = 0; i < pool; i++)
        {
            var npar = Instantiate(par);
            npar.gameObject.SetActive(false);
            parPool.Add(npar);
        }
    }
    public UnityEngine.ParticleSystem GetParticle()
    {
        foreach(var a in parPool)
        {
            if (!a.gameObject.activeInHierarchy)
            {
                a.gameObject.SetActive(true);
                return a;
            }
        }
        return null;
    }
    List<UnityEngine.ParticleSystem> o = new();
    public void ReturnPar(UnityEngine.ParticleSystem a)
    {
        o.Add(a);
        Invoke("Ret", timeParticle);
    }
    private void Ret()
    {
        var a = o[0];
        a.Stop();
        a.gameObject.SetActive(false);
        o.Remove(a);
    }
}
