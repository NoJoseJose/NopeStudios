using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

//using joeythelantern's online tut basis for a manual network message

public abstract class NetworkMessageHandler : NetworkBehaviour
{
    public const short movement_msg = 255;

    public class PlayerMovementMessage : MessageBase
    {
        public string objectTransformName;
        public Vector3 objectPosition;
        public Quaternion objectRotation;
        public float time;
    }
}