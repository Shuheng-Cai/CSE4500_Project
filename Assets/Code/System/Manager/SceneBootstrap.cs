using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBootstrap : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        var gb = PlayerManager.instance;
        var sp = spawnPoint != null ? spawnPoint : FindSpawnPoint();
        gb.player.transform.SetPositionAndRotation(sp.position, sp.rotation);
    }

    private Transform FindSpawnPoint()
    {
        var found = GameObject.FindWithTag("PlayerSpawn");
        return found != null ? found.transform : null;
    }
}
