using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Lives;
    public int startLives = 20;

    void Start()
    {
        Lives = startLives;
        Debug.Log("Game started with " + Lives + " lives.");
    }

    // ���ʹ����Ѻ�Ѿവ���Ե����ʴ� Log
    public static void UpdateLives(int amount)
    {
        Lives -= amount;

        if (amount > 0)
        {
            Debug.Log("Lives decreased by " + Mathf.Abs(amount) + ". Current lives: " + Lives);
        }

        // ��Ǩ�ͺ�ҡ���ԵŴŧ����͹��¡��� 0
        if (Lives <= 0)
        {
            Lives = 0;
            Debug.Log("Lives updated. Current lives: " + Lives + ". Game Over.");
        }
        else if (amount > 0)
        {
            Debug.Log("Lives increased by " + amount + ". Current lives: " + Lives);
        }
    }
}