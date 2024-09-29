using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Lives;
    public int startLives = 20;

    public static int Coins; // ���������Ѻ�纨ӹǹ����­
    public int startCoins = 1000;

    public TMP_Text hpText;
    public TMP_Text coinText; // ���� Text ����Ѻ�ʴ��ӹǹ����­

    void Start()
    {
        Lives = startLives;
        Coins = startCoins; // ��駤��������鹢ͧ����­
        Debug.Log("Game started with " + Lives + " lives and " + Coins + " coins.");

        hpText.text = "HP: " + Lives;
        coinText.text = "Coins: " + Coins; // �ʴ��ӹǹ����­�������
    }

    // ���ʹ����Ѻ�Ѿവ���Ե����ʴ� Log
    public static void UpdateLives(int amount)
    {
        Lives -= amount;

        // ��Ǩ�ͺ�ҡ���ԵŴŧ����͹��¡��� 0
        if (Lives < 0)
        {
            Lives = 0;
            Debug.Log("Lives updated. Current lives: " + Lives + ". Game Over.");
        }
        else if (amount < 0)
        {
            Debug.Log("Player loses " + Mathf.Abs(amount) + " life(s). Remaining lives: " + Lives);
        }
    }

    // ���ʹ����Ѻ�Ѿവ����­����ʴ� Log
    public static void AddCoins(int amount)
    {
        Coins += amount;
        Debug.Log("Coins updated. Current coins: " + Coins);
    }

    private void Update()
    {
        // �ѻവ��ͤ�������Ѻ�ʴ� HP
        if (Lives <= 0)
        {
            hpText.text = "Game over HP: " + Lives;
        }
        else
        {
            hpText.text = "HP: " + Lives;
        }

        // �ѻവ��ͤ�������Ѻ�ʴ��ӹǹ����­
        coinText.text = "Coins: " + Coins;
    }
}