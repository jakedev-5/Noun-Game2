using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("MathProblemList")]
public class MathProblemsContainer {

    [XmlArray("MathProblems")]
    [XmlArrayItem("Problem")]
    public List<MathProblems> problems = new List<MathProblems>();

    public static MathProblemsContainer Load(string path)
    {
        TextAsset _xml = Resources.Load<TextAsset>(path);

        XmlSerializer serializer = new XmlSerializer(typeof(MathProblemsContainer));

        StringReader reader = new StringReader(_xml.text);

        MathProblemsContainer problems = serializer.Deserialize(reader) as MathProblemsContainer;

        reader.Close();

        return problems;
    }
}
