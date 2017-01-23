using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CocosSharp;

namespace ruigeruben
{

    class TexturePool
    {
        public static CCSpriteSheet sheet = new CCSpriteSheet("OldSheetWhite.plist", "OldSheetWhite.png");

        public static CCSprite GetSprite(string Name)
        {
            CCSpriteFrame frame = sheet.Frames.Find(item => item.TextureFilename == Name + ".png");
            CCSprite sprite = new CCSprite(frame);
            return sprite;
        }
    }
}