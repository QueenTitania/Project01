using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MagicAttack : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject artNeutral;
    [SerializeField] GameObject artAnticipate;
    [SerializeField] GameObject artHit;
    [SerializeField] GameObject magicCircleObject;

    [SerializeField] GameObject standingPos;

    [SerializeField] float anticipateTime = 0;
    [SerializeField] float hitTime = 0;

    [SerializeField] CameraShake cameraShake;
    [SerializeField] float shakeDuration;
    [SerializeField] float shakeMagnitude;

    [SerializeField] ParticleSystem impactParticle;
    [SerializeField] AudioSource impactClip;

    [SerializeField] ParticleSystem magicParticle;
    [SerializeField] AudioSource magicClip;
    [SerializeField] ParticleSystem shootMagicParticle;
    [SerializeField] AudioSource shootMagicClip;
    [SerializeField] ParticleSystem circleMagicParticle;

     private ParticleSystem boomParticle;
     private AudioSource boomClip;
    
    public bool magicing = false;
    private FieldOfView fieldOfView;
    private Coroutine currentCor = null;
    private ParticleSystem magicParticleInstance;
    private AudioSource magicAudioInstance;
    private ParticleSystem particleInstance;
    private AudioSource audioInstance;
    private GameObject circleInstance;
    private Vector3 directionToTarget;

    // Start is called before the first frame update
    void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
        directionToTarget = (target.transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().isStopped = false;

        if(Vector3.Distance(transform.position, standingPos.transform.position) > 0)
            GetComponent<NavMeshAgent>().SetDestination(standingPos.transform.position);
        
        
        if(!magicing)
            Attack();
    }

    private void Attack()
    {
        magicing = true;
        if(currentCor == null)
        currentCor = StartCoroutine(WaitTime(anticipateTime, hitTime));

        magicing = false;
        
    }

    private IEnumerator WaitTime(float antAmount, float hitAmount)
    {
        //Debug.Log("anticipating");

        artAnticipate.SetActive(true);
        artNeutral.SetActive(false);
        CircleSpawn();
        
        //transform.rotation = Quaternion.Euler(0,directionToTarget,0);
        yield return new WaitForSeconds(antAmount);

        //Debug.Log("hitting");
        artAnticipate.SetActive(false);
        artHit.SetActive(true);
        ShootMagic();

        if(Vector3.Distance(circleInstance.transform.position, target.transform.position) <= 5f)
        {
            target.GetComponent<Health>().TakeDamage(2);
            ImpactHit();
        }
        else
        {
            StartCoroutine(cameraShake.Shake(shakeDuration,shakeMagnitude));
        }
        yield return new WaitForSeconds(hitAmount);

        //Debug.Log("end");
        artHit.SetActive(false);
        artNeutral.SetActive(true);
        currentCor = null;
        Destroy(circleInstance);
        
    }

    public void CircleSpawn()
    {
        
        circleInstance = Instantiate(magicCircleObject, new Vector3(target.gameObject.transform.position.x,0.2f,target.gameObject.transform.position.z), Quaternion.Euler(0,0,0));
        transform.LookAt(circleInstance.transform.position);
        magicParticleInstance = Instantiate(magicParticle, circleInstance.gameObject.transform);
        magicAudioInstance = Instantiate(magicClip, circleInstance.gameObject.transform);
        magicParticleInstance.Play();
        magicAudioInstance.Play();
        Destroy(magicParticleInstance, anticipateTime);
        Destroy(magicAudioInstance, impactClip.clip.length);

    }

    private void ImpactHit()
    {
        particleInstance = Instantiate(impactParticle, gameObject.transform.position, gameObject.transform.rotation);
        audioInstance = Instantiate(impactClip, gameObject.transform.position, gameObject.transform.rotation);
        particleInstance.Play();
        audioInstance.Play();
        Destroy(particleInstance, 1);
        Destroy(audioInstance, impactClip.clip.length);
    }


    private void ShootMagic()
    {
        magicParticleInstance = Instantiate(shootMagicParticle, this.gameObject.transform);
        particleInstance = Instantiate(circleMagicParticle, circleInstance.gameObject.transform);
        magicAudioInstance = Instantiate(shootMagicClip, circleInstance.gameObject.transform);
        magicParticleInstance.Play();
        magicAudioInstance.Play();
        particleInstance.Play();
        Destroy(magicParticleInstance, 1);
        Destroy(magicAudioInstance, shootMagicClip.clip.length);
        Destroy(particleInstance, 1);
    }
    
}
