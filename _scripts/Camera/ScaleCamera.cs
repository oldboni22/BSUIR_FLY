using UnityEngine;
using DG.Tweening;

public class ScaleCamera : MonoBehaviour
{
    [SerializeField] private BoxCollider2D[] _bounds;
    [SerializeField] private Camera _camera;
    private void Awake()
    {
        var bounds = new Bounds();
        foreach (var t in _bounds)
            bounds.Encapsulate(t.bounds);

        var vert = bounds.size.y;
        var horizontal = bounds.size.x * Screen.height / Screen.width;

        _camera.orthographicSize = Mathf.Max(horizontal, vert) * .5f;

    }
}
