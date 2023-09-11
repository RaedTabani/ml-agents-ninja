using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;
using Movement;
using Unity.MLAgents.Sensors;
using Core;


namespace AI{
    [RequireComponent(typeof(PhysicsbasedMovement))]
    [RequireComponent(typeof(NinjaConfiguration))]
    public class Ninja : Agent
    {
        [SerializeField] private Spawner spawner;
        [SerializeField] private RewardPunishmentHandler rewardPunishmentHandler;
        private NinjaConfiguration configuration;
        private PhysicsbasedMovement mover;
        //New Comment
        public override void Initialize()
        {
            mover = GetComponent<PhysicsbasedMovement>();
            configuration = GetComponent<NinjaConfiguration>();

            base.Initialize();
        }

        public override void OnEpisodeBegin()
        {
            ResetPosition();
            mover.ResetVelocity();
            spawner.Spawn();
        }
        private void ResetPosition(){
           Vector2 position = UnityEngine.Random.insideUnitCircle.normalized;
           transform.localPosition = new Vector3(UnityEngine.Random.Range(configuration.minSpawnRange,configuration.maxSpawnRange) * position.x ,1,UnityEngine.Random.Range(configuration.minSpawnRange,configuration.maxSpawnRange) * position.y);
        }
        public override void CollectObservations(VectorSensor sensor)
        {
            sensor.AddObservation(transform.localPosition);
            sensor.AddObservation(mover.GetVelocty().normalized);
        }
        public override void OnActionReceived(ActionBuffers actions)
        {
            ActionSegment<float> continuousActions = actions.ContinuousActions;
            float x = Mathf.Clamp(continuousActions[0],-1,1);
            float z = Mathf.Clamp(continuousActions[1],-1,1);

            Vector3 direction = new Vector3(x,0,z);
            mover.Move(direction);

            rewardPunishmentHandler.Handle(this,configuration.existantialReward);
        }
        public override void Heuristic(in ActionBuffers actionsOut)
        {
            float x = Input.GetAxis("Horizontal");
            float z= Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(x, 0,z);
            mover.Move(direction);
        }

        private void OnCollisionEnter(Collision other){
            if(!other.collider.CompareTag("Obstacle")) return;
            rewardPunishmentHandler.Handle(this,configuration.collisionPunishment);

            spawner.Deactivate();
            EndEpisode();
        }
    }
}