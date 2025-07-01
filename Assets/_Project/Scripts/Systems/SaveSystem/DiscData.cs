using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DiscData
{
    [SerializeField] private float _capacity;
    [SerializeField] private float _usedSpace;
    [SerializeField] private List<GameFileData> _files; 

    public float Capacity { get => _capacity; set => _capacity = value; }
    public float UsedSpace { get => _usedSpace; set => _usedSpace = value; }
    public List<GameFileData> Files { get => _files; set => _files = value; }
}
