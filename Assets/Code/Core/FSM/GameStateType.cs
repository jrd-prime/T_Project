namespace Code.Core.FSM
{
    public enum GameStateType
    {
        NotSet = -1,
        Menu = 0,
        Gameplay = 1,
        Pause = 2,
        GameOver = 3,
        Settings = 4,
        PopUp = 5,
        Win = 6,
        Exit = 99
    }
}
