using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TarnishedTool.Models;

namespace TarnishedTool.Services;

public class CustomRowNamesService
{
    private readonly string _filePath;
    private Dictionary<string, Dictionary<uint, string>> _customNames;

    public CustomRowNamesService()
    {
        _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "TarnishedTool",
            "CustomRowNamesPath.json");
        
        _customNames = new Dictionary<string, Dictionary<uint, string>>();
    }

    public void Load()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            var loaded = JsonSerializer.Deserialize<Dictionary<string, Dictionary<uint, string>>>(json);
            if (loaded != null)
            {
                _customNames = loaded;
            }
        }

    }

    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
        var json = JsonSerializer.Serialize(_customNames);
        File.WriteAllText(_filePath, json);
    }

    public string GetCustomRowNames(string paramName, uint rowId)
    {
        if (_customNames.TryGetValue(paramName, out var paramDict))
        {
            if (paramDict.TryGetValue(rowId, out var customRowNames))
            {
                return customRowNames;
            }
        }
        return null;
    }

    public void SetCustomRowNames(string paramName, uint rowId, string newName)
    {
        if (!_customNames.TryGetValue(paramName, out var paramDict)) 
        {
            paramDict = new Dictionary<uint, string>();
            _customNames[paramName] = paramDict;
        }
        paramDict[rowId] = newName;
    } 

    public bool HasCustomRowNames(string paramName, uint rowId) 
    {
        if (_customNames.TryGetValue(paramName, out var paramDict))
        {
            if (paramDict.ContainsKey(rowId))
            {
                return true;
            }
        }
        return false;
    }
}