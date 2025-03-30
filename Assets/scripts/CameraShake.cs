using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    public static CameraShake Instance { get; private set; }

    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;
    private float shakeTimer;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogWarning("CameraShake: Instance already Exist. Removing Object");
            Destroy(gameObject);
        }

        noise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    private void Update() {
        if (shakeTimer > 0) {
            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0f && noise != null) {
                noise.m_AmplitudeGain = 0f;
                noise.m_FrequencyGain = 0f;
            }
        }
    }

    public void Shake(float intensity, float time) {
        if (noise == null) return;

        noise.m_AmplitudeGain = intensity;
        noise.m_FrequencyGain = 1f;
        shakeTimer = time;
    }

}
