using System.Numerics;
using SoulsFormats;

namespace CutsceneEdit;

public static class Program
{
    public static void Main(string[] args)
    {
        Options op = new (args);
        ISoulsFile backup;
        string? filePath = args.FirstOrDefault(x => File.Exists(x));
        if (filePath == null)
        {
            throw new IOException("No BND4 or MQB given in arguments.");
        }
        if (BND4.IsRead(filePath, out BND4 bnd))
        {
            backup = BND4.Read(filePath);
            BinderFile? file = bnd.Files.FirstOrDefault(x => MQB.Is(x.Bytes));
            if (file != null)
            {
                MQB cutscene = MQB.Read(file.Bytes);
                cutscene.EditCutscene(op);
                file.Bytes = cutscene.Write();
            }

            bnd.Write(filePath);
        }
        else if (MQB.IsRead(filePath, out MQB cutscene))
        {
            backup = MQB.Read(filePath);
            cutscene.EditCutscene(op);
            cutscene.Write(filePath);
        }
        else
        {
            return;
        }

        if (!File.Exists($"{filePath}.bak"))
        {
            backup.Write($"{filePath}.bak");
        }
    }

    public static void EditCutscene(this MQB cutscene, Options op)
    {
        if (op.RotateType == Options.EditType.Change)
        {
            cutscene.ChangeCutsceneRotation(op.VectorChange.X, op.VectorChange.Y, op.VectorChange.Z, op.CutIndex);
        }
        else if (op.RotateType == Options.EditType.Set)
        {
            cutscene.SetCutsceneRotation(op.VectorChange.X, op.VectorChange.Y, op.VectorChange.Z, op.CutIndex);
        }
        
        if (op.MoveType == Options.EditType.Change)
        {
            cutscene.ChangeCutscenePosition(op.VectorChange.X, op.VectorChange.Y, op.VectorChange.Z, op.CutIndex);
        }
        else if (op.MoveType == Options.EditType.Set)
        {
            cutscene.SetCutscenePosition(op.VectorChange.X, op.VectorChange.Y, op.VectorChange.Z, op.CutIndex);
        }
    }

    public static void ChangeCutscenePosition(this MQB cutscene, float changeX, float changeY, float changeZ, int cutIndex)
    {
        List<MQB.Transform> transforms = new();
        
        if (cutIndex > -1)
        {
            transforms.AddRange(cutscene.Cuts[cutIndex].Timelines.SelectMany(y => y.Dispositions
                .SelectMany(z => z.Transforms)));
        }
        else
        {
            transforms.AddRange(cutscene.Cuts.Where(x => x.Duration >= 0)
                .SelectMany(x => x.Timelines.SelectMany(y => y.Dispositions
                    .SelectMany(z => z.Transforms))));
        }

        foreach (MQB.Transform transform in transforms)
        {
            transform.Translation = new Vector3(transform.Translation.X + changeX, transform.Translation.Y + changeY, transform.Translation.Z + changeZ);
        }
    }

    public static void SetCutscenePosition(this MQB cutscene, float newX, float newY, float newZ, int cutIndex)
    {
        if (cutIndex < 0)
        {
            throw new ArgumentException("You can only set the position of one cut at a time. Make sure to specify the cut index.");
        }
        foreach (MQB.Transform transform in cutscene.Cuts[cutIndex].Timelines.SelectMany(y => y.Dispositions
                         .SelectMany(z => z.Transforms)))
        {
            transform.Translation = new Vector3(newX, newY, newZ);
        }
    }
    
    public static void ChangeCutsceneRotation(this MQB cutscene, float changeX, float changeY, float changeZ, int cutIndex)
    {
        List<MQB.Transform> transforms = new();
        
        if (cutIndex > -1)
        {
            transforms.AddRange(cutscene.Cuts[cutIndex].Timelines.SelectMany(y => y.Dispositions
                .SelectMany(z => z.Transforms)));
        }
        else
        {
            transforms.AddRange(cutscene.Cuts.Where(x => x.Duration >= 0)
                .SelectMany(x => x.Timelines.SelectMany(y => y.Dispositions
                    .SelectMany(z => z.Transforms))));
        }

        foreach (MQB.Transform transform in transforms)
        {
            transform.Rotation = new Vector3(transform.Rotation.X + changeX, transform.Rotation.Y + changeY, transform.Rotation.Z + changeZ);
        }
    }
    
    public static void SetCutsceneRotation(this MQB cutscene, float newX, float newY, float newZ, int cutIndex)
    {
        List<MQB.Transform> transforms = new();
        
        if (cutIndex > -1)
        {
            transforms.AddRange(cutscene.Cuts[cutIndex].Timelines.SelectMany(y => y.Dispositions
                .SelectMany(z => z.Transforms)));
        }
        else
        {
            transforms.AddRange(cutscene.Cuts.Where(x => x.Duration >= 0)
                .SelectMany(x => x.Timelines.SelectMany(y => y.Dispositions
                    .SelectMany(z => z.Transforms))));
        }

        foreach (MQB.Transform transform in transforms)
        {
            transform.Rotation = new Vector3(newX, newY, newZ);
        }
    }
}