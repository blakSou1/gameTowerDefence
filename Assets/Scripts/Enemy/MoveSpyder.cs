using UnityEngine;
using UnityEngine.AI;

public class MoveSpyder : MonoBehaviour
{
    private TrigerMoveEnemy tr;
    private NavMeshAgent agent;
    [SerializeField] private float coolDawnAtack = 0.8f;
    private float time = 0;
    [SerializeField] private float rotSpeed = 10f;
    [SerializeField] private int damage = 10;
    [SerializeField] private Transform GanSpyder;

    private void Start()
    {
        tr = GetComponent<TrigerMoveEnemy>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        tr = GetComponent<TrigerMoveEnemy>();
        tr.Long += LongMove;
        tr.Average += AverageMove;
        tr.Close += CloseMove;
    }
    private void OnDisable()
    {
        if (tr != null)
        {
            tr.Long -= LongMove;
            tr.Average -= AverageMove;
            tr.Close -= CloseMove;
        }
        else
            Debug.Log("ну все капут переменная tr пуста");
    }

    private float longTime = 0;
    private void LongMove()
    {
        if ((Time.time - longTime) > (coolDawnAtack * 2))
        {
            _ = agent.SetDestination(tr.trTarget.position);
            longTime = Time.time;
        }
    }
    private void AverageMove()
    {
        _ = agent.SetDestination(tr.trTarget.position);
    }

    private void CloseMove()
    {
        Vector3 posGan = GanSpyder.position;
        agent.ResetPath();
        Vector3 dir = tr.trTarget.position - posGan;
        Quaternion loocRot = Quaternion.LookRotation(dir);
        Vector3 rot = Quaternion.Lerp(GanSpyder.rotation, loocRot, rotSpeed * Time.deltaTime).eulerAngles;
        GanSpyder.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);

        SpyderAtackReykast();
        //атакуем
    }

    private void SpyderAtackReykast()
    {
        //эфект огня для атаки/лазера
        if ((Time.time - time) > coolDawnAtack)
        {
            tr.trTarget.GetComponent<PlayerHp>().PlayerDamager(damage);
            time = Time.time;
        }
    }

}
