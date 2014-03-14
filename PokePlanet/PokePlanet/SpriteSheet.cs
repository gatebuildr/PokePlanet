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

        [XmlAttribute("path")] public String SheetPath;

        [XmlElement("animatedsprite")] public List<AnimatedSprite> SpriteList;

        [XmlElement("definitions")] public List<Definition> DefinitionList;

        [XmlAttribute("name")] public String ImageName;

        public static SpriteSheet FromFile(ContentManager content, string file)
        {
            using (var stream = File.OpenRead(file))
            {
                SpriteSheet sheet = FromStream(stream);
                if (sheet.SheetPath != null)
                    sheet.Texture = content.Load<Texture2D>(sheet.SheetPath);
                return sheet;
            }
        }

        public static SpriteSheet FromStream(Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof (SpriteSheet));
            return (SpriteSheet) serializer.Deserialize(stream);
        }

        public static void LoadAnimations(string path)
        {
            var serializer = new XmlSerializer(typeof (AnimatedSprite));
            var stream = File.OpenRead(path);
            AnimatedSprite animation = (AnimatedSprite) serializer.Deserialize(stream);
        }

        public AnimatedSprite GetAnimation(String name)
        {
            Console.WriteLine(SpriteList.Count + " elements in spritelist");
            foreach (AnimatedSprite sprite in SpriteList)
            {
                //Console.WriteLine(sprite.Name);
                if (sprite.Name.Equals(name))
                    return sprite;
            }
            Console.WriteLine("couldn't find your animation, bro");
            return null;
        }

        public SpriteCoords GetSprite(string name)
        {
            return DefinitionList[0].DirList[0].Sprites[0];
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