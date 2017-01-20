using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CocosSharp;

namespace ruigeruben
{

    class TexturePool
    {
        static CCSpriteSheet sheet = new CCSpriteSheet("sheetfinal.plist", "sheetimage.png");

        public static CCSprite GetSprite(string Name)
        {
            CCSpriteFrame frame = sheet.Frames.Find(item => item.TextureFilename == Name + ".png");
            CCSprite sprite = new CCSprite(frame);
            return sprite;
        }
    }
}