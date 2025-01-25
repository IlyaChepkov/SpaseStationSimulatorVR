using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;


public class RotateObjectController : MonoBehaviour
{
    public enum Axis
    {
        x,
        z
    }

    [SerializeField] private Transform _leverRoot;
    [SerializeField] private Axis _localAxis;
    [SerializeField] private float _startValue;
    [SerializeField] private Vector2 _constraints;

    [SerializeField] private float posConstrains;

    IXRInteractable _interactable;
    IXRInteractor _interactor;
    Vector3? startMove;
    private Quaternion startRoration;
    float nowPosition = 0;
    int counter;
    bool rotate = true;

    private void Start()
    {
        startRoration = transform.rotation;
    }

    private void Update()
    {
        if (_interactable is not null && _interactor is not null)
        { 
            if (startMove is null)
                startMove = _interactor.transform.TransformPoint(_interactor.transform.parent.position);
            MoveLever((_interactor.transform.position - startMove) ?? throw new System.Exception());
            startMove = _interactor.transform.TransformPoint(_interactor.transform.parent.position);
        }
    }

    public void SelectStart(SelectEnterEventArgs args)
    {
        Debug.Log("SelectStart");
        _interactable = args.interactableObject;
        _interactor = args.interactorObject;
    }

    public void SelectEnd()
    {
        Debug.Log("SelectEnd");
        _interactable = null;
        _interactor = null;
        startMove = null;
    }

    public void MoveLever(Vector3 worldPosition)
    {
        float value = worldPosition.x / posConstrains;
        if (value < 0) value = 0;
        if (value > 1) value = 1;
        Debug.Log(_constraints.x * value - nowPosition);
        transform.Rotate(_constraints.x * value - _constraints.x * nowPosition, 0, 0, Space.World);
        nowPosition = value;
    }
}

