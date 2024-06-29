using System;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Entities.Building.Components
{
    [RequireComponent(typeof(Building))]
    public class MoveBuilding : MonoBehaviour
    {
        public event Action OnPlace;
        [field: SerializeField] public bool IsPlace { get; private set; }
        [SerializeField] private GameObject _modelDoNotPlace;

        private NavMeshSurface _surface;
        
        private bool _doNotPlace;

        [Inject] 
        private void Construct(NavMeshSurface surface) =>
            _surface = surface;

        void Update()
        {
            if(IsPlace)
                return;
            
            transform.position = Helper.WorldMousePosition();

            if (!_doNotPlace && (Input.GetKeyDown(KeyCode.Space))) 
                Placement();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(IsPlace)
                return;
            
            _doNotPlace = true;
            _modelDoNotPlace.SetActive(true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(IsPlace)
                return;
            
            _doNotPlace = false;
            _modelDoNotPlace.SetActive(false);
        }

        public bool Placement()
        {
            if(_doNotPlace)
                return false;
            
            IsPlace = true;
            OnPlace?.Invoke();
            _surface.BuildNavMesh();
            return true;
        }
    }
}