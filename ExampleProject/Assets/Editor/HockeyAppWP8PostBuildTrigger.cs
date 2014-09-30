using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

public static class PostBuildTrigger
{
	// Name of folder in project directory containing files for build
	private static char dirSep = Path.DirectorySeparatorChar;
	private static string WP8PluginPath = "Plugins" + dirSep + "WP8" + dirSep ;
	private static string HockeyAppContentFolderName = "HockeyAppContent";
	private static string HockeyAppLibFolderName = "HockeyAppLib";
	private static string HockeyAppStartMarker = "HOCKEY_APP_MARKER_START";
	private static string HockeyAppEndMarker = "HOCKEY_APP_MARKER_END";
	private static string rn = "\r\n";
	private static string HockeyAppProjectContentLineTemplate = " <Content Include=\"HockeyAppContent\\{0}\" />" + rn;

	private static string HockeyAppProjectContentTemplate = "<!-- " + HockeyAppStartMarker + " // PLEASE DO NOT CHANGE  -->" + rn +
		"<ItemGroup>" + rn + 
		"<Reference Include=\"HockeyApp\">" + rn +
		"    <HintPath>HockeyApp.dll</HintPath>" + rn +
		"</Reference>" + rn +
		"{0}" + 
		"</ItemGroup>" + rn +
		"<!-- " + HockeyAppEndMarker + " // PLEASE DO NOT CHANGE  -->";

	private static string HockeyAppCodeTemplate = rn + "// "+ HockeyAppStartMarker + " -- PLEASE DO NOT CHANGE" + rn +
		"public partial class App" + rn + 
		"{" + rn +
		"	static App()" + rn +
        "   { // wire up dispatcher for plugin" + rn +
		"	if (typeof(HockeyApp.HockeyClientWP8SLExtension) != null) // ensures that HockeyApp.dll gets loaded " + rn + "{" +
		"    HockeyAppUnity.Dispatcher.InvokeOnAppThread = (callback) => UnityPlayer.UnityApp.BeginInvoke(() => callback());" + rn +
        "    HockeyAppUnity.Dispatcher.InvokeOnUIThread = (callback) => System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => callback());" + rn +
		"    }" + rn + "    }" + rn + "}" + rn +
		"// " + HockeyAppEndMarker + " -- PLEASE DO NOT CHANGE" + rn;


	[PostProcessBuild] // <- this is where the magic happens
	public static void OnPostProcessBuild(BuildTarget target, string path)
	{
		if (target.Equals (BuildTarget.WP8Player)) {
			//locate app.xaml.cs
			var targetDir = new DirectoryInfo(path);
			FileInfo appFile = GetUniqueFileByPattern(targetDir, "App.xaml.cs", "App.xaml.cs" );
			if(appFile == null) { return; }
			//locate csproj File
			DirectoryInfo projectDir = appFile.Directory;
			FileInfo projectFile = GetUniqueFileByPattern(targetDir, "*.csproj", "Project" );
			if(projectFile == null) { return; }
			var appAssetsDir = new DirectoryInfo(Application.dataPath);
			var contentPath = Path.Combine(appAssetsDir.FullName, WP8PluginPath + HockeyAppContentFolderName);
			if(!Directory.Exists(contentPath)) {
				Debug.LogWarning("Content directory does not exist: " + contentPath); 
			} else {
				var originalContentDir = new DirectoryInfo(contentPath);
				var originalLibDir = new DirectoryInfo(Path.Combine(appAssetsDir.FullName, WP8PluginPath + HockeyAppLibFolderName));
				var projectContentDir = new DirectoryInfo(Path.Combine(projectDir.FullName, HockeyAppContentFolderName));
				CopyAll(originalContentDir, projectContentDir, "*.png"); //copy HockeyAppContent to Project
				CopyAll(originalLibDir, projectDir, "*.bak");
				MakeChangesToProjectFile(projectFile, originalContentDir.GetFiles("*.png"));
			}
			MakeChangesToAppXamlCs(appFile);

	    }
	}

	private static FileInfo GetUniqueFileByPattern(DirectoryInfo directory, string filePattern, string fileInfo) {
		var searchFileList = directory.GetFiles(filePattern, SearchOption.AllDirectories);
		if (searchFileList.Length == 0) {
			Debug.LogError(fileInfo + " file not found !!");
		} 
		if(searchFileList.Length > 1) {
			Debug.LogWarning("Multiple " + fileInfo +  " files found");
		}
		return searchFileList [0];
	}

	private static void MakeChangesToProjectFile(FileInfo projectFile, FileInfo[] contentFiles) {
		var results = new List<string>();
		var contents = new List<string>(File.ReadAllLines(projectFile.FullName));
		bool isInHockeyAppArea = false;
		bool hockeyAppAreaFound = false;
		int counter = 0;
		int lastItemGroupLine = 0;
		foreach (string line in contents) {
			counter++;
			if(line.Contains(HockeyAppStartMarker)) {
				hockeyAppAreaFound = true;
				isInHockeyAppArea = true;
			}
			if(isInHockeyAppArea) { 
				if(line.Contains(HockeyAppEndMarker)) { 
					isInHockeyAppArea = false; 
					results.Add(GetItemGroupString(contentFiles));
				}
			} else {
				results.Add(line); 
				if(line.Contains("</ItemGroup>")) { lastItemGroupLine = counter; }
			}
		}
		if (!hockeyAppAreaFound) {
			results.Insert(lastItemGroupLine, GetItemGroupString(contentFiles));
		}
		File.WriteAllLines (projectFile.FullName, results.ToArray());
	}

	private static string GetItemGroupString(FileInfo[] files) {
		var builder = new StringBuilder ();
		foreach (var file in files) {
			builder.AppendFormat(HockeyAppProjectContentLineTemplate, file.Name);
		}
		return string.Format (HockeyAppProjectContentTemplate, builder.ToString ());
	}

	private static void MakeChangesToAppXamlCs(FileInfo appXamlCs) {
		var contents = File.ReadAllText (appXamlCs.FullName);
		if(!contents.Contains(HockeyAppEndMarker)){
			contents = contents.Insert(contents.LastIndexOf("}"), HockeyAppCodeTemplate);
		}
		File.WriteAllText (appXamlCs.FullName, contents);
	}

	private static int CopyAll(DirectoryInfo source, DirectoryInfo target, string pattern = "*.*")
	{
		int filecount = 0;
		if (Directory.Exists(target.FullName) == false)
		{
			Directory.CreateDirectory(target.FullName);
		}
		foreach (FileInfo fi in source.GetFiles(pattern))
		{
			filecount++;
			fi.CopyTo(Path.Combine(target.ToString(), fi.Name.Replace(".bak","")), true);
		}
		foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
		{
			DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
			filecount += CopyAll(diSourceSubDir, nextTargetSubDir);
		}
		return filecount;
	}
}