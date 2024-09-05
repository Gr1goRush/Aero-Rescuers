using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public static class ManagerUniRx
{
    private static List<IDisposable> _objectsForDispose = new List<IDisposable>();

    public static void AddObjectDisposable(IDisposable disposable) =>
        _objectsForDispose.Add(disposable);

    public static void Dispose<T>(ReactiveCommand<T> command)
    {
        if (command.IsDisposed)
            return;

        Disposeble(command);
    }

    public static void Dispose<T>(ReactiveProperty<T> property)
        => Disposeble(property);

    private static void Disposeble(IDisposable disposable)
    {
        _objectsForDispose.Remove(disposable);
        disposable.Dispose();
    }
}