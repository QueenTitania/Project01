using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotate : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0;
    private FieldOfView fieldOfView;

    // Start is called before the first frame update
    void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!fieldOfView.GetSeePlayer())
            RotateBoss();
    }

    private void RotateBoss()
    {
        transform.Rotate(0,rotationSpeed,0, Space.Self);
    }

    

}
