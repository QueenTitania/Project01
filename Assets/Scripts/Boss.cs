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

     public void Start()
    {
        gun = bullet.GetComponent<Shoot>();
        healthBar = this.GetComponent<Health>();
        healthSlider = slider.GetComponent<Slider>();

        healthBar.HealthBarUpdate += OnHealthUpdate;
        
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
            gun.BossShootCharge(bullet, this);
    }

    public void OnHealthUpdate(int newHealth)
    {
        healthSlider.value = newHealth;

        if(newHealth < 3)
            healthSlider.fillRect.GetComponent<Image>().color = Color.red;
        //Debug.Log("health at: " + newHealth);
    }

}
