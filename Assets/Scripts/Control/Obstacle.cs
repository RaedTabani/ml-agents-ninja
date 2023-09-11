using System;
using UnityEngine;
using Movement;
using Unity.VisualScripting;

namespace Control{

    [RequireComponent(typeof(TransformBasedMover))]
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private readonly float minSpawnRange = 5;
        [SerializeField] private readonly float maxSpawnRange = 9;
        
        private TransformBasedMover mover;
        private Action<Obstacle> Kill;

        private void Awake() {
            mover = GetComponent<TransformBasedMover>();
        }

        public void Init(Action<Obstacle> Kill){
            this.Kill = Kill;
            Reset();
        }

        public void Reset(){
            ResetPosition();
            ResetVelocity();
            gameObject.SetActive(true);
        }
        private void ResetPosition(){
           Vector2 position = UnityEngine.Random.insideUnitCircle.normalized;
           transform.localPosition = new Vector3(UnityEngine.Random.Range(minSpawnRange,maxSpawnRange) * position.x ,1,UnityEngine.Random.Range(minSpawnRange,maxSpawnRange) * position.y);

        }
        private void ResetVelocity(){
            mover.ChooseRandomVelocity();
            mover.Move();
        }

        private void OnCollisionEnter(Collision other) {
            if(!other.collider.CompareTag("Boundry")) return;
            mover.Flip(other.contacts[0].normal);
        }
    }
}