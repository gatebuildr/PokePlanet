using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PokePlanet
{
    public class SpriteReference
    {
        [XmlAttribute("name")] public string Name;
        [XmlAttribute("x")] public int X;
        [XmlAttribute("y")] public int Y;
        [XmlAttribute("z")] public int Z;
    }

    public class Cell
    {
        [XmlAttribute("index")] public int Index;
        [XmlAttribute("delay")] public int Delay;
        [XmlElement("spr")] public SpriteReference Sprite;
    }

    public class Animation
    {
        [XmlAttribute("name")] public String Name;
        [XmlAttribute("loops")] public int Loops;
        [XmlElement("cell")] public List<Cell> Cells;
        public string SpriteSheetName;
    }
}