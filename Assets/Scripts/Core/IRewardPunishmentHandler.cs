using Unity.MLAgents;

namespace Core{
    public interface IRewardPunishmentHandler 
    {
        public void Handle(Agent agent,float amount);
    }
}