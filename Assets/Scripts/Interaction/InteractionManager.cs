using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Particles;
 namespace Interaction.Manager {
    public class InteractionManager : MonoBehaviour
    {
        #region Inpsector Elements
        [Tooltip("particle prefab")]
        [SerializeField] private GameObject _prefab;


        #endregion

        #region Private Variables

        private ParticleType _particleType= ParticleType.Postive;

        #endregion

        #region Private Methods

        private void CreateParticle() {
        
        }

        #endregion


        #region Public API



        #endregion
    }
}