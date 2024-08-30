using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoAnimation : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _animationSpeed;

    private void Start()
    {
        ClockVise();
    }
    void ClockVise()
    {
        _image.fillClockwise = true;
        _image.DOFillAmount(1,_animationSpeed).OnComplete(CounterClockVise);
    }
    void CounterClockVise()
    {
        _image.fillClockwise = false;
        _image.DOFillAmount(0, _animationSpeed).OnComplete(ClockVise);
        
    }
    private void OnDestroy()
    {
        _image.DOKill();
    }

}
