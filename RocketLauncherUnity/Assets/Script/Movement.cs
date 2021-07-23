using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rocketRB;
    [HideInInspector] public AudioSource rocketAS;
    [SerializeField] float mainThrust = 1000;
    [SerializeField] float rotationThrust = 100;

    // Start is called before the first frame update
    void Start()
    {
        rocketRB =  GetComponent<Rigidbody>();
        rocketAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            //Debug.Log("thrusting");
            if (!rocketAS.isPlaying) {
            rocketAS.Play();
            }
            rocketRB.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
        else {
            rocketAS.Stop();
        }

    }
    void ProcessRotation() {
        if (Input.GetKey(KeyCode.RightArrow)) {
            rocketRB.freezeRotation = true; //Turn off physics
            transform.Rotate(Vector3.forward * -1 * rotationThrust * Time.deltaTime);
            rocketRB.freezeRotation = false; //Turn on physics
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) {
            rocketRB.freezeRotation = true;
            transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
            rocketRB.freezeRotation = false;
        }
    }
}
