
using Unity.MLAgents;
using UnityEngine;
namespace Core{
    public class RewardPunishmentHandler : MonoBehaviour,IRewardPunishmentHandler
    {
        public void Handle(Agent agent, float amount){
            agent.AddReward(amount);
        }

    }
}