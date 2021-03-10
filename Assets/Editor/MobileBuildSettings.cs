using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

public class Nexus5BuildSettings
{
	const string CompanyName = "AltImageGames";

	[MenuItem("File/Set Nexus 5")]
	public static void SetNexus5Settings()
	{
		//remote
		EditorSettings.unityRemoteDevice = "Any Android Device";

		//resolution
		Screen.SetResolution(1080,1920,true);
		PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;

		//android api level 23 (min, target)
		PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel23;
		PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel23;

		//package build settings
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android,BuildTarget.Android);
		PlayerSettings.companyName = CompanyName;
		PlayerSettings.productName = System.IO.Directory.GetParent(Application.dataPath).Name;
		PlayerSettings.applicationIdentifier = "com."+PlayerSettings.companyName+"."+PlayerSettings.productName;

		//reduces file size
		PlayerSettings.Android.androidIsGame = false;
		PlayerSettings.Android.androidTVCompatibility = false;
		PlayerSettings.Android.targetDevice = AndroidTargetDevice.ARMv7;
	}

	[MenuItem("File/Set LG G7")]
	public static void SetLGG7Settings()
	{
		//remote
		EditorSettings.unityRemoteDevice = "Any Android Device";

		//resolution
		Screen.SetResolution(1440,3120,true);
		PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;

		//android api level 23 (min, target)
		PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel23;
		PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel23;

		//package build settings
		EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android,BuildTarget.Android);
		PlayerSettings.companyName = CompanyName;
		PlayerSettings.productName = System.IO.Directory.GetParent(Application.dataPath).Name;
		PlayerSettings.applicationIdentifier = "com."+PlayerSettings.companyName+"."+PlayerSettings.productName;

		//reduces file size
		PlayerSettings.Android.androidIsGame = false;
		PlayerSettings.Android.androidTVCompatibility = false;
		PlayerSettings.Android.targetDevice = AndroidTargetDevice.ARMv7;
	}
}
#endif