using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireControl : NetworkBehaviour {

    public GameObject bulletPrefab;
    public GameObject bulletSpawn;
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKeyDown("space"))
        {
            CmdShoot();
        }
	}

    void CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        // bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.transform.forward * 2000);
        // velocity translates to network
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.transform.forward * 50;
        // Spawn the bullet prefab on the server
        // NetworkServer.Spawn(bullet);
        Destroy(bullet, 5.0f);
    }

    [ClientRpc]
    void RpcCreateBullet()
    {
        // Don't need to create bullet on server because we are doing that in CmdShoot
        if (!isServer)
        {
            CreateBullet();
        }
            
    }

    [Command]
    void CmdShoot()
    {
        // This line will create bullet on server
        CreateBullet();

        // Create the bullet on ALL clients
        RpcCreateBullet();
    }
}
