using UnityEngine;

public class DriftGameController : IDriftGameViewListener
{
    #region Private Members    

    private DriftGameData _data;
    private DriftGameView _view;
    private GameUIView _gameUIView;
    private IDriftGameListener _listener;
    private GameResources _gameResources;

    private Camera _camera;

    public DriftGameView View => _view;
    #endregion

    //  CONSTRUCTION
    public DriftGameController()
    {
        _data = new DriftGameData();
    }

    public void Initialize(DriftGameView view, GameUIView gameUIView, IDriftGameListener listener, GameResources gameResources, Camera camera)
    {
        _listener = listener;
        _gameResources = gameResources;
        _gameUIView = gameUIView;
        _camera = camera;
        _view = view;
    
        SubscribeEvents();
    }

    #region Private Methods      

    private void SubscribeEvents()
    {      
    }

    private void UnsubscribeEvents()
    {        
    }
    #endregion

    #region Public Methods    
    public void Load(Level _level)
    {
        _data.Load();
        _view.Create();
    }

    public void Unload()
    {
        _data.Unload();
        _view.Clear();      
    }
    
    public void OnDestory()
    {
        UnsubscribeEvents();
    }

    #endregion
}
