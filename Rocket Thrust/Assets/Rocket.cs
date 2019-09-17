using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    private Rigidbody rigidbody;
    private AudioSource audioSource;
    private ParticleSystem fire;

    Vector3 startPos;
    Quaternion startRot;
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 50f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        fire = GetComponentInChildren<ParticleSystem>();
        startPos = transform.position;
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;

            default:
                SceneManager.LoadScene(0);
                break;
        }
    }
    
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            fire.Play();

            if (!audioSource.isPlaying)
            {
                audioSource.Play();

                fire.startLifetime += 0.5f;
            }

        }
        else
        {
            audioSource.Stop();

            if (fire.startLifetime > 0.5f)
            {
                fire.startLifetime -= 0.1f;
            }
            else
            {
                fire.startLifetime = 0.5f;
            }
        }
    }

    private void Rotate()
    {
        var rotateMovement = rcsThrust * Time.deltaTime;


        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotateMovement);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotateMovement);
        }
    }
}
