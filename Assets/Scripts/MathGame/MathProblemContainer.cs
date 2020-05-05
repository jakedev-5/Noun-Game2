using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("ProblemList")]
public class MathProblemContainer 
{

    [XmlArray("Problems")]
    [XmlArrayItem("Problem")]
    public List<MathProblems> problems = new List<MathProblems>();

    public static MathProblemContainer Load(string path)
    {
        TextAsset _xml = Resources.Load<TextAsset>(path);

        XmlSerializer serializer = new XmlSerializer(typeof(MathProblemContainer));

        StringReader reader = new StringReader(_xml.text);

        MathProblemContainer problems = serializer.Deserialize(reader) as MathProblemContainer;

        reader.Close();

        return problems;
    }
}
