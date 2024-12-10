using System;
using Fusion;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : NetworkBehaviour
{
    [SerializeField] private CinemachineCamera vcam;

    private void Awake()
    {
        vcam = FindFirstObjectByType<CinemachineCamera>();
    }

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            vcam.Follow = transform;
            vcam.LookAt = transform;
        }
    }
}
