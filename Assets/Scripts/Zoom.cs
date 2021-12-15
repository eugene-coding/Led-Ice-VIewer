using UnityEngine;

public class Zoom : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _maxDistance = 20;
    [SerializeField] private float _minDistance = .6f;
    [SerializeField] private float _zoomRate = 10.0f;
    [SerializeField] private float _zoomDamping = 5.0f;

    private float _currentDistance;
    private float _desiredDistance;
    private Vector3 _position;

    private float DesiredDistance
    {
        get => _desiredDistance;
        set => _desiredDistance = Mathf.Clamp(value, _minDistance, _maxDistance);
    }

    private void Start()
    { 
        Initialize(); 
    }

    public void Initialize()
    {
        var distance = Vector3.Distance(transform.position, _target.position);

        _currentDistance = distance;
        DesiredDistance = distance;

        _position = transform.position;
    }

    void LateUpdate()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPreviousPosition = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePreviousPosition = touchOne.position - touchOne.deltaPosition;

            var prevTouchDeltaMag = (touchZeroPreviousPosition - touchOnePreviousPosition).magnitude;
            var TouchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            var deltaMagDiff = prevTouchDeltaMag - TouchDeltaMag;

            DesiredDistance += deltaMagDiff * Time.deltaTime * _zoomRate * 0.0025f * Mathf.Abs(DesiredDistance);
        }

        _currentDistance = Mathf.Lerp(_currentDistance, DesiredDistance, Time.deltaTime * _zoomDamping);

        _position = _target.position - (Vector3.forward * _currentDistance);

        transform.position = _position;
    }
}