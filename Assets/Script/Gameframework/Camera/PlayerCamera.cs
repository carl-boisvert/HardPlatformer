using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class PlayerCamera : MonoBehaviour
{

    [SerializeField]
    private CinemachineVirtualCamera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = gameObject.GetComponent<CinemachineVirtualCamera>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            _camera.LookAt = player.transform;
            _camera.Follow = player.transform;
        }
        else 
        {
            Debug.LogError("Camera can't find player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
