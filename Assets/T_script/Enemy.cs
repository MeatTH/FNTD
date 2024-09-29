using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 40; // ��駤�����ʹ��������� 40
    private bool killedByTurret = false; // ���������������ѵ�ٶ١�����ԧ����������

    void Start()
    {
        Debug.Log("Enemy spawned with health: " + health);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Enemy took damage, remaining health: " + health);

        if (health <= 0)
        {
            killedByTurret = true; // �ѵ�ٶ١�����ԧ���
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        // ������ѵ�ٶ١�ԧ��� �ҡ�������������­
        if (killedByTurret)
        {
            PlayerStats.AddCoins(10); // ��������­ 10 ������ѵ�ٵ�¨ҡ���ⴹ�ԧ
            Debug.Log("Coins added: 10");
        }

        Destroy(gameObject);
    }

    // �ѧ��ѹ���١���¡��������ѵ�٪��Ѻ HumanKingdom
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enemy collided with: " + other.gameObject.name); // Log �ʴ����ͧ͢�ѵ�ط�誹

        if (other.CompareTag("HumanKingdom")) // ��Ǩ�ͺ�硢ͧ�ѵ�ط�誹
        {
            PlayerStats.UpdateLives(1); // Ŵ���Ե�ͧ������ŧ 1
            Debug.Log("Player loses 1 life. Remaining lives: " + PlayerStats.Lives);

            // ź�ѵ���͡����������������­
            Destroy(gameObject); // ������ѵ������ͪ��Ѻ HumanKingdom
        }
    }
}