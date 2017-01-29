using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using LibGit2Sharp;

// Proof-of-concept for C# interface for managing a Git repo
public class GitRepository
{
    public struct WorkingFile
    {
        public enum WorkingStatus
        {
            SYNCHED,    // This file is up-to-date.
            PREPPED,    // This file is staged to commit.
            PENDING,    // This file has pending changes to stage for commit.
            REMOVED,    // This file has been removed from the working directory.
            RENAMED,    // This file has been renamed.
            NOTRACK,    // This file is not tracked.
        }

        public WorkingStatus status;    // Revision status.
        public string filePath;         // Absolute file path.
    }

    private string gitRepoPath = @"C:\Users\terryn\Documents\Visual Studio 2015\Projects\UnityVCSIntegrationTest\.git";

    public GitRepository(string repositoryPath)
    {
        
    }

    public IEnumerable<WorkingFile> GetStatus()
    {
        List<WorkingFile> entries = new List<WorkingFile>();

        using (var gitRepo = new Repository(gitRepoPath))
        {
            IEnumerable<StatusEntry> repoStatus = gitRepo.Index.RetrieveStatus();
            var flags = FileStatus.Modified | FileStatus.Removed | FileStatus.Added | FileStatus.Untracked;
            var notIg = from ent in repoStatus
                        where (ent.State & flags) != 0
                        select ent;

            foreach (var entry in notIg)
            {
                WorkingFile tempFile = new WorkingFile();

                tempFile.filePath = entry.FilePath;
                switch (entry.State)
                {
                    case FileStatus.Untracked:
                        tempFile.status = WorkingFile.WorkingStatus.NOTRACK;
                        break;
                    default:
                        continue;
                        break;
                }

                entries.Add(tempFile);
            }
        }

        return entries;
    }
}

public class GitWindow : EditorWindow
{
    GitRepository repo;

    bool showStatus;

    float refreshInterval;      // time interval between status refreshes (seconds)
    float refreshTimer;         // timer counting down to next refresh (seconds)

    void OnEnable()
    {
        repo = new GitRepository(Application.dataPath);

        refreshTimer = refreshInterval;
    }

    void OnGUI()
    {
        showStatus = EditorGUILayout.Foldout(showStatus, "Status");
        if(showStatus)
        {
            //EditorGUILayout.LabelField();
        }

        // WD/Index Toolbar
        EditorGUILayout.BeginHorizontal();

        if(GUILayout.Button("status"))
        {
            foreach(var entry in repo.GetStatus())
            {
                Debug.Log(entry.status + " - " + entry.filePath);
            }
        }

        EditorGUILayout.EndHorizontal();
    }

    [MenuItem("Window/Guity")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(GitWindow));
    }
}