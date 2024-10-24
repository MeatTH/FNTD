using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;
    public int damage = 10;            // ��駤�Ҵ�����ͧ����ع�� 10
    public float explosionRadius = 5f; // ����ա�����Դ (��˹����� AOE)
    public bool isExplodeOnStart;

    private void Start()
    {
        if(isExplodeOnStart)
        {
            HitTarget();
        }
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        // ����ͪ�������� ���¡��ѧ��ѹ�Ӵ����Ẻ AOE
        Explode();
        Destroy(gameObject);
    }

    void Explode()
    {
        // ��������·�����������ա�����Դ
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            Enemy e = collider.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(damage); // �Ӵ�������Ѻ�ѵ�ٷ������������
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // �ʴ�����ա�����Դ��������͡����ع� Scene View
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
