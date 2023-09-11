using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Movement{
    [RequireComponent(typeof(Rigidbody))]
    public class TransformBasedMover : Mover
    {
       

        private Vector3 velocity;
        private Rigidbody rb;

        private void Awake(){
            rb = GetComponent<Rigidbody>();
        }
        public void Move()
        {
           rb.velocity = velocity *acceleration;
        }
        public void ChooseRandomVelocity()
        {
            Vector2 direction = Random.insideUnitCircle;
            velocity = new Vector3(direction.x,0,direction.y);
            acceleration = Random.Range(minSpeed,maxSpeed);
        }

        public void Flip(Vector3 normal){
            velocity = Vector3.Reflect(velocity,normal); 
            Move();
        }

    }
}