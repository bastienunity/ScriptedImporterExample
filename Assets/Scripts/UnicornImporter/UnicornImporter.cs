using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

[ScriptedImporter(0, ".unicorn")]
public class UnicornImporter : ScriptedImporter
{
    public bool m_TailColorOverride;
    public Color[] m_TailColors;
    public Color[] m_FileTailColors;

    private Color ParseColor(string text)
    {
        var split = Regex.Match(text, @"color\(([01](?:\.[0-9]+)?), ?([01](?:\.[0-9]+)?), ?([01](?:\.[0-9]+)?)(?:, ?([01](?:\.[0-9]+)?))?\)");
        if(split.Groups.Count == 5)
            return new Color(float.Parse(split.Groups[1].Value),
                float.Parse(split.Groups[2].Value),
                float.Parse(split.Groups[3].Value),
                float.Parse(split.Groups[4].Value));
        if (split.Groups.Count == 4)
            return new Color(float.Parse(split.Groups[1].Value),
                float.Parse(split.Groups[2].Value),
                float.Parse(split.Groups[3].Value));
        return Color.magenta;
    }

    public override void OnImportAsset(AssetImportContext ctx)
    {
        var unicorn = ScriptableObject.CreateInstance<Unicorn>();
        unicorn.m_UnicornName = Path.GetFileNameWithoutExtension(ctx.assetPath);

        string[] lines = File.ReadAllLines(ctx.assetPath);
        m_FileTailColors = new Color[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            m_FileTailColors[i] = ParseColor(lines[i]);
        }

        unicorn.m_TailColors = m_TailColorOverride ? m_TailColors : m_FileTailColors;

        ctx.AddObjectToAsset("unicorn", unicorn);
        ctx.SetMainObject(unicorn);
    }
}
