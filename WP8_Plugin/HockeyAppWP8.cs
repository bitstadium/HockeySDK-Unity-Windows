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

	void Awake() {
		DontDestroyOnLoad(gameObject);
		#if (UNITY_WP8 && !UNITY_EDITOR)
		StartCoroutine(HockeyStart());
		#endif
	}

	IEnumerator HockeyStart() {
		HockeyAppUnity.HockeyClientUnity.Current.Configure (appID, hockeyServer);
		yield return new object ();
		HockeyAppUnity.HockeyClientUnity.Current.SendCrashes (autoUpload);
		yield return new object();
		if (updateOnStart) {
			HockeyAppUnity.HockeyClientUnity.Current.CheckForUpdates ();
			yield return new object();
		}
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
		HockeyAppUnity.HockeyClientUnity.Current.HandleUnityLogException(logString, stackTrace);
		if(LogType.Assert != type && LogType.Exception != type)	
		{	
			return;	
		}	
		HockeyAppUnity.HockeyClientUnity.Current.HandleUnityLogException(logString, stackTrace);
		#endif
	}
}
