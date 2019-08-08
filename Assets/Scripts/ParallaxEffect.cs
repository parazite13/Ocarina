using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField]
    private float amplitude = 1.5f;

    [SerializeField]
    private float speed = 2f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        var targetPosition = initialPosition + new Vector3(Input.acceleration.x, Input.acceleration.y, 0) * amplitude;
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
