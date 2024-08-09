using System;
using UnityEngine;

public class TrigerMoveEnemy : MonoBehaviour
{
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [HideInInspector] public Transform trTarget;
    private Transform tr;
    public event Action Close;
    public event Action Average;
    public event Action Long;
    private float time;
    private readonly float collDawnTime = 1f;

    private void Start()
    {
        _ = TryGetComponent(out tr);
    }

    private void Update()
    {
        DistanceTriger();
    }

    private void DistanceTriger()
    {
        float distance = Vector3.Distance(tr.position, trTarget.position);

        if (distance > maxDistance)
        {
            if ((Time.time - time) > collDawnTime)
            {
                time = Time.time;
                Long?.Invoke();
            }
        }
        else if (distance > minDistance && distance < maxDistance)
            Average?.Invoke();
        else
            Close?.Invoke();
    }

}
