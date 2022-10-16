using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Particles
{
    /// <summary>
    /// class to manage all particles and share necessary resources between the individual particle classes
    /// </summary>
    public class ParticleManager : MonoBehaviour
    {

        #region Static Variables

        //static variable implementation to get all particles to every other particle
        private static Particle[] s_particles;

        //material for postivie and negative particles
        private static Material s_positiveMaterial, s_negativeMaterial;
        
        #endregion

        #region Monobehavior Implementation 

        //on Awake collect all the particles  within _particles and load materials
        private void Awake()
        {
            s_particles = FindObjectsOfType(typeof(Particle)) as Particle[];
            s_positiveMaterial = Resources.Load<Material>("Materials/Positive");
            s_negativeMaterial= Resources.Load<Material>("Materials/Negative") ;
        }

        #endregion


        #region Public API

        /// <summary>
        /// method to get array of all particles into other particles
        /// </summary>
        /// <returns> _particles the array of particle</returns>
        public static Particle[] GetParticles()
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

        #endregion



    }
}
