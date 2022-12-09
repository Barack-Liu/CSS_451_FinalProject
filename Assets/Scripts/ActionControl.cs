//using UnityEngine.Networking;
using UnityEngine;
using System.Collections.Generic;
using Unity.Netcode;

public class ActionControl : NetworkBehaviour
{
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R))
    //        ResetALlPlayers();
    //}

    public void Damage(string id, int damage, string type)
    {
        if (IsServer)
            RpcDamage(id, damage, type);
        else
            CmdDamage(id, damage, type);
    }

    public void Block(string id, string type)
    {
        if (Manager.IsServer)
            RpcBlock(id, type);
        else
            CmdBlock(id, type);
    }

    public void Shoot(string id)
    {
        if (Manager.IsServer)
            RpcShoot(id);
        else
            CmdShoot(id);
    }

    public void ResetALlPlayers()
    {
        List<string> allPlayerId = Manager.GetAllPlayer();

        if (Manager.IsServer)
        {
            foreach (string id in allPlayerId)
                RpcResetPlayer(id);
        }
        else
        {
            foreach (string id in allPlayerId)
                CmdResetPlayer(id);
        }
    }

    public void SetOwnRatio(string id, string ratio)
    {
        if (Manager.IsServer)
            RpcSetOwnRatio(id, ratio);
        else
            CmdSetOwnRatio(id, ratio);
    }


    [ServerRpc]
    public void CmdDamageRpc(string id, int damage, string type)
    {
        RpcDamage(id, damage, type);
    }

    [ClientRpc]
    public void RpcDamageRpc(string id, int damage, string type)
    {
        player = Manager.GetPlayer(id);
        player.TakeDamage(damage, type);
    }
    
    [ServerRpc]
    public void CmdBlockRpc(string id, string type)
    {
        RpcBlock(id, type);
    }

    [ClientRpc]
    public void RpcBlockRpc(string id, string type)
    {
        player = Manager.GetPlayer(id);
        player.BlockAttack(type);
    }
    
    [ServerRpc]
    public void CmdSetOwnRatioRpc(string id, string ratio)
    {
        RpcSetOwnRatio(id, ratio);
    }

    [ClientRpc]
    public void RpcSetOwnRatioRpc(string id, string ratio)
    {
        player = Manager.GetPlayer(id);
        player.SetMyRatio(ratio);
    }

    [ServerRpc]
    public void CmdShootRpc(string id)
    {
        RpcShoot(id);
    }

    [ClientRpc]
    public void RpcShootRpc(string id)
    {
        player = Manager.GetPlayer(id);
        player.ShootFireball();
    }

    [ServerRpc]
    public void CmdResetPlayerRpc(string id)
    {
        RpcResetPlayer(id);
    }

    [ClientRpc]
    public void RpcResetPlayerRpc(string id)
    {
        player = Manager.GetPlayer(id);
        player.ResetAll();
    }

}
