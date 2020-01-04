
namespace SkyTrespass.Character
{
    public interface IAttack
    {

        void AttackPrepare(AttackMachine attackMachine);
        void AttackStart();
        void AttackUpdate();
        void AttackTick();
        void AttackEnd();
        void AttackExit();
    }
}