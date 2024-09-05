using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private LevelEntity _currentLevel;
    private PlaneEntity _currentPlane;

    [SerializeField] private List<LevelEntity> _levels;
    [SerializeField] private List<PlaneEntity> _planes;

    public LevelEntity CurrentLevel => _currentLevel;
    public PlaneEntity CurrentPlane => _currentPlane;

    private void Awake()
    {
        ContainerSaveerPlayerPrefs.Instance.SaveerData.MaxLevel = 30;
        var findedLevel =_levels.Find(level => level.LevelNumber == (ContainerSaveerPlayerPrefs.Instance.SaveerData.TypeGame == 1 ? ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentLevel : 0));
        _currentLevel = findedLevel;

        _currentPlane = _planes.Find(skin => skin.SkinPlane.Name == ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentSkin);
    }
}
