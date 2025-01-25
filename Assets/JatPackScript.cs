using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
// using UnityEngine.InputSystem;

public class JatPackScript : MonoBehaviour
{

    [SerializeField]
    private AudioSource jatPackSound;

    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private float maxDown;

    [SerializeField]
    private float maxUp;

    private Vector3 player;

    private InputDevice leftHand;

    private float velocity = 0;

    private float gravity = Physics.gravity.y;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jatPackSound.volume = 0;
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, devices);
        if (devices.Count > 0)
        {
            leftHand = devices[0];
        }
        float trigger;
        leftHand.TryGetFeatureValue(CommonUsages.trigger, out trigger);
        
        if (trigger != 0)
        {
            jatPackSound.volume = trigger;
            if (!jatPackSound.isPlaying)
            {
                jatPackSound.Play();
            }
        }
        else
        {
            jatPackSound.Stop();
        }
        Jump(trigger);
        characterController.Move(player);
    }

    void Jump(float power)
    {
        velocity += ((power * -gravity * 2f) + gravity) * Time.deltaTime;
        if (velocity > maxUp) velocity = maxUp;
        if (velocity < maxDown) velocity = maxDown;
        if (velocity < 0 && transform.position.y <= 0.5f) velocity = 0;
        player = new Vector3(
            0,
            velocity,
            0);
    }
}
