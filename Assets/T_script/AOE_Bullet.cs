using UnityEngine;

public class AOE_Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;
    public int damage = 10; // ���������ѵ�ٷء��Ǩ����Ѻ
    public float explosionRadius = 5f; // ����ա�����Դ

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // �������������� ����¡���ع
            return;
        }

        Vector3 dir = target.position - transform.position; // �ӹǳ��ȷҧ����������
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            Explode(); // ��ҡ���ع�������������¾� ���ӡ�����Դ
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World); // ����͹������ع
        transform.LookAt(target); // ������ع��ع仵���������
    }

    void Explode()
    {
        // ���ҧ������Դ��ǧ��������������ҡѺ explosionRadius
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, explosionRadius);

        // �Ӵ�������Ѻ�ѵ�ٷء��Ƿ�����������ա�����Դ
        foreach (Collider enemy in hitEnemies)
        {
            Enemy e = enemy.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(damage); // �Ӵ�������Ѻ�ѵ��
            }
        }

        // ����¡���ع��ѧ�ҡ������Դ
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // �ʴ�����ա�����Դ� Scene View ���ͪ���㹡�ôպѡ
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
