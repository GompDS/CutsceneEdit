using System.Numerics;

namespace CutsceneEdit;

public class Options
{
    public enum EditType
    {
        Change,
        Set,
        None
    }
    
    public EditType RotateType = EditType.None;

    public EditType MoveType = EditType.None;
    public Vector3 VectorChange { get; }

    public int CutIndex = -1;
    public Options(string[] args)
    {
        if (args[0].Length != 2) throw new IOException("Incorrect arguments given.");
        if (args[0][0] == 'r')
        {
            RotateType = args[0][1] switch
            {
                'c' => EditType.Change,
                's' => EditType.Set,
                _ => EditType.None
            };
        }
        else if (args[0][0] == 'm')
        {
            MoveType = args[0][1] switch
            {
                'c' => EditType.Change,
                's' => EditType.Set,
                _ => EditType.None
            };
        }
        else
        {
            throw new IOException("Must choose to rotate or move.");
        }

        Vector3 vectorChange = VectorChange;
        if (float.TryParse(args[1], out float valueX))
        {
            vectorChange.X = valueX;
        }
        else
        {
            vectorChange.X = 0;
        }
        
        if (float.TryParse(args[2], out float valueY))
        {
            vectorChange.Y = valueY;
        }
        else
        {
            vectorChange.Y = 0;
        }
        
        if (float.TryParse(args[3], out float valueZ))
        {
            vectorChange.Z = valueZ;
        }
        else
        {
            vectorChange.Z = 0;
        }
        
        if (int.TryParse(args[4], out int cutIndexValue))
        {
            CutIndex = cutIndexValue;
        }

        VectorChange = vectorChange;
    }
}