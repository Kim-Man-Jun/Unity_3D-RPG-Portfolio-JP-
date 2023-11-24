using Cinemachine;
using System;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField][Range(0f, 10f)] private float defaultDistance = 6f;
    [SerializeField][Range(0f, 10f)] private float minDistance = 2f;
    [SerializeField][Range(0f, 10f)] private float maxDistance = 6f;

    [SerializeField][Range(0f, 10f)] private float smoothing = 4f;
    [SerializeField][Range(0f, 10f)] private float zoomSensitivity = 1f;

    private CinemachineFramingTransposer framingTransposer;

    private CinemachineVirtualCamera virtualCamera;
    private GameObject player;

    private void Awake()
    {
        framingTransposer = GetComponent<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        Transform cameraLook = player.transform.Find("cameraLookPoint");

        virtualCamera.Follow = cameraLook;
        virtualCamera.LookAt = cameraLook;
    }

    private void Update()
    {
        Zoom();
    }

    private void Zoom()
    {
        float zoomValue = Input.GetAxisRaw("Mouse ScrollWheel") * zoomSensitivity;

        framingTransposer.m_CameraDistance -= zoomValue;

        if (framingTransposer.m_CameraDistance < minDistance)
        {
            framingTransposer.m_CameraDistance = minDistance;
        }
        else if (framingTransposer.m_CameraDistance > maxDistance)
        {
            framingTransposer.m_CameraDistance = maxDistance;
        }
    }
}
