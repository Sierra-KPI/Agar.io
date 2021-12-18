using System;
using System.Collections.Generic;
using Agario.Model;
using Agario.UnityView;
using UnityEngine;

namespace Agario.Controller
{
    public class Controller : MonoBehaviour
    {
        private AgarioGame _game;
        private View _view;

        [Header("Game Settings")]

        [Header("Players")]
        [SerializeField]
        private GameObject _playerPrefab;

        [Header("Food")]
        [SerializeField]
        private GameObject _foodPrefab;

        private void Start()
        {
            //int _rabbitsNumber = EntityFactory.GetEntitiesNumber(EntityType.Rabbit);
            //int _deersNumber = EntityFactory.GetEntitiesNumber(EntityType.Deer) / 10;
            //int _wolvesNumber = EntityFactory.GetEntitiesNumber(EntityType.Wolf);

            _game = new();
            _game.Start();
            _view = gameObject.AddComponent<View>();
            //_view.LineMaterial = _lineMaterial;

            //_sceneLoader = gameObject.AddComponent<SceneLoader>();
            //_sceneLoader.SetPauseMenu();

            CreateEntities();
        }

        private void Update()
        {
            //PauseMenuController();

            //if (_sceneLoader.isPaused)
            //{
            //    return;
            //}

            ReadMoves();
            //TryToKillByWolf();
            _game.Update();
            _view.ChangeGameObjectsPositions();
            //_view.DeleteDeadAnimals();
            //CheckGameEnd();
        }

        private void CreateEntities()
        {
            _view.CreateEntityObjects(_foodPrefab, _playerPrefab);

            foreach (EntityType entityType in
                (EntityType[])Enum.GetValues(typeof(EntityType)))
            {
                    List<Entity> animals = _game.GetAnimals(entityType);
                    _view.CreateEntities(animals);
            }
        }

        private void PlayerControler()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            _game.Hunter.MoveTo(h, v);
        }

        private void MousePosition()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector3 screenPosition = new Vector3(Input.mousePosition.x,
                    Input.mousePosition.y);
                Vector3 vector = Camera.main.ScreenToWorldPoint(screenPosition);
                vector.z = 0;
                //TryToKillByHunter(vector);
            }
        }

        //private void TryToKillByHunter(Vector3 vectorEnd)
        //{
        //    Vector3 vectorStart = new Vector3(_game.Hunter.Position.X,
        //        _game.Hunter.Position.Y);

        //    if (_game.Hunter.MakeShot())
        //    {
        //        var deadAnimal = _game.TryToKillAnimalByHunter(vectorEnd.x,
        //            vectorEnd.y);
        //        var shotLength = _game.Hunter.ShotDistance;

        //        if (deadAnimal != null)
        //        {
        //            _view.DestroyEntity(deadAnimal);
        //            shotLength = (_game.Hunter.Position -
        //                deadAnimal.Position).Length();
        //        }

        //        var direction = (vectorEnd - vectorStart).normalized;
        //        vectorEnd = vectorStart + direction * shotLength;
        //        _view.DrawShotLine(vectorStart, vectorEnd);
        //    }
        //}

        //private void TryToKillByWolf()
        //{
        //    var deadAnimal = _game.TryToKillAnimalByWolf();

        //    if (deadAnimal != null)
        //    {
        //        _view.DestroyEntity(deadAnimal);
        //    }

        //    var isHunterDead = _game.TryToKillHunter();

        //    if (isHunterDead)
        //    {
        //        _sceneLoader.LoadLoosingGameEnd();
        //    }
        //}

        private void ReadMoves()
        {
            PlayerControler();
            MousePosition();
        }

        //private void PauseMenuController()
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        _sceneLoader.LoadPauseMenu();
        //    }
        //}

        //private void CheckGameEnd()
        //{
        //    var allEntities = _game.GetAllEntities();

        //    if (allEntities.Count == 1 && allEntities[0].EntityType ==
        //        EntityType.Hunter)
        //    {
        //        _sceneLoader.LoadWinningGameEnd();
        //    }

        //    if (_game.Hunter.IsDead)
        //    {
        //        _sceneLoader.LoadLoosingGameEnd();
        //    }
        //}
    }
}
