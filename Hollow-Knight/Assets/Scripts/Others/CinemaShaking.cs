using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemaShaking : MonoBehaviour
{
    private Cinemachine.CinemachineImpulseSource myImpulse;

    private void Start()
    {
        myImpulse = GetComponent<Cinemachine.CinemachineImpulseSource>();
    }

    public void CinemaShake()
    {
        myImpulse.GenerateImpulse();
    
    }

}
