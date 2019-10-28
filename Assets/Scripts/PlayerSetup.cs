
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {
    [SerializeField]
    Behaviour[] componentsToDisabel;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    [SerializeField]
    GameObject playerUIPrefab;
    private GameObject playerUIInstance;


    Camera sceenCamera;

    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            sceenCamera = Camera.main;
            if (sceenCamera != null)
            {
                sceenCamera.gameObject.SetActive(false);
            }

            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;

        }
        GetComponent<Player>().Setup();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();

        GameManager.RegisterPlayer(_netID, _player);
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisabel.Length; i++)
        {
            componentsToDisabel[i].enabled = false;
        }
    }

    void OnDisable()
    {
        Destroy(playerUIInstance);

        if (sceenCamera != null)
        {
            sceenCamera.gameObject.SetActive(true);
        }

        GameManager.UnRegisterPlayer(transform.name);
    }

}

