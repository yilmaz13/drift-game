using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Game;
using Assets.Scripts.Resources;

namespace Assets.Scripts.Game.Views
{
    public class CarSelectionView : ABaseUIView
    {
        [SerializeField] private Transform contentTransform;
        [SerializeField] private GameObject carUIPrefab;
        private CarSelectionState _state;
        private GameResources _gameResources;
        private List<CarDataSO> _carDataList;

        public void Initialize(CarSelectionState state, GameResources gameResources)
        {
            _state = state;
            _gameResources = gameResources;
            _carDataList = _gameResources.CarDataList;
            PopulateCarList();
        }

        private void PopulateCarList()
        {
            foreach (Transform child in contentTransform)
            {
                Destroy(child.gameObject);
            }

            foreach (var carData in _carDataList)
            {
                GameObject carUIObj = Instantiate(carUIPrefab, contentTransform);
                CarUIController carUIController = carUIObj.GetComponent<CarUIController>();
                carUIController.Initialize(carData, _state.OnCarSelected);
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
