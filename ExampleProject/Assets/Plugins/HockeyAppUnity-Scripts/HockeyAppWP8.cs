/*******************************************************************************
 *
 * Author: Andreas Moosbrugger
 * 
 * Copyright (c) 2014 HockeyApp, Bit Stadium GmbH.
 * All rights reserved.
 * 
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 * 
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 * 
 ******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.InteropServices;

public class HockeyAppWP8 : MonoBehaviour {
	public string appID = "your-hockey-app-id";
	public string hockeyServer = "https://rink.hockeyapp.net/";
	public bool autoUpload = false;
	public bool updateOnStart = false;
	public bool updateOnResume = false;

	void Awake() {
		DontDestroyOnLoad(gameObject);
		#if (UNITY_WP8 && !UNITY_EDITOR)
		StartCoroutine(HockeyStart());
		#endif
	}

	IEnumerator HockeyStart() {
		#if (UNITY_WP8 && !UNITY_EDITOR)
		HockeyAppUnity.HockeyClientUnity.Current.Configure (appID, hockeyServer);
		yield return new object();
		var task = HockeyAppUnity.HockeyClientUnity.Current.SendCrashesAsync (autoUpload);
		while (!task.IsCompleted) yield return new object();
		if (updateOnStart) {
			HockeyAppUnity.HockeyClientUnity.Current.CheckForUpdates ();
			yield return new object();
		}
		#endif
		yield return new object();
	}

	private bool wasPaused = false;
	void OnApplicationPause(bool pauseStatus) {
		#if (UNITY_WP8 && !UNITY_EDITOR)
		if(updateOnResume && !pauseStatus && wasPaused) {
			wasPaused = false;
			HockeyAppUnity.HockeyClientUnity.Current.CheckForUpdates (); } 
		if(pauseStatus) {
			wasPaused = true;
		}
		#endif
}

void OnEnable() {
	#if (UNITY_WP8 && !UNITY_EDITOR)
	Application.RegisterLogCallback(OnHandleLogCallback);
	#endif
}

void OnDisable() {
	Application.RegisterLogCallback(null);
}

void OnDestroy() {
	Application.RegisterLogCallback(null);
}

/// <summary>
/// Callback for handling log messages.
/// </summary>
/// <param name="logString">A string that contains the reason for the exception.</param>
/// <param name="stackTrace">The stacktrace for the exception.</param>
/// <param name="type">The type of the log message.</param>
	public void OnHandleLogCallback(string logString, string stackTrace, LogType type) {
	#if (UNITY_WP8 && !UNITY_EDITOR)
		if(LogType.Assert != type && LogType.Exception != type  && LogType.Error != type )	
		{	
			return;	
		}	
		HockeyAppUnity.HockeyClientUnity.Current.HandleUnityLogException(logString, stackTrace);
	#endif
	}
}
