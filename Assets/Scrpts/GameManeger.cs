using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject dusreKaGola;
    [SerializeField] private Vector2 starpoint;
    [SerializeField] private Vector2 endpoint;
    [SerializeField] private float lerpDuration;

    private float elapsedTime = 0f;
    
    private Rigidbody2D rocketRb,dusrekagolaRb;
    void Start()
    {
        rocketRb = rocket.GetComponent<Rigidbody2D>();
        dusrekagolaRb = dusreKaGola.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / lerpDuration;

        t = Mathf.Clamp01(t);

        Vector2 currentPos = Vector2.Lerp(starpoint, endpoint, t);

        rocketRb.transform.position = new Vector2(currentPos.x, currentPos.y);
    }

    private void startingMotion() {
    }
}
