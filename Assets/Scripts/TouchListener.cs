using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchListener : MonoBehaviour
{
    public float LongPressThreshold = 0.5f;
    public float SingleClickThreshold = 0.2f;

    public delegate void TouchHandler(Touch touch);

    public static event TouchHandler OnSingleClick;
    public static event TouchHandler OnDoubleClick;
    public static event TouchHandler OnLongPressing;

    private Touch _touch;
    private float _touchTimer = 0.0f;

    private IEnumerator CheckSingleClick()
    {
        float t = 0.0f;
        while (t < SingleClickThreshold)
        {
            t += Time.deltaTime;
            yield return null;
        }
        if (OnSingleClick != null)
            OnSingleClick.Invoke(_touch);
    }

    private void Start()
    {
#if UNITY_EDITOR
        print("Please Use Unity Remote (<color=#ff0000ff>https://docs.unity3d.com/Manual/UnityRemote5.html</color>) To Test the Touch Feature");
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            _touchTimer += Time.deltaTime;
            switch (_touch.phase)
            {
                case TouchPhase.Ended:
                    if (_touch.tapCount >= 2)
                    {
                        StopAllCoroutines();
                        if (OnDoubleClick != null)
                        {
                            OnDoubleClick.Invoke(_touch);
                            Debug.Log("Working");
                        }
                    }
                    else if (_touchTimer < LongPressThreshold)
                    {
                        StartCoroutine(CheckSingleClick());
                    }

                    break;
                case TouchPhase.Moved:
                    if (_touchTimer > LongPressThreshold)
                    {
                        if (OnLongPressing != null)
                            OnLongPressing.Invoke(_touch);
                    }
                    break;
                case TouchPhase.Stationary:
                    if (_touchTimer > LongPressThreshold)
                    {
                        if (OnLongPressing != null)
                            OnLongPressing.Invoke(_touch);
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            _touchTimer = 0;
        }
    }
}