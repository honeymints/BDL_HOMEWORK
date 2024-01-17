using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName="Cube/CreateCube")]
public class DataCube : ScriptableObject
{
   public Color NewColor;
   public Vector3 Scale;
   public float PositionX;
   public Vector3 Position;

   //public List<GameObject> cubes=new List<GameObject>();
   
}
