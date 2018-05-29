using UnityEngine;
using UnityEditor;

public class GameWeapon
{
    public string Name { get; set; }
    public float Damage { get; set; }
    public float FireRate { get; set; }
    public float ThrowForce { get; set; }
    public float Scale { get; set; }
    public Transform Model { get; set; }
}