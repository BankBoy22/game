using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI currentHPText;
    public TextMeshProUGUI maxHPText;
    public Slider healthSlider;

    public string characterName = "�Ľñ��� �ޱ���";
    public int maxHealth = 100;
    public int currentHealth;

    public int attackDamage = 10;
    public int defense = 5;

    void Start()
    {
        currentHealth = maxHealth;
        playerNameText.text = characterName;
        currentHPText.text = maxHealth.ToString();
        maxHPText.text = maxHealth.ToString();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    void Update()
    {
        // ���� ���� ü���� Slider UI�� �ݿ�
        healthSlider.value = currentHealth;
        currentHPText.text = currentHealth.ToString();

    }

    public void TakeDamage(int damage)
    {
        int finalDamage = Mathf.Max(damage - defense, 0);
        currentHealth -= finalDamage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // TODO: ĳ���Ͱ� ������� ���� ���� �߰�
        Debug.Log(characterName + " died!");
    }
}
