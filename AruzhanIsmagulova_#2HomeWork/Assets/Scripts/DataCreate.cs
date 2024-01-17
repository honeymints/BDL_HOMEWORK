using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Character/CharacterCreate")]
public class CreateData : ScriptableObject
{
    private SomeData _someData = new SomeData();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class SomeData
{
    private string Name;
    private Vector3 scale;
    private Vector3 position;
    
}
