The official SDK for the HockeyApp service for Unity apps on Windows.

## Introduction

The HockeySDK for Windows allows users to send crash reports right from within the application.
When your app crashes, a file with basic information about the environment (device type, OS version, etc.), the reason and the stacktrace of the exception is created. 
The next time the user starts the app, he is asked to send the crash data to the developer. If he confirms the dialog, the crash log is sent to HockeyApp and then the file deleted from the device.

## Supported Platforms
As of now we support Unity WP8 builds, but we plan to add support for other Windows platforms soon.

## Features & Installation

### Windows Phone 8

1. Close your Unity-Project
2. Copy the contents of the HockeyAppUnityWindowsAssets.zip file to your Assets directory.
3. ReOpen your Project in Unity Editor
4. Create a new empty GameObject
5. Add a ScriptComponent to the GameObject. Choose HockeyAppWP8Script
6. in the component view add your HockeyApp AppID and tick the relevant boxes.
7. If you are building xour app with Unity 5, you need to do some additional [adjustments (Unity 5)](#Unity 5)
8. Build your project for Windows Phone 8 in the Unity Editor
9. In the created Solution check the box for ID_CAP_WEBBROWSERCOMPONENT in the WMAppManifest.xml under capabilities (needed to display releasenotes for app updates)

In the ExampleProject folder you can see an example project.

### <a name="Unity 5">Unity 5</a>
If you are using Unity 5, you need to do some adjustments in order to be able to build the project:

1. Select the **Assets/Plugins/HockeyAppUnityWP8.dll** inside the project window
2. In the inspector pane, only check the **WSA** platform and apply those changes
3. Select the **Assets/Plugins/WP8/HockeyAppUnityWP8.dll** inside the project window
4. In the inspector pane, only check the **WS8** platform
5. Check **Don't process** and in the Placeholder drop down, navigate to **Assets/Plugins/HockeyAppUnityWP8.dll**.

## License

The Hockey SDK is provided under the following license:

    The MIT License
    Copyright (c) 2014 HockeyApp, Bit Stadium GmbH.
    All rights reserved.
	
    Permission is hereby granted, free of charge, to any person
    obtaining a copy of this software and associated documentation
    files (the "Software"), to deal in the Software without
    restriction, including without limitation the rights to use,
    copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the
    Software is furnished to do so, subject to the following
    conditions:

    The above copyright notice and this permission notice shall be
    included in all copies or substantial portions of the Software.
	
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
    EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
    OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
    NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
    HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
    WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
    FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
    OTHER DEALINGS IN THE SOFTWARE.

## Support
This is currently an alpha version. Please report any bugs/feature requests as Github tickets. Thanks!

If you have any questions, problems or suggestions, please contact us at [support@hockeyapp.net](mailto:support@hockeyapp.net).

## Code of Conduct

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

## Contributor License

You must sign a [Contributor License Agreement](https://cla.microsoft.com/) before submitting your pull request. To complete the Contributor License Agreement (CLA), you will need to submit a request via the [form](https://cla.microsoft.com/) and then electronically sign the CLA when you receive the email containing the link to the document. You need to sign the CLA only once to cover submission to any Microsoft OSS project. 

## Release Notes
Can be found in the corresponding subdirectories
