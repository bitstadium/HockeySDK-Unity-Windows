mkdir .\Release\Assets\Plugins\WP8\HockeyAppContent
mkdir .\Release\Assets\Plugins\WP8\HockeyAppLib
mkdir .\Release\Assets\Editor

copy /y .\WP8_Plugin\HockeyAppUnityWP8Fake\Bin\Release\HockeyAppUnityWP8.dll .\Release\Assets\Plugins\ 
copy /y .\WP8_Plugin\HockeyAppUnityWP8Fake\Bin\Release\HockeyAppUnityWP8.pdb ..\Release\Assets\Plugins\

copy /y .\WP8_Plugin\HockeyAppUnity\Bin\Release\HockeyAppUnityWP8.dll .\Release\Assets\Plugins\wp8\
copy /y .\WP8_Plugin\HockeyAppUnity\Bin\Release\HockeyAppUnityWP8.pdb ..\Release\Assets\Plugins\wp8\

copy /y .\WP8_Plugin\HockeyAppUnity\Bin\Release\HockeyAppPCL.dll .\Release\Assets\Plugins\wp8\
copy /y .\WP8_Plugin\HockeyAppUnity\Bin\Release\HockeyAppPCL.pdb .\Release\Assets\Plugins\wp8\

copy /y .\WP8_Plugin\HockeyAppWP8PostBuildTrigger.cs .\Release\Assets\Editor\
copy /y .\WP8_Plugin\HockeyAppWP8.cs .\Release\Assets\Plugins\HockeyAppUnity-Scripts\
copy /y .\WP8_Plugin\VersionWP8.txt .\Release\Assets\

REM HockeySDK-Windows should be checked out in parallel directory
copy /y ..\HockeySDK-Windows\bin\wp8\Release\HockeyApp.dll .\Release\Assets\Plugins\wp8\HockeyAppLib\HockeyApp.dll.bak
copy /y ..\HockeySDK-Windows\bin\wp8\Release\HockeyApp.pdb .\Release\Assets\Plugins\wp8\HockeyAppLib\HockeyApp.pdb.bak

copy /y ..\HockeySDK-Windows\Nuget\content\wp8\HockeyAppContent\*.png .\Release\Assets\Plugins\wp8\HockeyAppContent\

del .\HockeyAppUnityWindowsAssets.zip 2>NUL
"C:\Program Files\7-Zip\7z.exe" a -tzip .\HockeyAppUnityWindowsAssets.zip .\Release\Assets