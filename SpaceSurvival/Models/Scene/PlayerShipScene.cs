using System;

namespace SpaceSurvival;

public class PlayerShipScene : Scene
{
    private PlayerInventoryPanel _playerInventoryPanel;
    private ShipStoragePanel _shipStoragePanel;
    private const int DistanceBetweenPanels = 50;

    private SpacewalkBtn _spacewalkBtn;
    private SpriteFont _font;

    private SoundEffectInstance _backSong;

    protected override void Load()
    {
        DragDropManager.Init();
        _playerInventoryPanel = new PlayerInventoryPanel(Globals.Content.Load<Texture2D>("Small_Blue_Panel"),
            new Vector2(Globals.WindowSize.X / 2f, 0), 0.35f);
        _shipStoragePanel =
            new ShipStoragePanel(Globals.Content.Load<Texture2D>("Big_Blue_Panel"),
                new Vector2(Globals.WindowSize.X / 2f, 0), 0.38f);

        _spacewalkBtn = new SpacewalkBtn(Globals.Content.Load<Texture2D>("Small_Orange_Cell"), Vector2.Zero, 0.5f);
        _spacewalkBtn.OnClick += ClickSpacewalkBtn;
        _font = Globals.Content.Load<SpriteFont>("font");

        _backSong = Globals.Content.Load<SoundEffect>("Audio/soundInShip").CreateInstance();
        _backSong.IsLooped = true;
        _backSong.Volume = 0.4f;
    }

    public override void Activate()
    {
        Load();
        _backSong.Play();
        _shipStoragePanel.FillCellsWithLoot();
    }

    private void ClickSpacewalkBtn(object sender, EventArgs e)
    {
        _backSong.Pause();
        SceneManager.SwitchScene((int)TypeScene.SpaceScene);
    }

    public override void Update()
    {
        var offsetFromTopScreen =
            (Globals.WindowSize.Y - _shipStoragePanel.Size.Y - _playerInventoryPanel.Size.Y - DistanceBetweenPanels) /
            2f;
        _shipStoragePanel.Update(Globals.WindowSize.X / 2f - _shipStoragePanel.Size.X / 2f, offsetFromTopScreen);
        _playerInventoryPanel.Update(Globals.WindowSize.X / 2f - _playerInventoryPanel.Size.X / 2f,
            _shipStoragePanel.Position.Y + _shipStoragePanel.Size.Y + DistanceBetweenPanels);
        DragDropManager.Update();

        _spacewalkBtn.Update();
    }

    protected override void Draw()
    {
        Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>("spaceShipBack"), Vector2.Zero, null, Color.White, 0f,
            Vector2.Zero, 1f, SpriteEffects.None, 0f);
        Globals.SpriteBatch.DrawString(_font, "SHIP STORAGE",
            new Vector2(Globals.WindowSize.X / 2f - 230, _shipStoragePanel.Position.Y - 70),
            Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        _shipStoragePanel.Draw();
        _playerInventoryPanel.Draw();
        _shipStoragePanel.DrawLoot();
        _playerInventoryPanel.DrawLoot();
        _spacewalkBtn.Draw();
    }
}