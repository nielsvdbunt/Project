using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CocosSharp;

namespace ruigeruben
{

    class TexturePool //texturepool makes a spritesheet and makes the seperate pictures from the spritesheet
    {
        public static CCSpriteSheet sheet = new CCSpriteSheet("final.plist", "final.png");

        public static CCSprite GetSprite(string Name)
        {
            CCSpriteFrame frame = sheet.Frames.Find(item => item.TextureFilename == Name + ".png");
            CCSprite sprite = new CCSprite(frame);
            sprite.IsColorModifiedByOpacity = true;
            sprite.IsColorCascaded = true;
         
            
            return sprite;
        }
    }
}