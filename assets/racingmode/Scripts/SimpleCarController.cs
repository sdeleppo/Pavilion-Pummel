 using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
 
    [System.Serializable]
    public class AxleInfo {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor;
        public bool steering;
    }
   
 [RequireComponent (typeof(AudioSource))]     
    public class SimpleCarController : MonoBehaviour {
        //Handling
        public List<AxleInfo> axleInfos; 
        public float maxMotorTorque;
        public float maxSteeringAngle;
        public float brakeForce;

        float motor;
        float steering;

        public Vector3 centerOfMassCorrection;
        public AudioClip collisionSound;
     //Components
        AudioSource a;
        Rigidbody r;
        //Internal
        public bool controlled;
        public bool braking;
        WheelHit wHit;
     [Header ("Effects")]
        public AudioSource slipSound;
     public ParticleSystem[] tiresmoke;


        void Start()
        {
            r = GetComponent<Rigidbody>();
            r.centerOfMass = centerOfMassCorrection;
            a = GetComponent<AudioSource>();
        }
         
        // finds the corresponding visual wheel
        // correctly applies the transform
        public void ApplyLocalPositionToVisuals(WheelCollider collider)
        {
            if (collider.transform.childCount == 0) {
                return;
            }
         
            Transform visualWheel = collider.transform.GetChild(0);
         
            Vector3 position;
            Quaternion rotation;
            collider.GetWorldPose(out position, out rotation);
         
            visualWheel.transform.position = position;
            visualWheel.transform.rotation = rotation;
        }

        public void ControlVehicle(Vector3 input)
        {
            motor = maxMotorTorque * input.z;
            steering = maxSteeringAngle * input.x;
        }

        public void FixedUpdate()
        {
            //Change pitch of engine depending on sound
            a.pitch = 1 + (r.velocity.magnitude / 10);

            if (controlled)
            {

                foreach (AxleInfo axleInfo in axleInfos)
                {
                    if (axleInfo.leftWheel.GetGroundHit(out wHit))
                    {
                        if (wHit.sidewaysSlip < -0.4f || wHit.sidewaysSlip > 0.4f)
                        {
                            if (!slipSound.isPlaying) { slipSound.Play(); }
                            foreach (ParticleSystem smoke in tiresmoke) { smoke.Play(); }
                        }
                        else
                        {
                            //slipSound.Stop();
                        }
                    }

                    if (axleInfo.steering)
                    {
                        axleInfo.leftWheel.steerAngle = steering;
                        axleInfo.rightWheel.steerAngle = steering;
                    }
                    if (axleInfo.motor)
                    {
                        axleInfo.leftWheel.motorTorque = motor;
                        axleInfo.rightWheel.motorTorque = motor;
                    }

				//Braking
				if (braking)
				{
					axleInfo.leftWheel.brakeTorque = brakeForce;
					axleInfo.rightWheel.brakeTorque = brakeForce;
				}
				else
				{
					axleInfo.leftWheel.brakeTorque = 0;
					axleInfo.rightWheel.brakeTorque = 0;
				}

                    ApplyLocalPositionToVisuals(axleInfo.leftWheel);
                    ApplyLocalPositionToVisuals(axleInfo.rightWheel);
                }
            }
        }

        void OnCollisionEnter(Collision col)
        {
            if (col.transform.tag == "Vehicle" || col.transform.tag == "Obstacle")
            {
                PlayCollisionSound();
            }
        }

        void PlayCollisionSound()
        {
            float currentPitch = a.pitch;
            a.pitch = 1 + Random.Range(-.1f, .1f);
            a.PlayOneShot(collisionSound);
            a.pitch = currentPitch;
        }

        void OnDrawGizmos()
        {
                Gizmos.color = Color.white;
                Gizmos.DrawWireSphere(transform.TransformDirection(centerOfMassCorrection) + transform.position, 0.25f);
        }


    }