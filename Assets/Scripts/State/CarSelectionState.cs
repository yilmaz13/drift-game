using UnityEngine;
using Assets.Scripts.Game.Views;
using Assets.Scripts.Resources;
using Assets.Scripts.State;

namespace Assets.Scripts.Game
{
    public class CarSelectionState : AStateBase
    {
        private IStateManager _stateManager;
        private ResourceReferences _resourceReferences;
        private GameResources _gameResources;
        private CarSelectionView _view;
        private DriftGameController _driftGameController;
        private SceneReferences _sceneReferences;

        public CarSelectionState(IStateManager stateManager,
                                 ResourceReferences resourceReferences,
                                 SceneReferences sceneReferences,
                                 DriftGameController driftGameController) : base(StateNames.CarSelection)
        {
            _stateManager = stateManager;
            _resourceReferences = resourceReferences;
            _gameResources = _resourceReferences.GameResources;
            _driftGameController = driftGameController;
            _sceneReferences = sceneReferences;
        }

        public override void Activate()
        {
            Debug.Log("<color=green>CarSelectionState Activated</color>");

            InitializeView();
            _view.Show();
        }

        public override void Deactivate()
        {
            Debug.Log("<color=red>CarSelectionState Deactivated</color>");

            if (_view != null)
            {
                _view.Hide();
                GameObject.Destroy(_view.gameObject);
                _view = null;
            }
        }

        public override void UpdateState()
        {
        }

        private void InitializeView()
        {
            if (_view == null)
            {
                GameObject viewObj = GameObject.Instantiate(_resourceReferences.CarSelectionViewPrefab, _sceneReferences.UIViewContainer.transform);
                _view = viewObj.GetComponent<CarSelectionView>();
                _view.Initialize(this, _gameResources);
            }
        }

        public void OnCarSelected(CarDataSO selectedCarData)
        {
            _driftGameController.SetSelectedCar(selectedCarData);
            _stateManager.ChangeState(StateNames.Gameplay);
        }
    }
}
