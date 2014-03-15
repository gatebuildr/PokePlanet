using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Xna.Framework;
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
                    sheet.Texture = content.Load<Texture2D>("Graphics/" + sheet.ImageName.Replace(".png", ""));
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
            XmlSerializer serializer = new XmlSerializer(typeof (SpriteSheet));
            return (SpriteSheet) serializer.Deserialize(stream);
        }

        public SpriteCoords GetSprite(string name)
        {
            Console.WriteLine("fetchin sprite " + name);
            String withNoSlash = name.Substring(1);
            foreach (SpriteCoords sprite in DefinitionList[0].DirList[0].Sprites)
            {
                if (sprite.Name.Equals(withNoSlash))
                    return sprite;
            }
            return null;
//            return DefinitionList[0].DirList[0].Sprites[0];
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
        public int x;

        [XmlAttribute("y")]
        public int y;

        [XmlAttribute("w")]
        public int Width;

        [XmlAttribute("h")]
        public int Height;
    }
}