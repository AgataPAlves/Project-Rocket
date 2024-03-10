 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float mainThrust = 0;
    [SerializeField] float rotationThrust = 0;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

    Rigidbody rb;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    //Rocket Thrusting
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {        
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    //Rocket Rotation
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }
    }



    private void StartThrusting()
    {
        // Thrust force
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        Debug.Log("Pressed SPACE - Thrusting");

        // Thrust Effects
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }

    private void RotateLeft()
    {
        // rotation movement
        ApplyRotation(rotationThrust);
        Debug.Log("Rotate Left");

        // rotation Effects
        if (!leftBooster.isPlaying)
        {
            leftBooster.Play();
        }
    }
    private void RotateRight()
    {
        // rotation movement
        ApplyRotation(-rotationThrust);
        Debug.Log("Rotate Right");

        // rotation Effects
        if (!rightBooster.isPlaying)
        {
            rightBooster.Play();
        }
    }

    private void StopRotation()
    {
        leftBooster.Stop();
        rightBooster.Stop();
    }
    
    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; 
    } 
}
