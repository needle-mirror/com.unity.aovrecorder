using System;


namespace UnityEditor.Recorder.Input
{
    public class AOVCameraInputSettings : CameraInputSettings
    {
        internal override Type inputType
        {
            #if HDRP_AOVREQUEST_API
            get { return typeof(AOVCameraAOVRequestAPIInput);}
            #else
            get { return typeof(AOVCameraDebugFrameworkInput); }
            #endif
        }

    }
}
