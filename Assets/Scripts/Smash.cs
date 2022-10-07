using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Smash : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject artNeutral;
    [SerializeField] GameObject artAnticipate;
    [SerializeField] GameObject artHit;

    [SerializeField] float anticipateTime = 0;
    [SerializeField] float hitTime = 0;
    
    [SerializeField] CameraShake cameraShake;
    [SerializeField] float shakeDuration;
    [SerializeField] float shakeMagnitude;

    [SerializeField] ParticleSystem impactParticle;
    [SerializeField] AudioSource impactClip;

    [SerializeField] ParticleSystem noimpactParticle;
    [SerializeField] AudioSource noimpactClip;

    private FieldOfView fieldfView;
    private bool smashing;
    private Coroutine currentCor = null;
    private ParticleSystem particleInstance;
    private AudioSource audioInstance;

    void Start()
    {
        fieldfView = GetComponent<FieldOfView>();
        smashing = false;
    }

    void Update()
    {
        if(fieldfView.GetSeePlayer() && Vector3.Distance(transform.position, target.transform.position) <= 5f && !smashing)
            SmashAttack();
    }

    private void SmashAttack()
    {
        smashing = true;
        if(currentCor == null)
        currentCor = StartCoroutine(WaitTime(anticipateTime, hitTime));

        smashing = false;
        
    }

    private IEnumerator WaitTime(float antAmount, float hitAmount)
    {
        //Debug.Log("co start");
        
        artAnticipate.SetActive(true);
        artNeutral.SetActive(false);

        yield return new WaitForSeconds(antAmount);

        artAnticipate.SetActive(false);
        artHit.SetActive(true);

        if(Vector3.Distance(transform.position, target.transform.position) <= 5f)
        {
            target.GetComponent<Health>().TakeDamage(1);
            ImpactHit();
        }
        else
        {
            StartCoroutine(cameraShake.Shake(shakeDuration,shakeMagnitude));
            ImpactNoHit();
        }

            
        yield return new WaitForSeconds(hitAmount);
        //Debug.Log("co hit");
        artHit.SetActive(false);
        artNeutral.SetActive(true);

        //Debug.Log("coroutine end");
        
        currentCor = null;
    }

    public void ImpactHit()
    {
        particleInstance = Instantiate(impactParticle, target.gameObject.transform.position, target.gameObject.transform.rotation);
        audioInstance = Instantiate(impactClip, target.gameObject.transform.position, target.gameObject.transform.rotation);
        particleInstance.Play();
        audioInstance.Play();
        Destroy(particleInstance, 1);
        Destroy(audioInstance, impactClip.clip.length);

    }

    public void ImpactNoHit()
    {
        particleInstance = Instantiate(noimpactParticle, gameObject.transform.position, gameObject.transform.rotation);
        audioInstance = Instantiate(noimpactClip, gameObject.transform.position, gameObject.transform.rotation);
        particleInstance.Play();
        audioInstance.Play();
        Destroy(particleInstance, 1);
        Destroy(audioInstance, impactClip.clip.length);

    }

}
