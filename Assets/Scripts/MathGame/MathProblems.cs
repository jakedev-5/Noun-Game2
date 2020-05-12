using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class MathProblems
{
    [XmlAttribute("id")]
    public string problemID;

    [XmlElement("QuestionLeading")]
    public string questionLeading;

    [XmlElement("QuestionLeadingAudio")]
    public string questionLeadingAudio;

    [XmlElement("Answer_1")]
    public string answer1;

    [XmlElement("Answer_2")]
    public string answer2;

    [XmlElement("Answer_3")]
    public string answer3;

    [XmlElement("Answer_4")]
    public string answer4;

    [XmlElement("Answer_5")]
    public string answer5;

    [XmlElement("Answer_6")]
    public string answer6;

    [XmlElement("CorrectAnswerIndex")]
    public int correctAnswerIndex;
}