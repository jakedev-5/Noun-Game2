using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("ProblemList")]
public class fillblank_ProblemsContainer {

    [XmlArray("Problems")]
    [XmlArrayItem("Problem")]
    public List<fillblank_Problems> problems = new List<fillblank_Problems>();

    public static fillblank_ProblemsContainer Load(string path)
    {
        TextAsset _xml = Resources.Load<TextAsset>(path);

        XmlSerializer serializer = new XmlSerializer(typeof(fillblank_ProblemsContainer));

        StringReader reader = new StringReader(_xml.text);

        fillblank_ProblemsContainer problems = serializer.Deserialize(reader) as fillblank_ProblemsContainer;

        reader.Close();

        return problems;
    }
}
