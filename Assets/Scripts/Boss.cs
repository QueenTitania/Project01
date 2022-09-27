using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject healthBar;



    public void HealthUpdate(int currentHealth)
    {
        healthBar.GetComponent<Slider>().value = currentHealth;

    }

}
