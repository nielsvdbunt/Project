using System;
using System.Collections.Generic;
using CocosSharp;
using Microsoft.Xna.Framework;

namespace ruigeruben
{
    public delegate void ClickEventHandler();
    class Button
    {
        bool m_TexTOnly;

        CCLabel m_Label;
        CCSprite m_Sprite;

        public event ClickEventHandler OnClicked;

        public Button(string Text, CCPoint Possition, string Font, int FontSize, CCLayer Layer)    
        {
            m_Label = new CCLabel(Text, Font, FontSize, CCLabelFormat.SpriteFont);
            m_Label.Position = Possition;
            Layer.AddChild(m_Label, 1);

            m_TexTOnly = true;
        }

        public Button(string Text, CCPoint Possition, string Font, int FontSize, CCLayer Layer, int tag)
        {
            m_Label = new CCLabel(Text, Font, FontSize, CCLabelFormat.SpriteFont);
            m_Label.Position = Possition;
            m_Label.Tag = tag;
            Layer.AddChild(m_Label, 1);

            m_TexTOnly = true;
        }

        public Button(string ImageName, string Text, CCPoint Possition, string Font, int FontSize, CCLayer Layer)
        {
            m_Sprite = new CCSprite(ImageName);
            m_Sprite.Position = Possition;
            Layer.AddChild(m_Sprite);

            m_Label = new CCLabel(Text, Font, FontSize, CCLabelFormat.SpriteFont);
            m_Label.Position = Possition;
            Layer.AddChild(m_Label, 1);

            m_TexTOnly = false;
        }
        public void SetTextPossition(CCPoint Possition)
        {
            m_Label.Position = Possition;
        }

        public void SetTextAnchorpoint(CCPoint anchorpoint)
        {
            m_Label.AnchorPoint = anchorpoint;
        }

        public void SetTextColor(CCColor3B color)
        {
            m_Label.Color = color;
        }

        public CCSprite GetSprite()
        {
            return m_Sprite;
        }

        public bool OnClickEvent(CCPoint Location)
        {
            CCNode node;
            if (m_TexTOnly)
                node = m_Label;
            else
                node = m_Sprite;

            if (node.BoundingBoxTransformedToWorld.ContainsPoint(Location))
            {
                if(OnClicked != null)
                        OnClicked();
                
                return true;
            }

            return false;          
        }

        public void Rotate()
        {
            m_Label.Rotation = 90f;
        }
          
    }
}