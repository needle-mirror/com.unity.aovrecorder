using System;
using System.Collections.Generic;
using UnityEditor.Recorder.Input;
using UnityEngine;


namespace UnityEditor.Recorder
{
    public enum AOVRecorderOutputFormat
    {
        PNG,
        JPEG,
        EXR     
    }

    // First entries (up to Alpha) of AOVGType must be kept in sync with
    // HDRP Runtime/Debug/MaterialDebug.cs MaterialShaderProperty
    public enum AOVGType
    {
        Beauty,
        Albedo,
        Normal,
        Smoothness,
        AmbientOcclusion,
        Metal,
        Specular,
        Alpha,
         // Additional option for the AOV Recorder
        Depth

    }

    public enum AOVCompressionType
    {
        none,
        rle,
        zip,
        zips,
        piz,
        pxr24,
        b44,
        b44a,
        dwaa,
        dwab
    }
     
    [RecorderSettings(typeof(AOVRecorder), "AOV Image Sequence", "aovimagesequence_16")]
    public class AOVRecorderSettings : RecorderSettings
    {
        public AOVRecorderOutputFormat outputFormat = AOVRecorderOutputFormat.EXR;
        [SerializeField] internal AOVGType AOVGSelection = AOVGType.Beauty;
        [SerializeField] internal AOVCompressionType AOVCompression = AOVCompressionType.dwaa;
        [SerializeField] internal AOVImageInputSelector m_AOVImageInputSelector = new AOVImageInputSelector();
 
        public AOVRecorderSettings()
        {
            fileNameGenerator.fileName = "aov_image_" + DefaultWildcard.Frame;
        }

        public override string extension
        {
            get
            {
                switch (outputFormat)
                {
                    case AOVRecorderOutputFormat.PNG:
                        return "png";
                    case AOVRecorderOutputFormat.JPEG:
                        return "jpg";
                    case AOVRecorderOutputFormat.EXR:
                        return "exr";
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public ImageInputSettings imageInputSettings
        {
            get { return m_AOVImageInputSelector.imageInputSettings; }
            set { m_AOVImageInputSelector.imageInputSettings = value; }
        }

        internal override bool ValidityCheck(List<string> errors)
        {
            var ok = base.ValidityCheck(errors);

            if(string.IsNullOrEmpty(fileNameGenerator.fileName))
            {
                ok = false;
                errors.Add("missing file name");
            }

            #if !HDRP_AVAILABLE
            ok = false;
            errors.Add("HDRP package not available");
            #endif

            return ok;
        }

        internal override  bool HasErrors()
        {
            #if HDRP_AVAILABLE
            
            return false;
            #else
            #if UNITY_2019_2_OR_NEWER
            var hdrpMinVersion = 6.6;
            #else
            var hdrpMinVersion = 5.11;
            #endif

            enabled = false;
            Debug.LogError("AOV Recorder requires the HDRP package version " + hdrpMinVersion + " or greater to be installed");
            return true;
            #endif
        }

        public override IEnumerable<RecorderInputSettings> inputsSettings
        {
            get { yield return m_AOVImageInputSelector.selected; }
        } 
    }
}
