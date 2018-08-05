using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook {


    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();

        LobbyManager lm = GetComponent<LobbyManager>();
        // car is the SetupLocalPlayer script.
        SetupLocalPlayer player = gamePlayer.GetComponent<SetupLocalPlayer>();


        // pName and pColor are variables in that script. Thus you can call those values using car.pName.
        // Want to get the team number value here and sync it
        player.pName = lobby.playerName;
        base.OnLobbyServerSceneLoadedForPlayer(manager, lobbyPlayer, gamePlayer);
    }
}
