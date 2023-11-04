using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


namespace GCSeries.zView.Samples
{
    public class GBasicPresenterSample : MonoBehaviour
    {
        private GView _gView = null;
        private GView gView
        {
            get
            {
                if (_gView == null)
                    _gView = FindObjectOfType<GView>();
                return _gView;
            }
        }

        [SerializeField]
        Button noModeButton = null;

        [SerializeField]
        Button TwoDModeButton = null;

        [SerializeField]
        Button ThreeDModeButton = null;

        [SerializeField]
        Button ARModeButton = null;
        //////////////////////////////////////////////////////////////////
        // Unity Monobehaviour Callbacks
        //////////////////////////////////////////////////////////////////

        void Start()
        {
            // Perform any additional setup for the sample scene.
            this.SetUpScene();

            if (gView == null)
            {
                Debug.LogError("场景内缺少GView组件");
                return;
            }

            // 启动2D投屏
            this.SetConnection(gView.GetStandardMode());

            // 绑定UI事件
            if (noModeButton != null)
            {
                noModeButton.onClick.AddListener(() =>
                {
                    this.SetConnection(IntPtr.Zero);
                });
            }

            if (TwoDModeButton != null)
            {
                TwoDModeButton.onClick.AddListener(() =>
                {
                    this.SetConnection(gView.GetStandardMode());
                });
            }

            if (ThreeDModeButton != null)
            {
                ThreeDModeButton.onClick.AddListener(() =>
                {
                    this.SetConnection(gView.GetThreeDMode());
                });
            }

            if (ARModeButton != null)
            {
                ARModeButton.onClick.AddListener(() =>
                {
                    this.SetConnection(gView.GetAugmentedRealityMode());
                });
            }
        }

        void OnApplicationQuit()
        {
            // 在程序退出的时候如果结束录屏,该函数可以重复调用.
            if (gView != null)
                gView.FinishVideoRecording(IntPtr.Zero);
        }

        //////////////////////////////////////////////////////////////////
        // Private Methods
        //////////////////////////////////////////////////////////////////

        private void SetConnection(IntPtr mode)
        {
            // 如果没创建连接，先创建一个
            if (gView.GetCurrentActiveConnection() == IntPtr.Zero)
                gView.ConnectToDefaultViewer();

            if (mode == IntPtr.Zero)
            {
                // 设置为不投屏模式,直接关闭投屏
                gView.SetConnectionMode(gView.GetCurrentActiveConnection(), IntPtr.Zero);
                return;
            }

            // 先检查投屏模式的可用性
            if (this.IsModeAvailable(gView.GetCurrentActiveConnection(), mode))
            {
                // 可用性检查通过,启动投屏
                gView.SetConnectionMode(gView.GetCurrentActiveConnection(), mode);
            }
            else
            {
                Debug.LogError($"当前设备环境不支持 {mode} 投屏模式");
            }
        }

        private bool IsModeAvailable(IntPtr connection, IntPtr mode)
        {
            if (connection != IntPtr.Zero)
            {
                int numSupportedModes = gView.GetNumConnectionSupportedModes(connection);
                for (int i = 0; i < numSupportedModes; ++i)
                {
                    GView.SupportedMode supportedMode = gView.GetConnectionSupportedMode(connection, i);
                    if (supportedMode.Mode == mode)
                    {
                        return (supportedMode.ModeAvailability == GView.ModeAvailability.Available);
                    }
                }
            }

            return false;
        }

        private void SetUpScene()
        {
            // Cache references to objects in the scene.
            _cubeObject = GameObject.Find("Cube");
            _sphereObject = GameObject.Find("Sphere");
            _capsuleObject = GameObject.Find("Capsule");
            _leftPillarObject = GameObject.Find("LeftPillar");
            _rightPillarObject = GameObject.Find("RightPillar");

            // Update each scene object's color.
            this.SetGameObjectColor(_cubeObject, Color.white);
            this.SetGameObjectColor(_sphereObject, Color.red);
            this.SetGameObjectColor(_capsuleObject, Color.green);
            this.SetGameObjectColor(_leftPillarObject, Color.yellow);
            this.SetGameObjectColor(_rightPillarObject, Color.grey);
        }

        private void Update()
        {
            // Spin the cube to more easily see the effects of pausing an active 
            // connection's mode.
            if (_cubeObject != null)
            {
                // Rotate the cube 30 degrees per second about its local y-axis.
                Vector3 currentRotation = _cubeObject.transform.localRotation.eulerAngles;
                currentRotation.y += (30.0f * Time.deltaTime);

                _cubeObject.transform.localRotation = Quaternion.Euler(currentRotation);
            }
        }

        private void SetGameObjectColor(GameObject gameObject, Color color)
        {
            if (gameObject == null)
            {
                return;
            }

            Renderer renderer = gameObject.GetComponent<Renderer>();
            if (renderer == null)
            {
                return;
            }

            renderer.material = new Material(Shader.Find("Diffuse"));
            renderer.material.color = color;
        }


        //////////////////////////////////////////////////////////////////
        // Private Members
        //////////////////////////////////////////////////////////////////
        private GameObject _cubeObject = null;
        private GameObject _sphereObject = null;
        private GameObject _capsuleObject = null;
        private GameObject _leftPillarObject = null;
        private GameObject _rightPillarObject = null;
    }
}

