using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] public int maxHealth = 5;

    [SerializeField] public AudioSource dieSFX;
    [SerializeField] public ParticleSystem dieVFX;

    public int currentHealth;

    private AudioSource dieAudio;
    private ParticleSystem dieExplode;

    public event System.Action<int> HealthBarUpdate;
    public event System.Action Damaged;

    public void Start()
    {
        currentHealth = maxHealth;
    }
    public void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
            currentHealth -= damage;

        HealthUpdate();

        if (currentHealth <= 0)
            Kill();
        Debug.Log("current health " + currentHealth);

        

        //if(this.GetComponent<Player>() != null)
          //  this.GetComponent<Player>().HealthUpdate();
          
        //if(this.GetComponent<Boss>() != null)
          //  this.GetComponent<Boss>().HealthUpdate(currentHealth);
    }

    public void HealthUpdate()
    {
        HealthBarUpdate?.Invoke(currentHealth);
        Damaged?.Invoke();
    }

    public void Kill()
    {
        dieAudio = Instantiate(dieSFX, transform.position, transform.rotation);
        dieAudio.Play();
        Destroy(dieAudio, dieAudio.clip.length);

        dieExplode = Instantiate(dieVFX, transform.position, transform.rotation);
        dieExplode.Play();
        Destroy(dieExplode, 1);

        gameObject.SetActive(false);
    }

}
