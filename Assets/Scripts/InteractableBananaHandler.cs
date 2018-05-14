using UnityEngine;
using VRStandardAssets.Utils;

public class InteractableBananaHandler : MonoBehaviour
{
    [SerializeField]
    VRInteractiveItem interactiveItem;

    [SerializeField]
    VRInput vrInput;


    public delegate void Func();

    public event Func OnVisible;
    public event Func OnInvisible;



    bool isGazeOver;

    AudioSource audioSource;
    MeshRenderer meshRenderer;
    MeshCollider meshCollider;


    void Awake()
    {
        _Initialize();
    }

    void OnEnable()
    {
        _Subscribe_Events();
    }

    void OnDisable()
    {
        _Unsubscribe_Events();
    }

    void _Initialize()
    {
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
    }

    void _Subscribe_Events()
    {
        vrInput.OnDown += HandleDown;
        vrInput.OnUp += HandleUp;

        interactiveItem.OnOver += HandleOver;
        interactiveItem.OnOut += HandleOut;
    }

    void _Unsubscribe_Events()
    {
        vrInput.OnDown -= HandleDown;
        vrInput.OnUp -= HandleUp;

        interactiveItem.OnOver -= HandleOver;
        interactiveItem.OnOut -= HandleOut;
    }

    void _FireEvent_OnVisible()
    {
        if (OnVisible != null)
        {
            OnVisible();
        }
    }

    void _FireEvent_OnInvisible()
    {
        if (OnInvisible != null)
        {
            OnInvisible();
        }
    }

    void HandleDown()
    {
        if (isGazeOver && meshRenderer.enabled)
        {
            audioSource.Play();
            Show(false);
        }
    }

    void HandleUp()
    {

    }

    void HandleOver()
    {
        isGazeOver = true;
    }

    void HandleOut()
    {
        isGazeOver = false;
    }

    public void Show(bool value)
    {
        meshRenderer.enabled = value;
        meshCollider.enabled = value;

        if (value)
        {
            _FireEvent_OnVisible();
        }
        else
        {
            _FireEvent_OnInvisible();
        }
    }
}