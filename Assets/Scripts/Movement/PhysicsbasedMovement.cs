using UnityEngine;

namespace Movement{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicsbasedMovement : Mover
    {
        
        private Rigidbody rb;

        private void Start() {
            rb = GetComponent<Rigidbody>();
        }
        private void Update() {
            ClampVelocity(); 
        }
        public override void Move(Vector3 direction)
        {
            rb.AddForce(direction * acceleration,ForceMode.Force);
        }
        protected override void ClampVelocity(){
            rb.velocity = rb.velocity.magnitude >maxSpeed ? rb.velocity.normalized * maxSpeed: rb.velocity;
        }
        public override Vector3 GetVelocty()
        {
            return rb.velocity;
        }
        public void ResetVelocity(){
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

    }

}
