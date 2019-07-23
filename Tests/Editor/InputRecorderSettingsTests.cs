using UnityEngine;
using NUnit.Framework;
using UnityEditor.Recorder.Input;
using UnityObject = UnityEngine.Object;

namespace UnityEditor.Recorder.Tests
{
	class AOVInputRecorderSettingsTests
	{
		[Test]
		public void AOVCameraInputSettings_ShouldHaveProperPublicAPI()
		{
			var input = new AOVCameraInputSettings
			{
				source = ImageSource.MainCamera,
				outputWidth = 640,
				outputHeight = 480,
				cameraTag = "AAA",
				allowTransparency = true,
				captureUI = true,
				flipFinalOutput = false
			};

			Assert.AreEqual(640, input.outputWidth);
			Assert.AreEqual(480, input.outputHeight);
			
			Assert.NotNull(input);
		}
	}
}
