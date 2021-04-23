using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public float speed = 1.5f;
    public Vector2 limitY = new Vector2(-5, 5);
    public Vector2 limitX = new Vector2(-5, 5);
    public Transform player;

    private void Update()
    {
        Track();
    }

    private void Track()
    {
        Vector3 posCam = transform.position;
        Vector3 posPla = player.position;

        posCam = Vector3.Lerp(posCam, posPla, 0.5f * speed * Time.deltaTime);
        posCam.z = -10;

        posCam.x = Mathf.Clamp(posCam.x, limitX.x, limitX.y);
        posCam.y = Mathf.Clamp(posCam.y, limitY.x, limitY.y);

        transform.position = posCam;
    }
}