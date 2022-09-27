using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject healthBar;
    private Shoot gun;
    private Slider healthSlider;

    public void Start()
    {
        gun = bullet.GetComponent<Shoot>();
        //healthSlider = healthBar.GetComponent<Slider>();
    }

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
            gun.ShootCharge(bullet, this);

    }

    public void HealthUpdate(int currentHealth)
    {
        healthBar.GetComponent<Slider>().value = currentHealth;

    }

}