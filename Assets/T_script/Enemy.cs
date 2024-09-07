using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 40; // ��駤�����ʹ��������� 40

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
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }

    // �ѧ��ѹ���١���¡��������ѵ�٪��Ѻ HumanKingdom
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enemy collided with: " + other.gameObject.name); // Log �ʴ����ͧ͢�ѵ�ط�誹

        if (other.CompareTag("HumanKingdom")) // ��Ǩ�ͺ�硢ͧ�ѵ�ط�誹
        {
            Debug.Log("Enemy reached HumanKingdom!"); // ��Ǩ�ͺ��Ҫ� HumanKingdom
            PlayerStats.Lives -= 1; // Ŵ���Ե�ͧ������ŧ 1
            Debug.Log("Player loses 1 life. Remaining lives: " + PlayerStats.Lives);

            Destroy(gameObject); // ������ѵ������ͪ��Ѻ HumanKingdom
        }
    }
}
