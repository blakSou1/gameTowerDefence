using UnityEngine;
using UnityEngine.UI;

public class UIElement : MonoBehaviour
{
    [Tooltip("���������� ���������-������ ��� ������� �����")]
    public Button buttonStart;

    [Tooltip("����� ���������� ������������ ���������������� ��������� �� ����� ����")]
    public GameObject panelGame;

    [Tooltip("����� ���������� ������������ ���� ������� ����")]
    public GameObject panelPreGame;

    [Tooltip("����� ������ ��� ������� ����� �������")]
    public GameObject objectTextTimeVoln;

    [Tooltip("����� ������ ��� ����������� �������")]
    public Text objectTextCoin;

    [Tooltip("����� ������ ��� ����������� ������� �� ����� ����� � ����� ����� �������")]
    public Text objectTimeVoln;

    [Tooltip("����� ������ ��� ����������� ������ �����")]
    public Text objectNumberVoln;

    [Tooltip("����� ������ ��������")]
    public GameObject objectShopPanel;

}
