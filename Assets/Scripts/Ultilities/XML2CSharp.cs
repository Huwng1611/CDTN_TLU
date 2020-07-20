/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace Xml2CSharp
{
    [XmlRoot(ElementName = "unit")]
    [System.Serializable]
    public class Unit
    {
        [XmlAttribute(AttributeName = "x")]
        public string X;
        [XmlAttribute(AttributeName = "y")]
        public string Y;
    }

    [XmlRoot(ElementName = "walls")]
    [System.Serializable]
    public class Walls
    {
        [XmlElement(ElementName = "unit")]
        public List<Unit> Unit;
    }

    [XmlRoot(ElementName = "players")]
    [System.Serializable]
    public class Players
    {
        [XmlElement(ElementName = "unit")]
        public Unit Unit;
    }

    [XmlRoot(ElementName = "boxes")]
    [System.Serializable]
    public class Boxes
    {
        [XmlElement(ElementName = "unit")]
        public List<Unit> Unit;
    }

    [XmlRoot(ElementName = "targets")]
    [System.Serializable]
    public class Targets
    {
        [XmlElement(ElementName = "unit")]
        public List<Unit> Unit;
    }

    [XmlRoot(ElementName = "level")]
    [System.Serializable]
    public class Level
    {
        [XmlElement(ElementName = "walls")]
        public Walls Walls;
        [XmlElement(ElementName = "players")]
        public Players Players;
        [XmlElement(ElementName = "boxes")]
        public Boxes Boxes;
        [XmlElement(ElementName = "targets")]
        public Targets Targets;
        [XmlAttribute(AttributeName = "name")]
        public string Name;
    }

}
