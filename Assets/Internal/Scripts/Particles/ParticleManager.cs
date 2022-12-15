using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Particles
{
    public enum ParticleType {Postive,Negative }

    /// <summary>
    /// class to manage all particles and share necessary resources between the individual particle classes
    /// </summary>
    public class ParticleManager : MonoBehaviour
    {

        #region Inpsector Elements
        [Tooltip("particle prefab")]
        [SerializeField] private GameObject _prefab;

        [Tooltip("spawn location")]
        [SerializeField] private GameObject _SpawnPoint;
        #endregion

        #region Static Variables

        //static variable implementation to get all particles to every other particle
        private static List<Particle> s_particles;

        //material for postivie, negative, and grabbed particles
        private static Material s_positiveMaterial, s_negativeMaterial,s_heldMaterial;

        #endregion

        #region Private Variables

        private ParticleType _particleType = ParticleType.Postive;
        
        #endregion

        #region Monobehavior Implementation 

        //on Awake collect all the particles  within _particles and load materials
        private void Awake()
        {
            s_particles = new List<Particle>( FindObjectsOfType(typeof(Particle)) as Particle[]);
            s_positiveMaterial = Resources.Load<Material>("Materials/Positive");
            s_negativeMaterial= Resources.Load<Material>("Materials/Negative") ;
            s_heldMaterial = Resources.Load<Material>("Materials/Held");

        }

        #endregion

        #region Private Methods

        private void CreateParticle()
        {
            var position = _SpawnPoint.transform.position;
            var particle = Instantiate(_prefab, position, Quaternion.identity);
            var charge = 0;
            switch (_particleType)
            {
                case ParticleType.Postive:
                    charge = 1;
                    break;
                case ParticleType.Negative:
                    charge = -1;
                    break;
            }
            particle.GetComponent<Particle>().SetCharge(charge);
            AddParticle(particle);
        }

        private void AddParticle(GameObject ParticleObject)
        {
            s_particles.Add(ParticleObject.GetComponent<Particle>());
        }

       

        #endregion


        #region Public API

        /// <summary>
        /// method to get array of all particles into other particles
        /// </summary>
        /// <returns> _particles the array of particle</returns>
        public static List<Particle> GetParticles()
        {
            return s_particles;
        }

        /// <summary>
        /// returns the positive particle material
        /// </summary>
        /// <returns>material </returns>
        public static Material GetPositiveMaterial()
        {
            return s_positiveMaterial;
        }

        /// <summary>
        /// returns the negative particle material
        /// </summary>
        /// <returns>material </returns>
        public static Material GetNegativeMaterial()
        {
            return s_negativeMaterial;
        }


        public static Material GetHeldMaterial()
        {
            return s_heldMaterial;
        }



        public void SetParticleType(int index)
        {
            if (index >= Enum.GetNames(typeof(ParticleType)).Length)
            {
                throw new Exception("index out of range");
            }
            _particleType = (ParticleType)index;
            CreateParticle();
        }


        public void ClearParticles()
        {
            foreach (var partical in s_particles)
            {
                Destroy(partical);
            }
            s_particles.Clear();
        }


        #endregion



    }
}
