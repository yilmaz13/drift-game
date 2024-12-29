using UnityEngine;
using Assets.Scripts.Popup;
using Assets.Scripts.Resources;

namespace Assets.Scripts.State
{
    public class GamePlayGameState : AStateBase,
                             IDriftGameListener
    {
        #region Private Members    

        IStateManager _stateManager;
        IUserDataManager _userDataManager;
        SceneReferences _sceneReferences;
        ResourceReferences _resourceReferences;

        private Camera _camera;
        private GameUIView _gameUIView;
        private DriftGameView _driftGameView;
        private DriftGameController _driftGameController;
        private RoadController _roadController;
        private CameraController _cameraController;

        private GameResources _gameResources;
        private Level _level;
        private bool _startedGame;

        //Pool
        #endregion

        //  CONSTRUCTION
        public GamePlayGameState(IStateManager stateManager,
                                 IUserDataManager userDataManager,
                                 SceneReferences sceneReferences,
                                 ResourceReferences resourceReferences) : base(StateNames.Game)
        {
            _stateManager = stateManager;
            _userDataManager = userDataManager;
            _sceneReferences = sceneReferences;
            _resourceReferences = resourceReferences;
            _gameResources = _resourceReferences.GameResources;
        }

        #region State Methos
        public override void Activate()
        {
            Debug.Log("<color=green>GameplayGame State</color> OnActive");

            InitializeCamera();
            InitializeDriftGame();

            _driftGameView.Show();
            SubscribeEvents();
            PlayLevel();
            Application.targetFrameRate = 60;
        }

        public override void Deactivate()
        {
            Debug.Log("<color=red>GameplayGame State</color> DeOnActive");

            UnsubscribeEvents();
        }

        public override void UpdateState()
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            _gameUIView.SetFPSCount(fps);
        }

        #endregion

        #region Private Methos

        private void InitializeDriftGame()
        {
            _camera = _sceneReferences.MainCam;
            if (_driftGameView == null)
            {
                InstantiateGameUI();
                InstantiateDriftGameView();
                InstantiateDriftController();
            }
        }
        private void InstantiateGameUI()
        {
            GameObject gameUIViewObject = GameObject.Instantiate(_resourceReferences.GameUIPrefab, _sceneReferences.UIViewContainer.transform);
            _gameUIView = gameUIViewObject.GetComponent<GameUIView>();
        }

        private void InstantiateDriftGameView()
        {
            GameObject mainMenuObject = GameObject.Instantiate(_resourceReferences.GameViewPrefab, _sceneReferences.ViewContainer.transform);
            _driftGameView = mainMenuObject.GetComponent<DriftGameView>();
            _driftGameView.Initialize(_driftGameController, Camera.main, _resourceReferences.GameResources);
        }

        private void InstantiateDriftController()
        {
            _driftGameController = new DriftGameController();
            _driftGameController.Initialize(_driftGameView, _gameUIView, this, _resourceReferences.GameResources, _camera);
        }

        private void InitializeCamera()
        {
            _cameraController = _sceneReferences.MainCam.GetComponent<CameraController>();
        }

        private void PlayLevel()
        {
            int index = _userDataManager.CurrentLevel();

            _level = ScriptableObject.CreateInstance<Level>();
            var currentLevelTemplate = UnityEngine.Resources.Load<Level>("Level" + index);
            _level.CopyLevel(_level, currentLevelTemplate);

            _driftGameController.Load(_level);

            _startedGame = true;

            _gameUIView.LoadLevel(index);
            _gameUIView.Show();
        }
        private void SubscribeEvents()
        {
            GameEvents.OnStartGame += StartGameListener;
            GameEvents.OnEndGame += EndGameListener;
            GameEvents.OnClickLevelNext += PlayNextLevel;
            GameEvents.OnClickGotoMenu += GotoMenu;
            GameEvents.OnClickLevelRestart += RestartLevel;
            GameEvents.OnSpawnedPlayer += FollowCameraPlayer;
        }

        private void UnsubscribeEvents()
        {
            _driftGameController.OnDestory();

            GameEvents.OnStartGame -= StartGameListener;
            GameEvents.OnEndGame -= EndGameListener;
            GameEvents.OnClickLevelNext -= PlayNextLevel;
            GameEvents.OnClickGotoMenu -= GotoMenu;
            GameEvents.OnClickLevelRestart -= RestartLevel;
            GameEvents.OnSpawnedPlayer -= FollowCameraPlayer;
        }

        private void ClearScene()
        {
            _gameUIView.UnloadLevel();
            _gameUIView.Hide();

            _driftGameController.Unload();
            PopupManager.Instance.HideAllPopups();
        }
        private void GotoMenu()
        {
            ClearScene();
            _stateManager.ChangeTransitionState(StateNames.Loading, StateNames.MainMenu);
        }

        private void RestartLevel()
        {
            ClearScene();
            PlayLevel();
        }
        private void PlayNextLevel()
        {
            ClearScene();
            PlayLevel();
        }

        private void EndGameListener(bool success)
        {
            if (!_startedGame)
                return;

            _startedGame = false;

            if (success)
            {
                //LevelSuccess           
                PopupManager.Instance.ShowPopup(PopupNames.LevelSuccessPopup);
            }
            else
            {
                //LevelFail
                PopupManager.Instance.ShowPopup(PopupNames.LevelFailedPopup);
            }
        }

        private void StartGameListener()
        {

        }
        #endregion

        public void FollowCameraPlayer(Transform playerTranfrom)
        {
            _cameraController.Initialize(playerTranfrom, _gameResources.FollowCameraSmootTime);
        }
    }
}
