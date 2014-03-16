using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PokePlanet
{
    [XmlRoot("img", IsNullable = false)]
    public class SpriteSheet
    {
        public Texture2D Texture;
        
        [XmlElement("definitions")] public List<Definition> DefinitionList;

        [XmlAttribute("name")] public String ImageName;

        [XmlAttribute("w")] public int Width;

        [XmlAttribute("h")] public int Height;

        public static SpriteSheet FromFile(ContentManager content, string file)
        {
            using (var stream = File.OpenRead(file))
            {
                SpriteSheet sheet = FromStream(stream);
                if (sheet.ImageName != null)
                {
                    Console.WriteLine("Loading texture " + sheet.ImageName);
                    sheet.Texture = content.Load<Texture2D>("sprites/" + sheet.ImageName.Replace(".png", ""));
                }
                else
                {
                    Console.WriteLine("no content found for " + sheet.ImageName);
                }
                return sheet;
            }
        }

        public static SpriteSheet FromStream(Stream stream)
        {
            var serializer = new XmlSerializer(typeof (SpriteSheet));
            return (SpriteSheet) serializer.Deserialize(stream);
        }

        public SpriteCoords GetSprite(string name)
        {
            Console.WriteLine("fetchin sprite " + name);
            String withNoSlash = name.Substring(1);
            return DefinitionList[0].DirList[0].Sprites.FirstOrDefault(sprite => sprite.Name.Equals(withNoSlash));
        }
}

    public class Definition
    {
        [XmlElement("dir")]
        public List<SpriteDir> DirList;
    }

    public class SpriteDir
    {
        [XmlAttribute("name")]
        public String Name;

        [XmlElement("spr")]
        public List<SpriteCoords> Sprites;
    }

    public class SpriteCoords
    {
        [XmlAttribute("name")]
        public String Name;

        [XmlAttribute("x")]
        public int X;

        [XmlAttribute("y")]
        public int Y;

        [XmlAttribute("w")]
        public int Width;

        [XmlAttribute("h")]
        public int Height;
    }
}