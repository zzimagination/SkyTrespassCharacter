
namespace SkyTrespass.Character
{
    public interface IAttack
    {

        void AttackPrepare();
        void AttackStart();
        void AttackUpdate();
        void AttackTick();
        void AttackEnd();
        void AttackExit();
    }
}