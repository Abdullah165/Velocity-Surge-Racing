using Fusion;

public class PlayerData : NetworkBehaviour
{
   [Networked] public float RaceTime { get; set; }

   public override void FixedUpdateNetwork()
   {
       RaceTime += Runner.DeltaTime;
   }

   public override void Spawned()
   {
       GameManager.instance.RegisterPlayer(this);
   }
}
