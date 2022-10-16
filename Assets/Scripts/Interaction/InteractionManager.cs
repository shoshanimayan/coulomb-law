using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Particles;
using System;
using UnityEngine.EventSystems;

namespace Interaction.Manager {
    public class InteractionManager : MonoBehaviour
    {
        #region Inpsector Elements
        [Tooltip("particle prefab")]
        [SerializeField] private GameObject _prefab;


        #endregion

        #region Private Variables

        private ParticleType _particleType= ParticleType.Postive;
        private float _distance;
        #endregion

        #region Private Methods

        private void Start()
        {
            var obj= GameObject.Find("Particle");
            _distance = Vector3.Distance(Camera.main.transform.position, obj.transform.position);
        }

        private void Update()
        {
            
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                   

                    // Construct a ray from the current touch coordinates
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    // Create a particle if hit
                    if (!Physics.Raycast(ray))
                    {
                        CreateParticle(ray.GetPoint(_distance), Quaternion.identity);
                    }
                }
                else if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    
                    // Construct a ray from the current touch coordinates
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    // Create a particle if hit
                    if (!Physics.Raycast(ray))
                    {
                        CreateParticle(ray.GetPoint(_distance), Quaternion.identity);
                    }
                }
            
        }


        private void CreateParticle(Vector3 position, Quaternion rotation) 
        {
            position = new Vector3(position.x, position.y, 0);
            var particle =Instantiate(_prefab, position, rotation);
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
            ParticleManager.AddParticle(particle);
        }

        #endregion


        #region Public API

        public void SetParticleType(int index) 
        {
            if (index >= Enum.GetNames(typeof(ParticleType)).Length)
            {
                throw new Exception("index out of range");
            }
            _particleType = (ParticleType)index;
        }

        #endregion
    }
}