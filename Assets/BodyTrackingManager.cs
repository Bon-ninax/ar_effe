using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class BodyTrackingManager : MonoBehaviour
{
    [SerializeField] private ARHumanBodyManager humanBodyManager;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        //humanBodyManager.humanBodiesChanged += onDisable;
    }

    private void OnDisable()
    {
        //faceManager.facesChanged -= OnFaceChanged;
    }




    
}

