[System.Serializable]
public enum BulletType
{
    parabolic,
    magnetic,
    bounce
}

[System.Serializable]
public struct Bullet
{
    public float speed;
    public BulletType bulletType;
    public int numberOfBounces;
}