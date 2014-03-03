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
    [XmlRoot("spritesheet", IsNullable=false)]
    public class SpriteSheet
    {
        public static SpriteSheet FromFile(ContentManager Content, String file)
        {
            using (var stream = File.OpenRead(file))
            {
                SpriteSheet sheet = FromStream(stream);
                return sheet;
            }
        }

        public static SpriteSheet FromStream(Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SpriteSheet));
            return (SpriteSheet)serializer.Deserialize(stream);
        }

        public AnimatedSprite GetAnimation(String name)
        {
            Console.WriteLine(SpriteList.Count + " elements in spritelist");
            foreach(AnimatedSprite sprite in SpriteList)
            {
                Console.WriteLine(sprite.Name);
                if (sprite.Name.Equals(name))
                    return sprite;
            }
            Console.WriteLine("couldn't find your animation, bro");
            return null;
        }

        [XmlAttribute("path")]
        public String SheetPath;

        [XmlElement("animatedsprite")]
        public List<AnimatedSprite> SpriteList;
    }
}