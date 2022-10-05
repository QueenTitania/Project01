using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject slider;
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

        if (Input.GetKeyDown(KeyCode.Space))
            gun.ShootCharge(bullet, this);

        
    }

    public void OnHealthUpdate(int newHealth)
    {
        healthSlider.value = newHealth;

        if(newHealth < 3)
            healthSlider.fillRect.GetComponent<Image>().color = Color.red;
        //Debug.Log("health at: " + newHealth);
    }



}