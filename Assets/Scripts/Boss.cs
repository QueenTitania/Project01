using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject slider;
    [SerializeField] GameObject bullet;
    private Health healthBar;
    private Shoot gun;
    private Slider healthSlider;
    private int currentHealth;

     public void Start()
    {
        gun = bullet.GetComponent<Shoot>();
        healthBar = this.GetComponent<Health>();
        healthSlider = slider.GetComponent<Slider>();
        currentHealth = GetComponent<Health>().currentHealth;
        healthBar.HealthBarUpdate += OnHealthUpdate;
        
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
            gun.BossShootCharge(bullet, this);
    }

    public void OnHealthUpdate(int newHealth)
    {
        currentHealth = newHealth;
        healthSlider.value = newHealth;

        if(newHealth <= 8 && newHealth >5)
            MagicAttackPhase();

        if(newHealth <= 5 && newHealth >=3)
            SmashAttackPhase();

        if(newHealth < 3 && newHealth >0)
            MagicAttackPhase();

        if(newHealth <= 3)
        {
            healthSlider.fillRect.GetComponent<Image>().color = Color.red;
            
        }
            
        //Debug.Log("health at: " + newHealth);
    }

    private void MagicAttackPhase()
    {
        GetComponent<Smash>().enabled = false;
        GetComponent<BossRotate>().enabled = false;
        GetComponent<MagicAttack>().enabled = true;
    }

    private void SmashAttackPhase()
    {
        GetComponent<MagicAttack>().enabled = false;
        GetComponent<BossRotate>().enabled = true;
        GetComponent<Smash>().enabled = true;
    }

}
