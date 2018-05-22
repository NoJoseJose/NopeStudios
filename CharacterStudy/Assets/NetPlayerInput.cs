using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class NetPlayerInput : NetworkBehaviour {

    public ReactionController player;
    float x, z = 0.0f;
    float serverX, serverZ = 0.0f;
	// Use this for initialization
	void Start () {
		
	}


    void Update()
    {
        /*
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        */
        


        if (isLocalPlayer)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            CmdMoveInput(x, z);
            player.desiredVelocity.x = x;
            player.desiredVelocity.z = z;
            player.desiredVelocity = player.desiredVelocity * player.maxDesiredSpeed;
        }
        if(isServer)
        {
            RpcMoveInput(serverX, serverZ);
        }

    }
    [Command]
    public void CmdMoveInput(float cx, float cz)
    {
        serverX = cx;
        serverZ = cz;
    }
    [ClientRpc]
    public void RpcMoveInput(float sx, float sz)
    {
        
        if(isLocalPlayer)
        {
           //return;
        }

        player.desiredVelocity.x = sx;
        player.desiredVelocity.z = sz;
        player.desiredVelocity = player.desiredVelocity * player.maxDesiredSpeed;
    }

}
