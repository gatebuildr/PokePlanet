using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace PokePlanet
{
    [XmlRoot("animations", IsNullable = false)]
    public class AnimationSet
    {
        [XmlAttribute("spriteSheet")] public string SpriteSheetName;
        [XmlAttribute("ver")] public String Version;
        [XmlElement("anim")] public List<Animation> Animations;

        public static List<Animation> LoadAnimations(string path)
        {
            var serializer = new XmlSerializer(typeof(AnimationSet));
            var stream = File.OpenRead(path);
            Console.WriteLine("Loading animations: " + path);
            var animationSet = (AnimationSet)serializer.Deserialize(stream);
            List<Animation> animations = animationSet.Animations;
            foreach(Animation animation in animations)
            {
                animation.SpriteSheetName = animationSet.SpriteSheetName;
            }
            return animations;
        }
    }
}
