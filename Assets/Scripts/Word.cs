using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class Word
{
    [XmlAttribute("name")]
    public string word;

    [XmlElement("Tag")]
    public string initialTag;

    [XmlElement("Audio")]
    public string wordClipPath;

    [XmlElement("Texture")]
    public string texturePath;
}

