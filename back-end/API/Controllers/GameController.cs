﻿using API.DTOs;
using API.Services;
using Data.DatabaseLayer;
using Data.ModelLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GameController : ControllerBase
    {
        private GameService _gameService;

        public GameController(IConfiguration inConfiguration)
        {
            ServiceInjector.Con = inConfiguration.GetConnectionString("ChatMayhem Connection") ?? "No connection string";

            string? connectionString = inConfiguration.GetConnectionString("ChatMayhem Connection");
            if (connectionString == null)
            {
                Console.WriteLine("Connection string is null");
            }

            _gameService = ServiceInjector.GameService; 
        }

        [HttpPost]
        public ActionResult<Game> Post(GameDto inGame)
        {
            Game game = GameDto.Convert(inGame);

            game = ServiceInjector.GameService.Add(game);

            if (game == null)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return game;
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<GameDto> Put(GameDto inGame, int id)
        {
            Game game = GameDto.Convert(inGame);

            game = ServiceInjector.GameService.UpdateGame(id, game);
            GameDto returnGame = GameDto.Convert(game);

            if (returnGame == null)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }

            return returnGame;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Game> Get(int id)
        {
            Game? game = ServiceInjector.GameService.GetGameById(id);

            if (game == null)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }

            return game;
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            bool deleted = ServiceInjector.GameService.DeleteGame(id);

            if (!deleted)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }

            return new StatusCodeResult(StatusCodes.Status200OK);
        }
    }
}
