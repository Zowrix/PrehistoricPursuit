using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public enum EaseType
    {
        Linear,
        EaseIn,
        EaseOut,
        EaseInOut
    }

    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    public float platformSpeed;
    public EaseType easeType;

    private Vector3 _currentTarget;
    private float _t = 0f;
    private float _direction = 1f; // La plateforme commence en allant vers l'endPoint

    void Start()
    {
        _currentTarget = _endPoint.position;
    }

    void FixedUpdate()
    {
        _t += _direction * Time.deltaTime / platformSpeed;
        _t = Mathf.Clamp01(_t);

        float easedT = GetEase(_t, easeType);

        transform.position = Vector3.Lerp(_startPoint.position, _endPoint.position, easedT);

        if (_t >= 1f || _t <= 0f)
        {
            _direction *= -1f;
            _currentTarget = _direction > 0f ? _endPoint.position : _startPoint.position;
        }
    }

    private float GetEase(float t, EaseType easeType)
    {
        switch (easeType)
        {
            case EaseType.Linear:
                return t;
            case EaseType.EaseIn:
                return Mathf.Pow(t, 2f);
            case EaseType.EaseOut:
                return 1f - Mathf.Pow(1f - t, 2f);
            case EaseType.EaseInOut:
                if (t < 0.5f)
                {
                    return 2f * Mathf.Pow(t, 2f);
                }
                else
                {
                    return 1f - 2f * Mathf.Pow(1f - t, 2f);
                }
            default:
                return t;
        }
    }
}