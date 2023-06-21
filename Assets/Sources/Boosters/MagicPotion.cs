using Sources.PlayerScripts;

namespace Sources.Boosters
{
    public class MagicPotion : Booster
    {
        private const float BoostValue = 2.5f;
        private Character _character;

        public override void Activate()
        {
            _character.SetSpeed(BoostValue);
        }

        public void SetCharacter(Character character)
        {
            _character = character;
        }
    }
}