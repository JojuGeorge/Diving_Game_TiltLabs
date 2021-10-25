using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class WaterMechanics : MonoBehaviour
{

    [SerializeField] private GameObject _boundingBoxGO, _cameraGO;
    [SerializeField] private Volume _postProcessingVolume;
    [SerializeField] private Color _underWaterColor;

    private bool _underWater = false;
    private Vignette _vignette;
    private DepthOfField _depthOfField;
    private ColorAdjustments _colorAdjustments;


    private void Start()
    {
        _postProcessingVolume.profile.TryGet(out _vignette);
        _postProcessingVolume.profile.TryGet(out _depthOfField);
        _postProcessingVolume.profile.TryGet(out _colorAdjustments);
    }

    private void FixedUpdate()
    {
        if (_boundingBoxGO.GetComponent<BoxCollider>().bounds.Contains(_cameraGO.transform.position))
            _underWater = true;
        else
            _underWater = false;

        if (_underWater)
        {
            _vignette.intensity.value = 0.32f;
            _depthOfField.focusDistance.value = 3f;
            _colorAdjustments.colorFilter.value = _underWaterColor ;
        }
        else {
            _vignette.intensity.value = 0.292f;
            _depthOfField.focusDistance.value = 5f;
            _colorAdjustments.colorFilter.value = Color.white;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
           // Debug.Log("(In WaterMechanics.cs )Player dived into the water");
            other.GetComponent<Player>().inWater = true;
            _underWater = true;
        }
    }
}
