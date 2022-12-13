using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class Wobble : MonoBehaviour
    {
        [SerializeField] private float speed = 2f;
        [SerializeField]private float maxRotation = 30f;


        // Update is called once per frame
        void Update()
        {
            //squish effect
            // gameObject.transform.rotation = Quaternion.Euler(0, maxRotation * Mathf.Sin(Time.time * speed), 0);


            //wobble
            var targetAngle = Quaternion.Euler(Vector3.forward * maxRotation* (Mathf.Sin(Time.time*speed)));
            transform.rotation = Quaternion.Lerp(transform.rotation, targetAngle, Time.deltaTime);
        }
    }
}