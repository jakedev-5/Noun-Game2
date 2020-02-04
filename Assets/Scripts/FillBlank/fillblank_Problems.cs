using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class fillblank_Problems
{
    [XmlAttribute("id")]
    public string problemID;

    [XmlElement("QuestionLeading")]
    public string questionLeading;

    [XmlElement("QuestionLeadingAudio")]
    public string questionLeadingAudio;

    [XmlElement("QuestionTrailing")]
    public string questionTrailing;

    [XmlElement("QuestionTrailingAudio")]
    public string questionTrailingAudio;

    [XmlElement("Answer_1")]
    public string answer1;

    [XmlElement("Answer_2")]
    public string answer2;

    [XmlElement("Answer_3")]
    public string answer3;

    [XmlElement("CorrectAnswerIndex")]
    public int correctAnswerIndex;
}
