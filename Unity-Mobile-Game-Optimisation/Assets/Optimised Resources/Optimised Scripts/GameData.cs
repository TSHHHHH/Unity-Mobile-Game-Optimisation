using System;

[Serializable]
public class GameData
{
    public bool FPSCounterToggle;
    public float musicVolume;

    public GameData(SettingCanvas settingData)
    {
        FPSCounterToggle = settingData.FPSCounterToggle;
        musicVolume = settingData.musicVolume;
    }
}