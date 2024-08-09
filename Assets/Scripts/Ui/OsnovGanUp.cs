using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OsnovGanUp : MonoBehaviour
{
    [SerializeField] private int _UpDamage = 20;
    [SerializeField] private Button buttonUpGan;
    [SerializeField] private Text textCoinUpGan;
    [SerializeField] private int coinUp = 200;
    [SerializeField] private float UpCoinUp = 1.2f;
    [SerializeField] private PlayerAtack dam;

    private void Start()
    {
        buttonUpGan.onClick.AddListener(ClicButton);
        textCoinUpGan.text = $"{coinUp}";
    }
    private void ClicButton()
    {
        if(Coin.coin > coinUp)
        {
            dam.UpDamage(_UpDamage);
            Coin.coin -= coinUp;
            coinUp = (int)(coinUp * UpCoinUp);
            textCoinUpGan.text = $"{coinUp}";
        }
    }
}
