using UnityEngine;
namespace Movement{
    public class Mover : MonoBehaviour
    {
        [SerializeField] protected float maxSpeed;
        [SerializeField] protected float minSpeed;
        [SerializeField] protected float acceleration;

        public virtual void Move(Vector3 direction){}
        protected virtual void ClampVelocity(){}
        public virtual Vector3 GetVelocty(){return default;}
    
    }
}