using System;
using System.Linq;

namespace SpaceSurvival;

public class PlanetScene : Scene
{
    private readonly MapGenerate _map;
    private static MainHero _player;
    private Vector2 _playerCoords;
    private readonly Vector2 _playerPosition;
    private const int Scale = 6;

    private readonly PlanetEnemyManager _planetEnemyManager;
    private readonly LootManager _lootManager;

    private PlayerInventoryPanel _playerInventoryPanel;
    private readonly SpacewalkBtn _spacewalkBtn;

    private const float DurationLanding = 1f;
    private float _durationLandingLeft;

    private SoundEffectInstance _backSong;
    private readonly SoundEffectInstance _landingSound;
    private readonly SoundEffectInstance _selectLootSound;
    private SoundEffectInstance _stepSound;
    private readonly SoundEffectInstance _clickBtnSound;

    private Matrix _translation;

    public PlanetScene(TypePlanet typePlanet)
    {
        _map = new MapGenerate(Scale, typePlanet);
        _playerCoords = _map.GetRandomEmptyCell();
        _player = new MainHero(Globals.Content.Load<Texture2D>("PlayerSpriteSet"),
            _playerCoords, Scale, _map);
        _playerPosition = _player.Position;

        SetStepAndBackSound(typePlanet);
        _landingSound = Globals.Content.Load<SoundEffect>("Audio/landingSound").CreateInstance();
        _landingSound.Volume = 0.5f;
        _selectLootSound = Globals.Content.Load<SoundEffect>("Audio/sellectLootSound").CreateInstance();
        _clickBtnSound = Globals.Content.Load<SoundEffect>("Audio/clickBtnSound").CreateInstance();

        _lootManager = new LootManager(_map, Scale);
        _lootManager.AddLoot(LootType.Gold, 2);
        _lootManager.AddLoot(LootType.Ruby, 4);
        _lootManager.AddLoot(LootType.Bronze, 7);

        _planetEnemyManager = new PlanetEnemyManager(_player.Coords, _map, Scale);
        CreateEnemies(typePlanet);

        _playerInventoryPanel =
            new PlayerInventoryPanel(Globals.Content.Load<Texture2D>("Small_Blue_Panel"), _player.Position, 0.4f);
        _spacewalkBtn = new SpacewalkBtn(Globals.Content.Load<Texture2D>("Small_Orange_Cell"), Vector2.Zero, 0.5f);
        _spacewalkBtn.OnClick += ClickSpacewalkBtn;
    }

    private void SetStepAndBackSound(TypePlanet typePlanet)
    {
        switch (typePlanet)
        {
            case TypePlanet.Green:
                _backSong = Globals.Content.Load<SoundEffect>("Audio/backSongForGreenPlanet").CreateInstance();
                _stepSound = Globals.Content.Load<SoundEffect>("Audio/stepsOnGreenAndRedPlanet").CreateInstance();
                break;
            case TypePlanet.Red:
                _backSong = Globals.Content.Load<SoundEffect>("Audio/backSongForRedPlanet").CreateInstance();
                _stepSound = Globals.Content.Load<SoundEffect>("Audio/stepsOnGreenAndRedPlanet").CreateInstance();
                break;
            case TypePlanet.Violet:
                _stepSound = Globals.Content.Load<SoundEffect>("Audio/stepsOnVioletPlanet").CreateInstance();
                _backSong = Globals.Content.Load<SoundEffect>("Audio/backSongForVioletPlanet").CreateInstance();
                _backSong.Volume = 0.7f;
                break;
            case TypePlanet.Ice:
                _backSong = Globals.Content.Load<SoundEffect>("Audio/backSongForIcePlanet").CreateInstance();
                _stepSound = Globals.Content.Load<SoundEffect>("Audio/stepsOnIcePlanet").CreateInstance();
                break;
        }
    }

