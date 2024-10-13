using UnityEngine;
using TMPro;

public class CoinSystem : MonoBehaviour
{
    public static int Coins; // �ӹǹ����­�Ѩ�غѹ
    public int startCoins = 100; // �ӹǹ����­�������

    public TMP_Text coinText; // ����ʴ�����­� UI
    private static int currentTurretCost;

    private static readonly int[] turretCosts = { 0, 0, 50 }; // Costs for each turret

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

    public static int GetTurretCost(int turretIndex)
    {
        return turretCosts[Mathf.Clamp(turretIndex - 1, 0, turretCosts.Length - 1)];
    }

    public static void SetCurrentTurretCost(int cost)
    {
        currentTurretCost = cost;
    }

    public static int GetCurrentTurretCost()
    {
        return currentTurretCost;
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