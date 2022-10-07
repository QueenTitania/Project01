using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] float shootForce = 1000;
    [SerializeField] float lifetime = 2;
    [SerializeField] ParticleSystem impactParticle;
    [SerializeField] AudioSource impactClip;
    [SerializeField] ParticleSystem shootParticle;
    [SerializeField] AudioSource shootClip;

    private ParticleSystem particleInstance;
    private AudioSource audioInstance;
    private ParticleSystem muzzleFlash;
    private AudioSource bang;

    private GameObject shot;

    private void Start()
    {

    }

    public void ShootCharge(GameObject bullet, Player player)
    {
        shot = Instantiate(bullet, player.transform.position, player.transform.rotation);
        
        ShootFX(shot);
        shot.GetComponent<Rigidbody>().AddForce(shot.transform.forward * shootForce);
        Physics.IgnoreCollision(shot.GetComponent<Collider>(), player.GetComponent<Collider>());
        Destroy(shot, lifetime);

    }

    public void BossShootCharge(GameObject bullet, Boss boss)
    {
        shot = Instantiate(bullet, boss.transform.position, boss.transform.rotation);
        
        ShootFX(shot);
        shot.GetComponent<Rigidbody>().AddForce(shot.transform.forward * shootForce);
        Physics.IgnoreCollision(shot.GetComponent<Collider>(), boss.GetComponent<Collider>());
        Destroy(shot, lifetime);

    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Health>().TakeDamage(1);

        Impact();
    }

    private void ShootFX(GameObject gun)
    {
        muzzleFlash = Instantiate(shootParticle, gun.transform.position, gun.transform.rotation);
        bang = Instantiate(shootClip, gun.transform.position, gun.transform.rotation);
        muzzleFlash.Play();
        bang.Play();
        Destroy(muzzleFlash, 1);
        Destroy(bang, shootClip.clip.length);
    }

    public void Impact()
    {
        particleInstance = Instantiate(impactParticle, this.gameObject.transform.position, this.gameObject.transform.rotation);
        audioInstance = Instantiate(impactClip, this.gameObject.transform.position, this.gameObject.transform.rotation);
        particleInstance.Play();
        audioInstance.Play();
        Destroy(particleInstance, 1);
        Destroy(audioInstance, impactClip.clip.length);
        Destroy(this.gameObject);


    }


}

