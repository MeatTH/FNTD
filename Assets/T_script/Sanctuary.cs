using System.Collections.Generic;
using UnityEngine;

public class Sanctuary : MonoBehaviour
{
    public float range = 10f;           // ���зӴ�����ͺ���
    public int damage = 20;             // �ӹǹ���������
    public float fireRate = 1f;         // �ѵ�ҡ���ԧ (�ӹǹ���駵���Թҷ�)
    public string enemyTag = "Enemy";   // ���ѵ��

    public int cost;                    // ��������㹡���ҧ Sanctuary
    public int upgradeCost;             // ��������㹡���ѻ�ô Sanctuary
    public int level = 1;               // ������������� 1
    public GameObject[] sanctuaryPrefabs; // Array �ͧ prefab ����Ѻ���������

    private Transform target;           // ��������ѵ��
    private float fireCountdown = 0f;   // ��ǹѺ�����ѧ����Ѻ�ѵ�ҡ���ԧ

    void Start()
    {
        // ������鹡�����ѵ�ٷء 0.5 �Թҷ�
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {
        // ��������������� ����ͧ�ӧҹ
        if (target == null)
            return;

        // ����Ͷ֧�����ԧ
        if (fireCountdown <= 0f)
        {
            ExplodeAOE(); // ����㹺���ǳ�ͺ���
            fireCountdown = 1f / fireRate; // ������ҡ���ԧ���駶Ѵ�
        }

        fireCountdown -= Time.deltaTime;
    }

    void UpdateTarget()
    {
        // ����������ѵ�ٷ�����������
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= range)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // ����ѵ����������� �е�駤�����������
        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void ExplodeAOE()
    {
        // ����������ѵ�������
        if (target == null) return;

        // ���Դ�����˹觢ͧ�ѵ���������
        Collider[] hitEnemies = Physics.OverlapSphere(target.position, range);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag(enemyTag))
            {
                Enemy enemyComponent = enemy.GetComponent<Enemy>();
                if (enemyComponent != null)
                {
                    enemyComponent.TakeDamage(damage); // �Ӵ��������ѵ��
                }
            }
        }
    }

    // �ѧ��ѹ����Ѻ����ѻ�ô Sanctuary (����͹�Ѻ������ Turret)
    public void UpgradeSanctuary()
    {
        if (level < sanctuaryPrefabs.Length)
        {
            int currentPrefabIndex = level - 1; // ���Թ�硫�Ѩ�غѹ����Ѻ prefab
            level++;
            UpgradeToNextLevelPrefab(currentPrefabIndex); // �ѻ�ô��ѧ prefab �Ѵ�
            upgradeCost += 100; // ������������㹡���ѻ�ô
        }
        else
        {
            Debug.Log("Sanctuary ������дѺ�٧�ش����!");
        }
    }

    private void UpgradeToNextLevelPrefab(int currentPrefabIndex)
    {
        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;
        Destroy(gameObject);

        GameObject newSanctuary = Instantiate(sanctuaryPrefabs[currentPrefabIndex + 1], currentPosition, currentRotation);
        newSanctuary.GetComponent<Sanctuary>().level = level;
        Debug.Log($"Sanctuary �ѻ�ô������� {level}! ����¹��� prefab ����");
    }

    // �ʴ����С�÷Ӵ���� AOE �� Scene View
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
