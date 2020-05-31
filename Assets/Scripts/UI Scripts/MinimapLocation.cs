using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapLocation : MonoBehaviour
{
    public Transform player;
    public Image playerIcon;

    void LateUpdate()
    {
        Vector3 position = player.position;
        position.y = transform.position.y;
        transform.position = position;

        playerIcon.transform.rotation = Quaternion.Euler(0, 0, -player.eulerAngles.y);
    }
}
