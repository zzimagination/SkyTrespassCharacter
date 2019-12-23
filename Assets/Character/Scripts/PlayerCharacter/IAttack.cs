
namespace SkyTrespass.Character
{
    public interface IAttack
    {

        void Prepare(AttackMachine attackMachine);
        void Start();
        void Update();
        void Tick();
        void End();
        void Exit();
    }
}