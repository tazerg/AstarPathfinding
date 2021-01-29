using CryoDI;

public class GameMain : UnityStarter
{
    protected override void SetupContainer(CryoContainer container)
    { 
        base.SetupContainer(container);

        var colorDatabase = ColorDatabaseLoader.LoadDatabase();
        container.RegisterInstance<IColorDatabase>(colorDatabase, LifeTime.Global);

        container.RegisterSingleton<IPathfindingAlgorithm, AstarAlgorithm>(LifeTime.Scene);

        container.RegisterSingleton<IErrorMessageFactory, ErrorMessageFactory>(LifeTime.Global);
        container.RegisterSingleton<IErrorMessageController, ErrorMessageController>(LifeTime.Global);

        container.RegisterSceneObject<UIGridView>(LifeTime.Scene); 
    }
}