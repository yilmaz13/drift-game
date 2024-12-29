using UnityEngine;

public class ResourceReferences : MonoBehaviour
{
    //  MEMBERS      
    public GameObject GameUIPrefab     { get { return _gameUIPrefab; } }
    public GameObject LoadingUIView    { get { return _loadingUIView; } }
    public GameObject GameViewPrefab   { get { return _gameViewPrefab; } }
    public GameObject RoadControllerPrefab   { get { return _roadControllerPrefab; } }
    public GameObject MainMenuUIPrefab { get { return _mainMenuUIPrefab; } }
    public GameResources GameResources { get { return _gameResources; } }
    public GameObject ProjectilePoolPrefab { get { return _projectilePoolPrefab; } }
   

    [SerializeField] private GameObject    _gameUIPrefab;
    [SerializeField] private GameObject    _loadingUIView;
    [SerializeField] private GameObject    _gameViewPrefab;
    [SerializeField] private GameObject    _roadControllerPrefab;
    [SerializeField] private GameObject    _mainMenuUIPrefab;
    [SerializeField] private GameObject    _projectilePoolPrefab;
    [SerializeField] private GameResources _gameResources;
}
