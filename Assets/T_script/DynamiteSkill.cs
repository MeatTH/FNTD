using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DynamiteSkill : MonoBehaviour
{
    public Button dynamiteButton; // ���� UI ����Ѻ��ʡ�� Dynamite
    public int skillCost = 100; // �ҤҢͧʡ��
    public int skillDamage = 50; // ����������·��ʡ�ŷ�
    public float cooldownTime = 30f; // ���� Cooldown 30 �Թҷ�
    public float explosionRadius = 5f; // ����բͧ������� AOE

    private Camera mainCamera;
    private bool isCooldown = false; // ��Ǩ�ͺ��ҡ��ѧ����㹪�ǧ Cooldown �������
    private float cooldownTimer; // ��ǨѺ��������Ѻ Cooldown

    void Start()
    {
        mainCamera = Camera.main;

        if (dynamiteButton != null)
        {
            dynamiteButton.onClick.AddListener(ActivateSkill);
        }
    }

    void Update()
    {
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            dynamiteButton.interactable = false; // �Դ�����ҹ����㹪�ǧ Cooldown

            if (cooldownTimer <= 0f)
            {
                isCooldown = false;
                dynamiteButton.interactable = true; // �Դ�����ҹ��������� Cooldown ����ش
            }
        }
    }

    void ActivateSkill()
    {
        if (isCooldown)
        {
            Debug.Log("Skill is on cooldown.");
            return;
        }

        if (CoinSystem.current.SpendCoins(skillCost))
        {
            Debug.Log("Skill Dynamite activated! Select a location.");
            StartCoroutine(SelectLocationAndDamage());
            StartCooldown(); // ����� Cooldown
        }
        else
        {
            Debug.Log("Not enough coins to use Dynamite.");
        }
    }

    private void StartCooldown()
    {
        isCooldown = true;
        cooldownTimer = cooldownTime;
        Debug.Log($"Dynamite skill on cooldown for {cooldownTime} seconds.");
    }

    private IEnumerator SelectLocationAndDamage()
    {
        bool locationSelected = false;

        while (!locationSelected)
        {
            if (Input.GetMouseButtonDown(0)) // ����ͤ�ԡ����
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 explosionPoint = hit.point; // ���˹觷�����Դ
                    Collider[] colliders = Physics.OverlapSphere(explosionPoint, explosionRadius); // ��Ǩ�ͺ�ѵ��������

                    foreach (Collider nearbyObject in colliders)
                    {
                        Enemy enemy = nearbyObject.GetComponent<Enemy>();
                        if (enemy != null)
                        {
                            enemy.TakeDamage(skillDamage);
                            Debug.Log($"{enemy.name} took {skillDamage} damage from Dynamite.");
                        }
                    }

                    Debug.Log($"Dynamite exploded at {explosionPoint} with radius {explosionRadius}.");
                    locationSelected = true;
                }
            }

            yield return null; // ������Ѵ�
        }
    }
}
