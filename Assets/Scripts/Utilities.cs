using UnityEngine;

public static class Utilities
{
    public static float Wrap(float value, float min, float max)
    {
        
        return (value < min) ? max : (value > max) ? min : value;
    }
    
    public static Vector3 Wrap(Vector3 v, Vector3 min, Vector3 max)
    {
        v.x = Wrap(v.x, min.x, max.x);
        v.y = Wrap(v.y, min.y, max.y);
        v.z = Wrap(v.z, min.z, max.z);
        
        return v;
    }
}
