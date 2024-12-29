using Assets.Scripts.Game.Views;
using Assets.Scripts.Resources;
using UnityEngine;

namespace Assets.Scripts.State
{
    public class MenuGameState : AStateBase
    {
        #region Fields
        private IStateManager _stateManager;
        private IUserDataManager _userDataManager;
        private SceneReferences _sceneReferences;
        private ResourceReferences _resourceReferences;
        private MainMenuView _mainMenuView;
        #endregion

        #region Constructor
        public MenuGameState(IStateManager stateManager,
                             IUserDataManager userDataManager,
                             SceneReferences sceneReferences,
                             ResourceReferences resourceReferences) : base(StateNames.MainMenu)
        {
            _stateManager = stateManager;
            _userDataManager = userDataManager;
            _sceneReferences = sceneReferences;
            _resourceReferences = resourceReferences;
        }
        #endregion

        #region Public Methods
        public override void Activate()
        {
            Debug.Log("<color=green>MenuGame State</color> OnActive");

            if (_mainMenuView == null)
            {
                var _mainMenuViewObject = GameObject.Instantiate(_resourceReferences.MainMenuUIPrefab, _sceneReferences.UIViewContainer.transform);
                _mainMenuView = _mainMenuViewObject.transform.GetComponent<MainMenuView>();
            }

            var level = _userDataManager.CurrentLevel();

            _mainMenuView.Show();
            _mainMenuView.DisplayLevelText(level);

            GameEvents.OnClickGotoGameScene += OnClickGotoGameListener;
        }

        public override void Deactivate()
        {
            Debug.Log("<color=red>MenuGame State</color> OnDeactive");

            _mainMenuView.Hide();
            GameEvents.OnClickGotoGameScene -= OnClickGotoGameListener;
        }

        public override void UpdateState()
        {
        }
        #endregion

        #region Private Methods
        private void OnClickGotoGameListener()
        {
            _stateManager.ChangeTransitionState(StateNames.Loading, StateNames.Game);
        }
        #endregion
    }
}
