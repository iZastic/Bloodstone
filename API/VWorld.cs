using Unity.Entities;
using UnityEngine;

namespace Wetstone.API;

/// <summary>
/// Various utilities for interacting with the Unity ECS world.
/// </summary>
public static class VWorld
{
    private static World? _clientWorld;
    private static World? _serverWorld;

    /// <summary>
    /// Return the Unity ECS World instance used on the server build of VRising.
    /// </summary>
    public static World Server
    {
        get
        {
            if (_serverWorld != null) return _serverWorld;

            _serverWorld = GetWorld("Server")
                ?? throw new System.Exception("There is no Server world (yet). Did you install a server mod on the client?");
            return _serverWorld;
        }
    }

    /// <summary>
    /// Return the Unity ECS World instance used on the client build of VRising.
    /// </summary>
    public static World Client
    {
        get
        {
            if (_clientWorld != null) return _clientWorld;

            _clientWorld = GetWorld("Client_0")
                ?? throw new System.Exception("There is no Client world (yet). Did you install a client mod on the server?");
            return _clientWorld;
        }
    }

    /// <summary>
    /// Return the default Unity ECS World instance. Both client and server use this
    /// to store some "global" systems, like the InputSystem.
    /// </summary>
    public static World Default => World.DefaultGameObjectInjectionWorld;

    /// <summary>
    /// Return whether we're currently running on the server build of VRising.
    /// </summary>
    public static bool IsServer => Application.productName == "VRisingServer";

    /// <summary>
    /// Return whether we're currently running on the client build of VRising.
    /// </summary>
    public static bool IsClient => Application.productName == "VRising";

    private static World? GetWorld(string name)
    {
        foreach (var world in World.s_AllWorlds)
        {
            if (world.Name == name)
            {
                _serverWorld = world;
                return world;
            }
        }

        return null;
    }
}