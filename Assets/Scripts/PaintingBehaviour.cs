using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class PaintingBehaviour : MonoBehaviour
{
    private Rigidbody _rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable _grab;

    private Vector3 _startPosition;
    private Quaternion _startRotation;
    [SerializeField] private GameObject button;
    void Awake(){
    button.SetActive(false);
    _rb = GetComponent<Rigidbody>();
    _grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();


    _rb.constraints = RigidbodyConstraints.FreezeAll;

    _startPosition = transform.position;
    _startRotation = transform.rotation;

    _grab.selectEntered.AddListener(OnPickedUp);
    }

    private void OnPickedUp(SelectEnterEventArgs args){
        _rb.constraints = RigidbodyConstraints.None;
        button.SetActive(true);
    }
    void OnDestroy(){
        _grab.selectEntered.RemoveListener(OnPickedUp);
    }
}