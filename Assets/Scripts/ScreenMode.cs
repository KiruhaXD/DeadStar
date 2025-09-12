using UnityEngine;

public class ScreenMode
{
    private string _name;
    private FullScreenMode _fullscreenMode;

    public ScreenMode(string name, FullScreenMode fullscreenMode)
    {
        _name = name;
        _fullscreenMode = fullscreenMode;
    }

    public string Name => _name;
    public FullScreenMode FullscreenMode => _fullscreenMode;
}
