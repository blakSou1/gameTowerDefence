using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private int heal = 100;
    [SerializeField] private int maxHp = 100;
    [SerializeField] private float timeInvulnerable = 1f;
    private float time = 0;

    public void PlayerDamager(int damage)
    {
        if ((Time.time - time) > timeInvulnerable)
        {
            heal -= damage;
            if (heal <= 0)
            {
                Debug.Log("You Dead!!!");
            }
            time = Time.time;
        }
    }
    public void Hill()
    {
        heal = maxHp;
    }
    public void UpMaxHp(int Up)
    {
        maxHp += Up;
    }
}
