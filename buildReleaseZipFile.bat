mkdir .\Release\UnityAssets\Plugins\WP8\HockeyAppContent
mkdir .\Release\UnityAssets\Plugins\WP8\HockeyAppLib
mkdir .\Release\UnityAssets\Editor

copy .\WP8_Plugin\HockeyAppUnityFake\Bin\Release\HockeyAppUnityWP8.dll .\Release\UnityAssets\Plugins\ /Y
copy .\WP8_Plugin\HockeyAppUnityFake\Bin\Release\HockeyAppUnityWP8.pdb ..\Release\UnityAssets\Plugins\ /Y

copy .\WP8_Plugin\HockeyAppUnity\Bin\Release\HockeyAppUnityWP8.dll .\Release\UnityAssets\Plugins\wp8\ /Y
copy .\WP8_Plugin\HockeyAppUnity\Bin\Release\HockeyAppUnityWP8.pdb ..\Release\UnityAssets\Plugins\wp8\ /Y

copy .\WP8_Plugin\HockeyAppUnity\Bin\Release\HockeyAppPCL.dll .\Release\UnityAssets\Plugins\wp8\ /Y
copy .\WP8_Plugin\HockeyAppUnity\Bin\Release\HockeyAppPCL.pdb .\Release\UnityAssets\Plugins\wp8\ /Y

copy .\WP8_Plugin\HockeyAppWP8PostBuildTrigger.cs .\Release\UnityAssets\Editor\ /Y
copy .\WP8_Plugin\HockeyAppWP8Script.cs .\Release\UnityAssets\Plugins\HockeyAppUnity-Scripts\ /Y
copy .\WP8_Plugin\VersionWP8.txt .\Release\UnityAssets\VersionWP8.txt /Y

REM HockeySDK-Windows should be checked out in parallel directory
copy ..\HockeySDK-Windows\bin\wp8\Release\HockeyApp.dll .\Release\UnityAssets\Plugins\wp8\HockeyAppLib\HockeyApp.dll.bak /Y
copy ..\HockeySDK-Windows\bin\wp8\Release\HockeyApp.pdb .\Release\UnityAssets\Plugins\wp8\HockeyAppLib\HockeyApp.pdb.bak /Y

copy ..\HockeySDK-Windows\Nuget\content\wp8\HockeyAppContent\*.png .\Release\UnityAssets\Plugins\wp8\HockeyAppContent\ /Y

del .\HockeyAppUnityWindowsAssets.zip 2>NUL
"C:\Program Files\7-Zip\7z.exe" a -tzip .\HockeyAppUnityWindowsAssets.zip .\Release\Assets