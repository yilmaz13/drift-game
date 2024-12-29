using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DriftGameController : IDriftGameViewListener
{
    #region Private Members    

    private DriftGameData _data;
    private DriftGameView _view;
    private GameUIView _gameUIView;
    private IDriftGameListener _listener;
    private GameResources _gameResources;
    private RoadController _roadController;

    private Camera _camera;

    private PlayerCarController _playerCarController;
    private List<NPCCarController> _npcCarControllers;
    private SpawnManager _spawnManager;
    public DriftGameView View => _view;
    #endregion

    //  CONSTRUCTION
    public DriftGameController()
    {
        _data = new DriftGameData();
        _npcCarControllers = new List<NPCCarController>();
    }

    public void Initialize(DriftGameView view, GameUIView gameUIView, IDriftGameListener listener, GameResources gameResources, Camera camera)
    {
        _listener = listener;
        _gameResources = gameResources;
        _gameUIView = gameUIView;
        _camera = camera;
        _view = view;

        _spawnManager = SpawnManager.Instance;
        SubscribeEvents();
        InstantiateRoadController();
    }

    #region Private Methods      

    private void InstantiateRoadController()
    {
        if (_roadController == null)
        {
            GameObject roadObj = GameObject.Instantiate(_gameResources.RoadController, _view.ViewTransform);
            _roadController = roadObj.GetComponent<RoadController>();
        }
        
        _roadController.Initialize();
    }
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
      
        _playerCarController = GameObject.Instantiate(_gameResources.PlayerCar, _view.transform).GetComponent<PlayerCarController>();
        _playerCarController.Initialize();
        _roadController.Initialize();

        for (int i = 0; i < 50; i++)
        {
            //   NPCCarController nPCCarController = GameObject.Instantiate(_gameResources.AiCar, _view.transform).GetComponent<NPCCarController>();
            NPCCarController nPCCarController = _spawnManager.GetGameObject("AI Car").GetComponent<NPCCarController>();
            nPCCarController.transform.SetParent(_view.transform);

            nPCCarController.transform.localPosition = new Vector3(Random.Range(-2,2), 0, (i + 1)*50);
            _npcCarControllers.Add(nPCCarController);
        }
       
        GameEvents.SpawnedPlayer(_playerCarController.transform);
    }

    public void Unload()
    {
        _data.Unload();
        _view.Clear();

        for(int i = 0; i < _npcCarControllers.Count; i++)
        {
            _npcCarControllers[i].GetComponent<PoolObject>().GoToPool();           
        }

        _npcCarControllers.Clear();
        _roadController.Unload();
        GameObject.Destroy(_playerCarController.gameObject);
    }
    
    public void OnDestory()
    {
        UnsubscribeEvents();
    }

    #endregion
}
