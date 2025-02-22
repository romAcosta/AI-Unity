using UnityEngine;

public static class Utilities
{
    public static float Wrap(float value, float min, float max)
    {
        return value < min ? max : value > max ? min : value;
    }

    public static Vector3 Wrap(Vector3 v, Vector3 min, Vector3 max)
    {
        v.x = Wrap(v.x, min.x, max.x);
        v.y = Wrap(v.y, min.y, max.y);
        v.z = Wrap(v.z, min.z, max.z);

        return v;
    }

    public static Vector3[] GetDirectionsInCircle(int num, float angle)
    {
        if (num <= 0) return null;
        if (num == 1) return new[] { Vector3.forward };

        // create array of vector3
        var result = new Vector3[num];
        var index = 0;

        // set forward direction if odd number
        if (num % 2 == 1)
        {
            result[index++] = Vector3.forward;
            num--;
        }

        // compute angle between rays (angle * 2 / num rays - 1)
        var angleOffset = angle * 2 / num - 1;

        // add directions symmetrically around the circle
        for (var i = 1; i <= num / 2; i++)
        {
            result[index++] = Quaternion.AngleAxis(+angleOffset * i, Vector3.up) * Vector3.forward;
            result[index++] = Quaternion.AngleAxis(-angleOffset * i, Vector3.up) * Vector3.forward;
        }

        return result;
    }
}