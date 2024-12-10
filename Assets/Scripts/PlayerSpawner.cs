using Fusion;
using Unity.Mathematics;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private GameObject carPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            var spawnedCarPosition = new Vector3(player.RawEncoded % Runner.Config.Simulation.PlayerCount * 3, 1, 0);
            Runner.Spawn(carPrefab, spawnedCarPosition, quaternion.identity, player);
        }
    }
}