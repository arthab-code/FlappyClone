using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour, IRestartableObject
{
    private Vector3 originPosition;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 playerPosition = player.transform.position + originPosition;
        transform.position = Vector3.Lerp(originPosition, new Vector3(playerPosition.x, 0, playerPosition.z),  3f);
    }

    public void DoRestart()
    {
        transform.position = originPosition;
    }
}
