using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveerDataInPlayerPrefs : SaveerData
{
    private readonly string KEY_COUNT_MONEY = "Money";
    private readonly string KEY_COUNT_GAME_MONEY = "GameMoney";
    private readonly string KEY_COUNT_SKYDRIVER = "CountSkydriver";
    private readonly string KEY_VOLUME_MUSIC = "VolumeMusic";
    private readonly string KEY_VOLUME_SOUND = "VolumeSound";
    private readonly string KEY_IS_VIBRATION_ON = "IsVibrationOn";
    private readonly string KEY_CURRENT_SKINS = "CurrentSkins";
    private readonly string KEY_OPEN_SKINS = "OpenSkins";
    private readonly string KEY_TYPE_GAME = "TypeGame";
    private readonly string KEY_CURRENT_LEVEL = "CurrentLevel";
    private readonly string KEY_MAX_OPENED_LEVEL = "MaxOpenedLevel";
    private readonly string KEY_MAX_LEVEL = "MaxLevel";
    private readonly string KEY_IS_EDUCATION = "IsEducation";
    private readonly string KEY_OPENED_LEVELS = "OpenedLevels";

    public int Money { get { return Load<int>(KEY_COUNT_MONEY, 0); } set { Save<int>(KEY_COUNT_MONEY, value); } }
    public int CountSkydriver { get { return Load<int>(KEY_COUNT_SKYDRIVER, 0); } set { Save<int>(KEY_COUNT_SKYDRIVER, value); } }
    public int GameMoney { get { return Load<int>(KEY_COUNT_GAME_MONEY, 0); } set { Save<int>(KEY_COUNT_GAME_MONEY, value); } }
    public float VolumeMusic { get { return Load<float>(KEY_VOLUME_MUSIC, 1); } set { Save<float>(KEY_VOLUME_MUSIC, value); } }
    public float VolumeSound { get { return Load<float>(KEY_VOLUME_SOUND, 0.6f); } set { Save<float>(KEY_VOLUME_SOUND, value); } }
    public int IsVibrationOn { get { return Load<int>(KEY_IS_VIBRATION_ON, 1); } set { Save<int>(KEY_IS_VIBRATION_ON, value); } }
    public string CurrentSkin { get { return Load<string>(KEY_CURRENT_SKINS, "Plane1"); } set { Save<string>(KEY_CURRENT_SKINS, value); } }
    public string OpenSkins { get { return Load<string>(KEY_OPEN_SKINS, "Plane1"); } set { Save<string>(KEY_OPEN_SKINS, value); } }
    public int TypeGame { get { return Load<int>(KEY_TYPE_GAME, 1); } set { Save<int>(KEY_TYPE_GAME, value); } }
    public int CurrentLevel { get { return Load<int>(KEY_CURRENT_LEVEL, 1); } set { Save<int>(KEY_CURRENT_LEVEL, value); } }
    public int MaxOpenedLevel { get { return Load<int>(KEY_MAX_OPENED_LEVEL, 1); } set { Save<int>(KEY_MAX_OPENED_LEVEL, value); } }
    public int MaxLevel { get { return Load<int>(KEY_MAX_LEVEL, 1); } set { Save<int>(KEY_MAX_LEVEL, value); } }
    public int IsEducation { get { return Load<int>(KEY_IS_EDUCATION, 0); } set { Save<int>(KEY_IS_EDUCATION, value); } }
    public string OpenedLevels { get { return Load<string>(KEY_OPENED_LEVELS, "1;11;21;"); } set { Save<string>(KEY_OPENED_LEVELS, value); } }

    public override T Load<T>(string nameParameter, T defaultValue)
    {
        if (PlayerPrefs.HasKey(nameParameter) == false)
            return defaultValue;

        Type inType = typeof(T);

        if (inType == typeof(int))
            return (T)(object)PlayerPrefs.GetInt(nameParameter);
        else if (inType == typeof(float))
            return (T)(object)PlayerPrefs.GetFloat(nameParameter);
        else
            return (T)(object)PlayerPrefs.GetString(nameParameter);
    }

    public override void Save<T>(string nameParameter, T value)
    {
        Type inType = typeof(T);

        if (inType == typeof(int))
            PlayerPrefs.SetInt(nameParameter, int.Parse(value.ToString()));
        else if (inType == typeof(float))
            PlayerPrefs.SetFloat(nameParameter, float.Parse(value.ToString()));
        else if (inType == typeof(string))
            PlayerPrefs.SetString(nameParameter, value.ToString());
    }
}
