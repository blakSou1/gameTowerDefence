using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnManager : MonoBehaviour
{
    private UIElement UI;
    [SerializeField] private EnemyManager[] spawnPoint;
    readonly private List<EnemyManager> activManager = new();
    [SerializeField] private int countManager = 2;

    [SerializeField] private float timeInPauseVolne = 30f;
    [SerializeField] private float timeVoln = 60f;
    [SerializeField] private float timeSpawnEnemy = 3f;
    [SerializeField] private float timePreStart = 5f;

    private int volnNumber = 0;

    public int poolSize = 5;//размер пула

    public event Action CreatEnemy;
    public event Action CreatePoolEnemy;
    public event Action DeletePullEnemy;

    private void Awake()
    {
        UI = GetComponent<UIElement>();
        UI.buttonStart.onClick.AddListener(StartGameTrigger);
        UI.panelGame.SetActive(false);
        UI.panelPreGame.SetActive(true);
        foreach(var a in spawnPoint)
        {
            a.manager = GetComponent<EnemySpawnManager>();
        }
    }
    private void Start()
    {
        foreach (var a in spawnPoint)
        {
            a.gameObject.SetActive(false);
        }
        StartCoroutine(Coins());
    }

    private IEnumerator Coins()
    {
        while (true)
        {
            UI.objectTextCoin.text = $"Coin:{Coin.coin}";
            yield return new WaitForSeconds(5f);
        }
    }
    private IEnumerator TimeVoln()
    {
        for(int i = 0; i < timeVoln; i++)
        {
            UI.objectTimeVoln.text = $"{i}";
            yield return new WaitForSeconds(1f);
        }
        pauseVoln = true;
        StatusVoln.statVoln = false;
        StartCoroutine(PauseTime());
    }
    private IEnumerator PausePreStartGame()
    {
        for (float i = timePreStart; i > 0; i--)
        {
            UI.objectTextTimeVoln.GetComponent<Text>().text = $"{i}";
            yield return new WaitForSeconds(1f);
        }
        UI.objectTextTimeVoln.SetActive(false);
        NextVolnTrigger();
    }
    private IEnumerator TimePause()
    {
        for (int i = 0; i < timeInPauseVolne; i++)
        {
            UI.objectTimeVoln.text = $"{i}";
            yield return new WaitForSeconds(1f);
        }
    }

    bool pauseVoln = false;
    private IEnumerator CreateEnemyVoln()
    {
        while (!pauseVoln) 
        {
            CreatEnemy?.Invoke();
            yield return new WaitForSeconds(timeSpawnEnemy);
        }
    }
    private IEnumerator PauseTime()
    {
        bool i = false;
        UI.objectShopPanel.SetActive(true);
        DeleteActivPoint();
        CreatActivPoint();
        CreatePoolEnemy?.Invoke();
        StartCoroutine(TimePause());
        while (!i)
        {
            i = true;
            yield return new WaitForSeconds(timeInPauseVolne);
        }
        NextVolnTrigger();
    }
    private void NextVolnTrigger()
    {
        StatusVoln.statVoln = true;
        pauseVoln = false;
        StartCoroutine(CreateEnemyVoln());
        UI.objectShopPanel.SetActive(false);
        StartCoroutine(TimeVoln());
    }//изменения перед запуском новой волны

    private List<int> GenerateNum()
    {
        List<int> num = new();
        for(int i = 0; i < countManager; i++)
        {
            int ranN;
            do
            {
                ranN = UnityEngine.Random.Range(0, spawnPoint.Length);
            }
            while (num.Contains(ranN));
            num.Add(ranN);
        }
        return num;
    }

    private void CreatActivPoint()
    {
        volnNumber++;
        UI.objectNumberVoln.text = $"{volnNumber}";
        List<int> randomNum = GenerateNum();
        foreach (var a in randomNum)
        {
            var manager = spawnPoint[a];
            manager.gameObject.SetActive(true);
            activManager.Add(manager);
        }
        CreatePoolEnemy?.Invoke();
    }
    private void DeleteActivPoint()
    {
        DeletePullEnemy?.Invoke();
        foreach (var a in activManager)
            a.gameObject.SetActive(false);
        activManager.Clear();
    }

    private void StartGameTrigger()
    {
        UI.panelPreGame.SetActive(false);
        UI.panelGame.SetActive(true);
        UI.objectTextTimeVoln.SetActive(true);
        UI.objectShopPanel.SetActive(false);
        StartCoroutine(PausePreStartGame());
        CreatActivPoint();
    }
}