using UnityEngine;
using TMPro;

public class CoinSystem : MonoBehaviour
{
    public static int Coins; // �ӹǹ����­�Ѩ�غѹ
    public int startCoins = 1000; // �ӹǹ����­�������

    public TMP_Text coinText; // ����ʴ�����­� UI

    void Start()
    {
        Coins = startCoins; // ��˹��ӹǹ����­�������
        Debug.Log("Game started with " + Coins + " coins."); // �ʴ���� Console
        UpdateCoinText(); // �Ѿഷ UI �ͧ����­
    }

    // �ѧ��ѹ����Ѻ����ѡ����­
    public static bool SpendCoins(int amount)
    {
        if (Coins >= amount)
        {
            Coins -= amount;
            Debug.Log("Coins spent: " + amount + ". Current coins: " + Coins);
            return true;
        }
        else
        {
            Debug.Log("Not enough coins. Current coins: " + Coins);
            return false;
        }
    }

    // �ѧ��ѹ����Ѻ�����������­
    public static void AddCoins(int amount)
    {
        Coins += amount;
        Debug.Log("Coins added: " + amount + ". Current coins: " + Coins);
    }

    // �Ѿഷ UI �ͧ����­
    private void Update()
    {
        UpdateCoinText();
    }

    // �ʴ��ӹǹ����­��˹�Ҩ�
    private void UpdateCoinText()
    {
        coinText.text = "Coins: " + Coins;
    }
}