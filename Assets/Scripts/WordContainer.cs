using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("WordList")]
public class WordContainer
{

    [XmlArray("Words")]
    [XmlArrayItem("Word")]
    public List<Word> words = new List<Word>(); // Initialize word list

    // Utility function to load our WordList XML
    public static WordContainer Load(string path)
    {
        // Load the XML file from the passed path
        TextAsset _xml = Resources.Load<TextAsset>(path);

        // Initialize a serializer
        XmlSerializer serializer = new XmlSerializer(typeof(WordContainer));
        
        // Initialize string reader using our loaded XML file
        StringReader reader = new StringReader(_xml.text);

        // Initialize our WordContainer from serialized XML 
        WordContainer words = serializer.Deserialize(reader) as WordContainer;

        // Close the reader to save resources
        reader.Close();

        // Return WordContainer <words>
        return words;
    }
}

