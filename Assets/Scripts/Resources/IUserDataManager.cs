namespace Assets.Scripts.Resources
{
    public interface IUserDataManager
    {
        int CurrentLevel();
        void SetNextLevel();
        void SetCurrentLevel(int level);
    }
}
