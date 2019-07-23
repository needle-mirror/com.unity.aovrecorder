using System;
using UnityEditor.Recorder.Input;
using UnityEngine;

namespace UnityEditor.Recorder
{
    [Serializable]
    class AOVImageInputSelector : InputSettingsSelector
    {
        [SerializeField] public CameraInputSettings cameraInputSettings = new AOVCameraInputSettings();
        public ImageInputSettings imageInputSettings
        {
            get { return (ImageInputSettings) selected; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                if (value is CameraInputSettings )
                {
                    selected = value;
                }
                else
                {
                    throw new ArgumentException("Video input type not supported: '" + value.GetType() + "'");
                }
            }
        }
    }
}
