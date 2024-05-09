using System;
using System.Collections.Generic;

namespace SpaceSurvival;

public class UIManager
{
    private SmallBluePanel _smallBluePanel;

    public UIManager()
    {
    }

    public void CreateInventoryPanel(Vector2 pos)
    {
        _smallBluePanel = new SmallBluePanel(Globals.Content.Load<Texture2D>("Small_Blue_Panel"), pos, 0.4f);
    }

    public void Update(Vector2 playerPos, Point mapSize)
    {
        _smallBluePanel.Update(playerPos, mapSize);
    }

    public void Draw()
    {
        _smallBluePanel.Draw();
    }
}