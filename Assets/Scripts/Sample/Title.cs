using UnityEngine;

public class Title : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Fader.instance.BlackIn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Fader.instance.BlackOut(1f, "Scenes/Game");
        }
    }
}
