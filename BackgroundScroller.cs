using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform _background1 = null;
    [SerializeField] private Transform _background2 = null;

    private bool    _swap           = true;
    private float   _currentWidth   = 20;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentWidth < _camera.position.x)
        {
            if (_swap)
            {
                _background1.localPosition = new Vector3(_background1.localPosition.x + 40, 0, 10);
            }
            else
            {
                _background2.localPosition = new Vector3(_background2.localPosition.x + 40, 0, 10);

            }
            _currentWidth += 20;
            _swap = !_swap;
        }

        if(_currentWidth > _camera.position.x + 20)
        {
            if (_swap)
            {
                _background2.localPosition = new Vector3(_background2.localPosition.x - 40, 0, 10);
            }
            else
            {
                _background1.localPosition = new Vector3(_background1.localPosition.x - 40, 0, 10);

            }
            _currentWidth -= 20;
            _swap = !_swap;
        }
    }
}
