using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

    public float shakeAmount = 0.25f;
    public float decreaseFactor = 1.0f;

    private new Camera camera;
    private Vector3 cameraPos;
    private float shakeValue = 0.0f;

    void Awake()
    {
        this.camera = (Camera)this.GetComponent<Camera>();

        if (this.camera == null)
        {
            Debug.Log("CameraShake: Unable to find 'Camera' component attached to GameObject.");
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check if the screen should be shaking.
        if (this.shakeValue > 0.0f)
        {
            // Shake the camera.
            this.camera.transform.localPosition = cameraPos + (Random.insideUnitSphere * this.shakeAmount * this.shakeValue);

            // Reduce the amount of shaking for next tick.
            this.shakeValue -= Time.deltaTime * this.decreaseFactor;

            // Check to see if we've stopped shaking.
            if (this.shakeValue <= 0.0f)
            {
                // Clamp the shake amount back to zero, and reset the camera position to our cached value.
                this.shakeValue = 0.0f;
                this.camera.transform.localPosition = this.cameraPos;
            }
        }
    }

    public void shake(float amount)
    {
        // Check if we're already shaking.
        if (this.shakeValue <= 0.0f)
        {
            // If we aren't, cache the camera position.
            this.cameraPos = this.camera.transform.position;
        }

        // Set the 'shake' value.
        this.shakeValue = amount;
    }
}
