using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamera : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _zoomSpeed;
    [SerializeField]
    private float _zoom;

    private Camera _camera;

    private Vector3 _targetPosition;
    private float _targetZoom;

    private float _zoomInSize;
    private float _zoomOutSize;

    private void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        _targetZoom = _camera.orthographicSize;
        _targetPosition = this.transform.position;

        _zoomOutSize = _camera.orthographicSize;
        _zoomInSize = _zoomOutSize - _zoom;
    }

    private void Update()
    {
        this.transform.position += (_targetPosition - this.transform.position) * _moveSpeed;
        _camera.orthographicSize += (_targetZoom - _camera.orthographicSize) * _zoomSpeed;
    }

    public void SetTargetPosition(Vector3 position)
    {
        _targetPosition = position;
    }

    public void ZoomIn()
    {
        _targetZoom = _zoomInSize;
    }

    public void ZoomOut()
    {
        _targetZoom = _zoomOutSize;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = _camera.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            _camera.transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        _camera.transform.localPosition = originalPos;
    }
}
