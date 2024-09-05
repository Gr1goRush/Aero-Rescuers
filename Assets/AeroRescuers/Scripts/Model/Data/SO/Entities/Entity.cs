using UnityEngine;

public abstract class Entity : ScriptableObject
{
    public abstract ViewEntity View { get; }

    public abstract void Initialize(Transform parent);

    public abstract void FixedUpdate();

    public abstract void AddObjectDisposable();

    public abstract void Dispose();
}
