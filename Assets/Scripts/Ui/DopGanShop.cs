using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DopGanShop : MonoBehaviour
{
    [SerializeField] private GameObject DopGan;
    [SerializeField] private GameObject panelNewDopGan;
    [SerializeField] private GameObject panelApgradeDopGan;
    [SerializeField] private Button buttonNewGan;
    [SerializeField] private Text textButtonNewGan;

    [SerializeField] private int coinNewGan = 300;

    private void Start()
    {
        if (DopGan.activeInHierarchy)
            DopGan.SetActive(false);
        if (panelApgradeDopGan.activeInHierarchy)
            panelApgradeDopGan.SetActive(false);
        if (!panelNewDopGan.activeInHierarchy)
            panelNewDopGan.SetActive(true);
        buttonNewGan.onClick.AddListener(NewDopGanButtonClic);
        textButtonNewGan.text = $"{coinNewGan}";
    }
    public void NewDopGanButtonClic()
    {
        if (Coin.coin > coinNewGan)
        {
            DopGan.SetActive(true);
            Coin.coin -= coinNewGan;
            buttonNewGan = null;
            Destroy(panelNewDopGan);
            panelApgradeDopGan.SetActive(true);
        }

    }
}
