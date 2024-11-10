using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DynamiteSkill : MonoBehaviour
{
    public Button dynamiteButton; // ���� UI ����Ѻ��ʡ�� Dynamite
    public int skillCost = 100; // �ҤҢͧʡ��
    public int skillDamage = 50; // ����������·��ʡ�ŷ�
    public float cooldownTime = 30f; // ���� Cooldown 30 �Թҷ�

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
            Debug.Log("Skill Dynamite activated! Select an enemy.");
            StartCoroutine(SelectEnemyAndDamage());
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

    private IEnumerator SelectEnemyAndDamage()
    {
        bool enemySelected = false;

        while (!enemySelected)
        {
            if (Input.GetMouseButtonDown(0)) // ����ͤ�ԡ����
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Enemy enemy = hit.collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(skillDamage);
                        Debug.Log($"Dynamite used! {enemy.name} took {skillDamage} damage.");
                        enemySelected = true;
                    }
                    else
                    {
                        Debug.Log("No enemy selected. Please click on an enemy.");
                    }
                }
            }

            yield return null; 
        }
    }
}
