using System.Threading.Tasks;
using UnityEngine;

public class SmoothRotation : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 rotationMovement;
    [SerializeField] private int durationMilliseconds;
    [SerializeField] private int postDurationMilliseconds;

    private bool _rotating;
    private float _timeSpent;
    private float _durationInSeconds;
    
    private Quaternion _initialRotation;
    private Quaternion _finalRotation;

    private void Update()
    {
        if (!_rotating)
            return;
        
        _timeSpent += Time.deltaTime;
        
        target.rotation = Quaternion.Lerp(
            _initialRotation, 
            _finalRotation, 
            _timeSpent / _durationInSeconds);
    }

    public async Task Rotate()
    {
        _durationInSeconds = durationMilliseconds / 1000f;
        _timeSpent = 0;
        
        _initialRotation = target.rotation;
        _finalRotation = _initialRotation * Quaternion.Euler(rotationMovement);

        _rotating = true;
        await Task.Delay(durationMilliseconds);
        _rotating = false;
        await Task.Delay(postDurationMilliseconds);
    }
}
