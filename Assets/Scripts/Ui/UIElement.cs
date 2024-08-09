using UnityEngine;
using UnityEngine.UI;

public class UIElement : MonoBehaviour
{
    [Tooltip("визуальный интерфейс-кнопка для запуска волны")]
    public Button buttonStart;

    [Tooltip("обект интерфейса составляющий пользовательский интерфейс во время игры")]
    public GameObject panelGame;

    [Tooltip("обект интерфейса составляющий пред игровое меню")]
    public GameObject panelPreGame;

    [Tooltip("обект текста для времени перед стартом")]
    public GameObject objectTextTimeVoln;

    [Tooltip("обект текста для отображения баланса")]
    public Text objectTextCoin;

    [Tooltip("обект текста для отображения времени до конца волны и паузы между волнами")]
    public Text objectTimeVoln;

    [Tooltip("обект текста для отображения номера волны")]
    public Text objectNumberVoln;

    [Tooltip("обект панели магазина")]
    public GameObject objectShopPanel;

}