    private void CreateEnemies(TypePlanet typePlanet)
    {
        if (typePlanet == TypePlanet.Red || typePlanet == TypePlanet.Violet)
            _planetEnemyManager.CreateEnemies(10, Globals.Content.Load<Texture2D>("Enemy2"), 7);
        else if (typePlanet == TypePlanet.Green)
            _planetEnemyManager.CreateEnemies(10, Globals.Content.Load<Texture2D>("Enemy1"), 8);
        else if (typePlanet == TypePlanet.Ice)
            _planetEnemyManager.CreateEnemies(10, Globals.Content.Load<Texture2D>("Enemy3"), 7);
    }

    private void ClickSpacewalkBtn(object sender, EventArgs e)
    {
        _backSong.Pause();
        _clickBtnSound.Play();
        _player.SwordBattleSound.Stop();
        SceneManager.SwitchScene((int)TypeScene.SpaceScene);
    }


    protected override void Load()
    {
    }

    public override void Activate()
    {
        _durationLandingLeft = 0;
        _player.Coords = _playerCoords;
        _player.Position = _playerPosition;
        _player.Map = _map;

        _landingSound.Play();
        _backSong.Resume();

        _playerInventoryPanel =
            new PlayerInventoryPanel(Globals.Content.Load<Texture2D>("Small_Blue_Panel"), _player.Position, 0.4f);
    }


    private void CalculateTranslation()
    {
        var dx = _player.Position.X - Globals.WindowSize.X / 2f + _player.Size.X / 2f;
        dx = MathHelper.Clamp(dx, 0, _map.MapSize.X - Globals.WindowSize.X);
        var dy = _player.Position.Y - Globals.WindowSize.Y / 2f + _player.Size.Y / 2f;
        dy = MathHelper.Clamp(dy, 0, _map.MapSize.Y - Globals.WindowSize.Y);
        _translation = Matrix.CreateTranslation(-dx, -dy, 0f);
    }

    private void CheckCollisionWithLoot()
    {
        var loot = _lootManager.Loots.FirstOrDefault(loot => _player.Coords == loot.Coords);
        if (loot != null && InventoryManager.CapacityPlayerInventory > 0)
        {
            _selectLootSound.Play();
            InventoryManager.AddToPlayerInventory(loot);
            _lootManager.Loots.Remove(loot);
            _playerInventoryPanel.FillCellWithLoot();
        }
    }

    public override void Update()
    {
        if (_player.CurrentHealth <= 0)
        {
            _backSong.Stop();
            _player.Reset();
            SceneManager.SwitchScene((int)TypeScene.PlayerDeathScene);
        }

        if (InputManager.Direction != Vector2.Zero && !_player.IsAttacking)
            _stepSound.Play();
        else
            _stepSound.Stop();

        _durationLandingLeft += Globals.TotalSeconds;
        if (_durationLandingLeft > DurationLanding)
        {
            _planetEnemyManager.Update(_player.Coords);
            _playerInventoryPanel.Update(_player.Position, _player.Size, _map.MapSize);
            CheckCollisionWithLoot();
            _player.Update(_planetEnemyManager.Enemies);
            _spacewalkBtn.Update();
        }

        _spacewalkBtn.UpdatePosition(new Vector2(-_translation.Translation.X, -_translation.Translation.Y));
        _playerCoords = _player.Coords;
        CalculateTranslation();
    }

    protected override void Draw()
    {
        if (_durationLandingLeft < DurationLanding)
        {
            Globals.SpriteBatch.Draw(Globals.Content.Load<Texture2D>("landingBack"),
                new Rectangle((int)-_translation.Translation.X, (int)-_translation.Translation.Y, Globals.WindowSize.X,
                    Globals.WindowSize.Y), Color.White);
        }
        else
        {
            _map.Draw();
            _lootManager.Draw();
            _planetEnemyManager.Draw();
            _player.Draw();
            _playerInventoryPanel.Draw();
            _playerInventoryPanel.DrawLoot();
            _spacewalkBtn.Draw();
        }
    }

    public override RenderTarget2D GetFrame()
    {
        Globals.GraphicsDevice.SetRenderTarget(Target);
        Globals.GraphicsDevice.Clear(Color.Black);

        Globals.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _translation);
        Draw();
        Globals.SpriteBatch.End();

        Globals.GraphicsDevice.SetRenderTarget(null);
        return Target;
    }
}