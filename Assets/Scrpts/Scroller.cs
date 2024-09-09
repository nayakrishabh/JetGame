using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;


    private float x = 0, y = 0.1f;

    public static Scroller Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    public void scrollingLogic(Vector2 uvpos) {
        rawImage.uvRect = new Rect(rawImage.uvRect.position + uvpos * Time.deltaTime, rawImage.uvRect.size);
    }
}
    

