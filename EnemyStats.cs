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

    public string characterName = "후시구로 메구미";
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
        // 적의 현재 체력을 Slider UI에 반영
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
        // TODO: 캐릭터가 사망했을 때의 로직 추가
        Debug.Log(characterName + " died!");
    }
}
