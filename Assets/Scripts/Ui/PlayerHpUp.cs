using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpUp : MonoBehaviour
{
    [SerializeField] private Button buttonUpHp;
    [SerializeField] private Button buttonHill;
    [SerializeField] private Text textUpHp;
    [SerializeField] private Text textHill;
    [SerializeField] private float nUpCoinHill = 1.2f;
    [SerializeField] private float nUpCoinUpHp = 1.2f;
    [SerializeField] private int startCoinHill = 200;
    [SerializeField] private int startCoinUpHp = 200;
    [SerializeField] private int countUpHp = 20;
    [SerializeField] private PlayerHp hp;

    private void Start()
    {
        buttonUpHp.onClick.AddListener(ClicUpHp);
        buttonHill.onClick.AddListener(ClicHill);
        textUpHp.text = $"{startCoinUpHp}";
        textHill.text = $"{startCoinHill}";
    }
    private void ClicUpHp()
    {
        if(Coin.coin > startCoinUpHp)
        {
            hp.UpMaxHp(countUpHp);
            Coin.coin -= startCoinUpHp;
            startCoinUpHp = (int)(startCoinUpHp * nUpCoinUpHp);
            textUpHp.text = $"{startCoinUpHp}";
        }
    }
    private void ClicHill()
    {
        if(Coin.coin > startCoinHill)
        {
            hp.Hill();
            Coin.coin -= startCoinHill;
            startCoinHill = (int)(startCoinHill * nUpCoinHill);
            textHill.text = $"{startCoinHill}";
        }
    }
}
