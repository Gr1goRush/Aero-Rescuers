using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinPlane", menuName = "ScriptableObject/SkinPlane")]
public class SkinPlane : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _spriteSkin;

    public string Name => _name;
    public Sprite SpriteSkin => _spriteSkin;
}
