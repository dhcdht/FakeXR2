public enum FingerType
{
    None,
    Thumb,
    Index,
    Middle,
    Ring,
    Pinky
}

public class Finger
{
    public FingerType _type = FingerType.None;
    public float _current = 0.0f;
    public float _target = 0.0f;

    public Finger(FingerType type)
    {
        _type = type;
    }
}
