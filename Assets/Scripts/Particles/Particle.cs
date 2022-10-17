using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Particles
{
    /// <summary>
    /// particle class to handle the negative and positive particle behaviors in coulombs law
    /// </summary>
    public class Particle : MonoBehaviour, IParticle
    {
        #region Inpsector Elements
        [Tooltip("charge to determine if particle is negative or positive, Charge cannot be 0")]
        [SerializeField] private float _charge = 1;

        [Tooltip("exponent for the charge to help create very larger or smaller numbers within the inspector ( charge * (10^exponent))")]
        [SerializeField] private int _exponent = -5;

        #endregion


        #region Private Variables
        // k value for coulombs formula (could be 8.99 instead of 9 but sources varied and this is simpler)
        private float _k = 9 * Mathf.Pow(10, 9);

        //rigidbody for applying force
        private Rigidbody _rb;

        //is grabbed
        private bool _grabbed;

        #endregion

        #region Monobehavior Implementations




        /// <summary>
        /// set rigidbody before start
        /// </summary>
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
           
        }

        
        /// <summary>
        ///  Start is called before the first frame update, gets array of all particles, sets up + - visuals
        /// </summary>
        private void Start()
        {
            _k = 9 * Mathf.Pow(10, 9);


            if (_charge > 0)
            {
                GetComponentsInChildren<TextMeshProUGUI>()[0].text = "+";
                GetComponent<Renderer>().material = ParticleManager.GetPositiveMaterial();

            }
            else
            {
                GetComponentsInChildren<TextMeshProUGUI>()[0].text = "-";
                GetComponent<Renderer>().material = ParticleManager.GetNegativeMaterial();

            }
        }


        /// <summary>
        /// fixed updated is called every timestep to apply Coulombs law on particle
        /// </summary>
        private void FixedUpdate()
        {
            if (!_grabbed)
            {
                foreach (Particle particle in ParticleManager.GetParticles())
                {

                    if (particle != this && !particle.IsGrabbed())
                    {
                        ApplyCoulombsLaw(particle);
                    }
                }
            }
        }


        /// <summary>
        /// make sure _charge cannot be set to 0 in inspector
        /// </summary>
        private void OnValidate()
        {
            if (_charge == 0)
            {
                _charge = .001f;
            }
        }

        #endregion

        #region Maths


        /// <summary>
        /// applies Coulombs law with directional force onto the particle, 
        /// if the distance between two particles is zero, return to avoid divide by zero error
        /// </summary>
        /// <param name="particle"> other particle </param>
        private void ApplyCoulombsLaw(Particle particle)
        {
            if (Vector3.Distance(transform.position, particle.transform.position) == 0)
            {
                return;
            }

            _rb.AddForce(GetForceValue(particle) * GetDirectonalVector(particle));
        }

        /// <summary>
        /// returns the directional vector for the other particle coming to this one
        /// </summary>
        /// <param name="particle"> other particle </param>
        /// <returns>Vector3 directional vector</returns>
        private Vector3 GetDirectonalVector(Particle particle)
        {
            return (transform.position - particle.transform.position).normalized;
        }

        /// <summary>
        /// gets the force to be applied onto this particle by the other particle through coulombs law
        /// </summary>
        /// <param name="particle">other particle</param>
        /// <returns></returns>
        private float GetForceValue(Particle particle)
        {
            float distance = Vector3.Distance(transform.position, particle.transform.position);
            return ((_k * ((_charge * Mathf.Pow(10, _exponent)) * particle.GetCharge())) / Mathf.Pow(distance, 2));

        }

        #endregion



        #region Public API

        public Particle(float charge, int exponent=-5)
        {
            _charge = charge;
            _exponent = exponent;
        }

        /// <summary>
        /// method to return charge to other classes
        /// </summary>
        /// <returns>charge in float form</returns>
        public float GetCharge()
        {
            return _charge * (Mathf.Pow(10, _exponent));
        }

        public void SetCharge(float charge, int exponent = -5)
        {
            _charge = charge;
            _exponent = exponent;
        }

        public void Grab(bool grabbed) 
        {
            if (grabbed)
            {
                _grabbed = true;
            }
            else
            {
                _grabbed = false;
            }
        }

        public bool IsGrabbed()
        {
            return _grabbed;
        }

        #endregion
    }
}
